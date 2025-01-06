using Demo.Business.Exceptions;

namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[ExceptionFilter]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }


    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<ApplicationDto>> GetAll()
    {
        string userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        UserType userType = Enum.Parse<UserType>(User.Claims.First(c => c.Type == nameof(UserType)).Value);


        if (userType == UserType.Volunteer)
            return Ok(_applicationService.GetApplicationsForVolunteer(userId));
        else
            return Ok(_applicationService.GetApplicationsForOrganization(userId));
    }


    [HttpGet("{id}")]
    public ActionResult<ApplicationDto> GetById(int id)
    {
        var application = _applicationService.GetApplicationById(id);
        if (application == null)
        {
            return NotFound($"Application with ID {id} not found.");
        }

        return Ok(application);
    }


    [HttpGet("{id}/reviews")]
    public ActionResult<ApplicationWithReviewsDto> GetWithReviews(int id)
    {
        var application = _applicationService.GetApplicationWithReviews(id);
        if (application == null)
        {
            return NotFound($"Application with ID {id} not found.");
        }

        return Ok(application);
    }


    [HttpGet("{id}/details")]
    public ActionResult<ApplicationWithOpportunityAndVolunteerDto> GetWithOpportunityAndVolunteer(int id)
    {
        var application = _applicationService.GetWithOpportunityAndVolunteer(id);
        if (application == null)
        {
            return NotFound($"Application with ID {id} not found.");
        }

        return Ok(application);
    }


    [HttpPost]
    [VolunteerFilter]
    public ActionResult<ApplicationDto> Create([FromBody] CreateApplicationDto createApplicationDto)
    {
        string volunteerId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        if (createApplicationDto == null)
            return BadRequest("Application data is null.");

        var createdApplication = _applicationService.CreateApplication(volunteerId,createApplicationDto);
        return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
    }


    [HttpPut]
    [OrganizationFilter]
    public ActionResult<ApplicationDto> Update([FromBody] UpdateApplicationDto updateApplicationDto)
    {
        string organizationId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        var updatedApplication = _applicationService.UpdateApplication(updateApplicationDto, organizationId);
        if (updatedApplication == null)
            return NotFound($"Application with ID: {updateApplicationDto.Id} not found.");

        return Ok(updatedApplication);
    }


    [HttpDelete("{id}")]
    [VolunteerFilter]
    public IActionResult Delete(int id)
    {
        var volunteerId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        _applicationService.DeleteApplication(id, volunteerId);
        return NoContent();   
    }
}
