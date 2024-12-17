namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
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
        try
        {
            var applications = _applicationService.GetAllApplications();
            return Ok(applications);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}")]
    public ActionResult<ApplicationDto> GetById(int id)
    {
        try
        {
            var application = _applicationService.GetApplicationById(id);
            if (application == null)
            {
                return NotFound($"Application with ID {id} not found.");
            }

            return Ok(application);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/reviews")]
    public ActionResult<ApplicationWithReviewsDto> GetWithReviews(int id)
    {
        try
        {
            var application = _applicationService.GetApplicationWithReviews(id);
            if (application == null)
            {
                return NotFound($"Application with ID {id} not found.");
            }

            return Ok(application);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/details")]
    public ActionResult<ApplicationWithOpportunityAndVolunteerDto> GetWithOpportunityAndVolunteer(int id)
    {
        try
        {
            var application = _applicationService.GetWithOpportunityAndVolunteer(id);
            if (application == null)
            {
                return NotFound($"Application with ID {id} not found.");
            }

            return Ok(application);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    //[VolunteerFilter]
    public ActionResult<ApplicationDto> Create([FromBody] CreateApplicationDto createApplicationDto)
    {
        //string tokenId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        //if (createApplicationDto.VolunteerId != tokenId)
        //    return BadRequest($"Mismatch between logged in user ID and the data to be proceeded");


        try
        {
            if (createApplicationDto == null)
                return BadRequest("Application data is null.");

            var createdApplication = _applicationService.CreateApplication(createApplicationDto);
            return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id}")]
    //[OrganizationFilter]
    public ActionResult<ApplicationDto> Update(int id, [FromBody] UpdateApplicationDto updateApplicationDto)
    {       

        //if (updateApplicationDto == null)
        //    return BadRequest("Application data is null.");

        //if (id != updateApplicationDto.Id)
        //    return BadRequest("ID mismatch.");

        try
        {
            var updatedApplication = _applicationService.UpdateApplication(id, updateApplicationDto);
            if (updatedApplication == null)
                return NotFound($"Application with ID {id} not found.");

            return Ok(updatedApplication);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }


    [HttpDelete("{id}")]
    //[VolunteerFilter]
    public IActionResult Delete(int id)
    {
        
        try
        {
            if (!_applicationService.DeleteApplication(id))
                return NotFound($"Application with ID {id} not found.");


            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
