using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IVolunteerService _volunteerService;
    private readonly IOrganizationService _organizationService;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<ApplicationUser> userManager, IVolunteerService volunteerService, 
        IOrganizationService organizationService, IConfiguration configuration)
    {
        _userManager = userManager;
        _volunteerService = volunteerService;
        _organizationService = organizationService;
        _configuration = configuration;
    }


    #region Register

    [HttpPost("registerVolunteer")]
    public async Task<ActionResult<VolunteerDto>> RegisterVolunteer([FromForm] RegisterVolunteerDto dto)
    {
        try
        {
            var user = await RegisterApplicationUser(dto, UserType.Volunteer);
            if (user == null)
                return BadRequest(ModelState);

            _volunteerService.AddVolunteer(dto,user.Id);
            return RedirectToAction("Get", "Volunteer", new { id = user.Id }); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpPost("registerOrganization")]
    public async Task<ActionResult<OrganizationDetailsDto>> RegisterOrganization([FromForm] RegisterOrganizationDto dto)
    {
        try
        {
            var user = await RegisterApplicationUser(dto, UserType.Organization);
            if (user == null)
                return BadRequest(ModelState);

            _organizationService.Add(dto, user.Id);
            return RedirectToAction("GetById", "Organization", new { id = user.Id });

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private async Task<ApplicationUser?> RegisterApplicationUser(RegisterUserDto userDto, UserType userType)
    {
        var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError(string.Empty, "This user already have an account.");
            return null;
        }


        var user = new ApplicationUser
        {
            Email = userDto.Email,
            UserName = userDto.Name,
            PhoneNumber = userDto.Phone,
            UserType = userType
        };

        user.Photo = (userDto.Photo != null ? PhotoSetting.UploadPhoto(userDto.Photo, userType.ToString()) 
            : (userType == UserType.Organization ? PhotoSetting.OrganizationIcon : PhotoSetting.VolunteerIcon));


        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return null;
        }

        return user;
    }

    #endregion


    #region Login

    [HttpPost("Login")]
    public async Task<ActionResult<string>> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return BadRequest("Wrong email or password");

        //Give him a JWT
        var claims = new List<Claim> 
        { 
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? "..."),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "..."),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserType", user.UserType.ToString())    
        }; 

        
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secretKey"]?? "TempSecretKey016188"));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

        JwtSecurityToken tokenDesign = new JwtSecurityToken(
            issuer: _configuration["JWT:issuer"],
            expires: DateTime.UtcNow.AddDays(1),
            claims: claims,
            signingCredentials: credentials
        );


        return Ok(new JwtSecurityTokenHandler().WriteToken(tokenDesign));
    }

    #endregion


    #region Delete Account

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete() 
    {
        string id = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";


        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound($"No user found with ID = {id}");

        if(user.Photo != null)
            PhotoSetting.Delete(user.UserType.ToString(), user.Photo);

        try
        {
            int rows = await _userManager.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
            
            if (rows > 0) 
                return NoContent();
            else 
                return BadRequest($"Error in deleting user with ID {id}");
        }     
        catch(Exception ex) 
        { 
            return BadRequest($"Error {ex.Message} in deleting user with ID {id}");  
        }

    }

    #endregion


    #region Update

    [HttpPut("updateVolunteer")]
    [Authorize]
    public async Task<ActionResult> UpdateVolunteer([FromForm] UpdateVolunteerDto dto)
    {
        string volunteerId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        try
        {
            var user = await _userManager.FindByIdAsync(volunteerId);
            if (user == null || user.UserType != UserType.Volunteer)
                return NotFound("Volunteer account not found.");

            // Update ApplicationUser properties
            user.UserName = dto.Name;
            user.PhoneNumber = dto.Phone;
            
            if(user.Photo != null)
                PhotoSetting.Delete(UserType.Volunteer.ToString(), user.Photo);
            user.Photo = 
                (dto.Photo == null ? PhotoSetting.VolunteerIcon : PhotoSetting.UploadPhoto(dto.Photo, UserType.Volunteer.ToString()));

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                foreach (var error in updateUserResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return BadRequest(ModelState);
            }

            // Update Volunteer fields
            _volunteerService.UpdateVolunteer(volunteerId, dto);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpPut("updateOrganization")]
    [Authorize]
    public async Task<ActionResult> UpdateOrganization([FromForm] UpdateOrganizationDto dto)
    {
        string organizationId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        try
        {
            var user = await _userManager.FindByIdAsync(organizationId);
            if (user == null || user.UserType != UserType.Organization)
                return NotFound("Organization account not found.");

            // Update ApplicationUser properties
            user.UserName = dto.Name;
            user.PhoneNumber = dto.Phone;

            if (user.Photo != null)
                PhotoSetting.Delete(UserType.Organization.ToString(), user.Photo);
            user.Photo =
                (dto.Photo == null ? PhotoSetting.OrganizationIcon : PhotoSetting.UploadPhoto(dto.Photo, UserType.Organization.ToString()));


            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                foreach (var error in updateUserResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return BadRequest(ModelState);
            }

            // Update Organization fields
            _organizationService.UpdateOrganization(organizationId,dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    #endregion
   
}

