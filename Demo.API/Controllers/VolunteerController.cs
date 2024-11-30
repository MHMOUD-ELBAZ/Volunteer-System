

namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]

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
        try
        {
            var volunteer = _volunteerService.GetById(id);
            if (volunteer == null)
                return NotFound($"No volunteer with this ID: {id}");

            return Ok(volunteer);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }


    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<VolunteerDto>> GetAll()
    {
        try
        {
            var volunteers = _volunteerService.GetAllVolunteers();
            return Ok(volunteers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/applications")]
    public ActionResult<VolunteerWithApplicationsDto> GetVolunteerApplications(string id)
    {
        try
        {
            var volunteer = _volunteerService.GetWithApplications(id);
            if (volunteer != null) return Ok(volunteer);
            return NotFound($"No volunteer found with ID: {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public ActionResult<VolunteerDto> UpdateVolunteerSkills(UpdateVolunteerSkillsDto dto)
    {
        try
        {
            var updated = _volunteerService.UpdateVolunteerSkills(dto);
            if (updated != null) return Ok(updated);

            return NotFound($"No volunteer with this ID: {dto.volunteerId}");
        }
        catch (Exception ex) { return StatusCode(500, $"Internal server error: {ex.Message}"); }
    }
}
