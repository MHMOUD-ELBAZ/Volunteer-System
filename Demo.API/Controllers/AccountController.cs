using Demo.API.Services;
using Demo.Business.DTOs.Organization;
using Demo.Business.DTOs.Volunteer;


namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IVolunteerService _volunteerService;
    private readonly IOrganizationService _organizationService;

    public AccountController(UserManager<ApplicationUser> userManager, IVolunteerService volunteerService, IOrganizationService organizationService)
    {
        _userManager = userManager;
        _volunteerService = volunteerService;
        _organizationService = organizationService;
    }


    #region Register

    [HttpPost("registerV")]
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


    [HttpPost("registerO")]
    public async Task<ActionResult<OrganizationDetailsDto>> RegisterOrganization([FromForm] RegisterOrganizationDto dto)
    {
        try
        {
            var user = await RegisterApplicationUser(dto, UserType.Volunteer);
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


    #region Delete Account

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) 
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound($"No user found with ID = {id}");

        if(user.Photo != null)
            PhotoSetting.Delete(user.UserType.ToString(), user.Photo);

        var result  = await _userManager.DeleteAsync(user);
        if(result.Succeeded)
            return NoContent();

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return BadRequest(ModelState); 
    }

    #endregion


    #region Update

    [HttpPut("updateVolunteer/{volunteerId}")]
    public async Task<ActionResult> UpdateVolunteer(string volunteerId, [FromForm] UpdateVolunteerDto dto)
    {
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


    [HttpPut("updateOrganization/{organizationId}")]
    public async Task<ActionResult> UpdateOrganization(string organizationId,[FromForm] UpdateOrganizationDto dto)
    {
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

//var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//if (string.IsNullOrEmpty(userId))
//    return Unauthorized("User is not logged in.");