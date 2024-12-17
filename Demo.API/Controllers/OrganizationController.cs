namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }


    [HttpGet("{id}")]
    public ActionResult<OrganizationDetailsDto> GetById(string id)
    {
        try
        {
            var organization = _organizationService.GetOrganizationById(id);
            if (organization == null)
                return NotFound($"No organization found with ID: {id}");

            return Ok(organization);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/applications")]
    public ActionResult<OrganizationWithApplicationsDto> GetWithApplications(string id)
    {
        try
        {
            var organization = _organizationService.GetOrganizationWithApplications(id);
            if (organization == null)
                return NotFound($"No organization found with ID: {id}");

            return Ok(organization);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/reviews")]
    public ActionResult<OrganizationWithReviewsDto> GetWithReviews(string id)
    {
        try
        {
            var organization = _organizationService.GetWithReviews(id);
            if (organization == null)
                return NotFound($"No organization found with ID: {id}");

            return Ok(organization);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
      
    
    [HttpGet("{id}/opportunities")]
    public ActionResult<OrganizationWithOpportunitiesDto> GetWithOpportunities(string id)
    {
        try
        {
            var organization = _organizationService.GetOrganizationWithOpportunities(id);
            if (organization == null)
                return NotFound($"No organization found with ID: {id}");

            return Ok(organization);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<OrganizationDto>> GetAll()
    {
        try
        {
            var organizations = _organizationService.GetAllOrganizations();
            return Ok(organizations);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

