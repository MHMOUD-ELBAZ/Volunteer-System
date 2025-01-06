using Demo.Business.DTOs.Opportunity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExceptionFilter]
public class VolunteerController : ControllerBase
{
    private readonly IVolunteerService _volunteerService;

    public VolunteerController(IVolunteerService volunteerService)
    {
        _volunteerService = volunteerService;
    }


    [HttpGet("{id}")]
    public ActionResult<VolunteerDto> Get(string id)
    {
        var volunteer = _volunteerService.GetById(id);
        if (volunteer == null)
            return NotFound($"No volunteer with this ID: {id}");

        return Ok(volunteer);
    }


    [HttpGet("GetAll")]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<IEnumerable<VolunteerDto>> GetAll()
    {
        var volunteers = _volunteerService.GetAllVolunteers();
        return Ok(volunteers);
    }

    [HttpGet("applications")]
    [Authorize]
    [VolunteerFilter]
    public ActionResult<VolunteerWithApplicationsDto> GetVolunteerApplications()
    {
        string volunteerId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        var volunteer = _volunteerService.GetWithApplications(volunteerId);
        if (volunteer != null) return Ok(volunteer);
        
        return NotFound($"No volunteer found with ID: {volunteer}");
    }

    [HttpGet("{id}/reviews")]
    public ActionResult<VolunteerWithReviewsDto> GetVolunteerWithReviews(string id) 
    {
        var volunteer = _volunteerService.GetWithReviews(id);

        if (volunteer != null) return Ok(volunteer);

        return NotFound($"No volunteer found with ID: {id}");
    }

    [HttpPut]
    [Authorize]
    [VolunteerFilter]
    public ActionResult<VolunteerDto> UpdateVolunteerSkills(UpdateVolunteerSkillsDto dto)
    {
        string volunteerId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";

        var updated = _volunteerService.UpdateVolunteerSkills(volunteerId, dto);
        if (updated != null) return Ok(updated);

        return NotFound($"No volunteer with this ID: {volunteerId}");
    }
}
