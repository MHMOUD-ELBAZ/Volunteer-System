
namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExceptionFilter]
public class OpportunityController : ControllerBase
{
    private readonly IOpportunityService _opportunityService;

    public OpportunityController(IOpportunityService opportunityService)
    {
        _opportunityService = opportunityService;
    }

    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<OpportunityWithOrganizationDto>> GetAll()
    {
        var opportunities = _opportunityService.GetAllOpportunities();
        return Ok(opportunities);
    }


    [HttpGet("{id}")]
    public ActionResult<OpportunityDto> GetById(int id)
    {
        var opportunity = _opportunityService.GetById(id);
        if (opportunity == null)
        {
            return NotFound($"Opportunity with ID {id} not found.");
        }

        return Ok(opportunity);
    }


    [HttpGet("{id}/organization")]
    public ActionResult<OpportunityWithOrganizationDto> GetOpportunityWithOrganization(int id)
    {
        var opportunity = _opportunityService.GetOpportunityWithOrganization(id);
        if (opportunity == null)
        {
            return NotFound($"Opportunity with ID {id} not found.");
        }

        return Ok(opportunity);
    }


    [HttpGet("{id}/applications")]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<OpportunityWithApplicationsDto> GetOpportunityWithApplications(int id)
    {
        string organizationId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        var opportunity = _opportunityService.GetOpportunityWithApplications(id);
        
        if (opportunity == null)
            return NotFound($"Opportunity with ID {id} not found.");

        if (opportunity.OrganizationId != organizationId)
            return new ObjectResult("Only opportunity owner can view applications.") { StatusCode = StatusCodes.Status403Forbidden };


        return Ok(opportunity);
    }

    [HttpPost]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<OpportunityDto> Create([FromBody] CreateOpportunityDto opportunityDto)
    {
        string organizationId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        if (opportunityDto == null)
        {
            return BadRequest("Opportunity data is null.");
        }

        var createdOpportunity = _opportunityService.Create(opportunityDto, organizationId);
        return CreatedAtAction(nameof(GetById), new { id = createdOpportunity.Id }, createdOpportunity);
    }

    [HttpPut("{id}")]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<OpportunityDto> Update(int id, [FromBody] CreateOpportunityDto opportunityDto)
    {

        if (opportunityDto == null)
            return BadRequest("Opportunity data is null.");

        string organizationId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        var updatedOpportunity = _opportunityService.Update(id, opportunityDto, organizationId);
        if (updatedOpportunity == null)
            return NotFound($"Opportunity with ID {id} not found.");


        return Ok(updatedOpportunity);
    }


    [HttpDelete("{id}")]
    [Authorize]
    [OrganizationFilter]
    public IActionResult Delete(int id)
    {
        string organizationId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        _opportunityService.Delete(id, organizationId);

        return NoContent();
    }

}

