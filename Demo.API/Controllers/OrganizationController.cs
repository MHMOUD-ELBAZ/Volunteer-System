namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExceptionFilter]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }


    [HttpGet("details")]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<OrganizationDetailsDto> GetById()
    {
        string organizationId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        var organization = _organizationService.GetOrganizationById(organizationId);
        if (organization == null)
            return NotFound($"No organization found with ID: {organizationId}");

        return Ok(organization);
    }


    [HttpGet("reviews")]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<OrganizationWithReviewsDto> GetWithReviews()
    {
        string id = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        var organization = _organizationService.GetWithReviews(id);

        if (organization == null)
            return NotFound($"No organization found with ID: {id}");

        return Ok(organization);
    }
      
    
    [HttpGet("opportunities")]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<OrganizationWithOpportunitiesDto> GetWithOpportunities()
    {
        string id = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        
        var organization = _organizationService.GetOrganizationWithOpportunities(id);
        
        if (organization == null)
            return NotFound($"No organization found with ID: {id}");

        return Ok(organization);
    }


    [HttpGet("GetAll")]
    [Authorize]
    public ActionResult<IEnumerable<OrganizationDto>> GetAll()
    {
        var organizations = _organizationService.GetAllOrganizations();
        return Ok(organizations);
    }

}

