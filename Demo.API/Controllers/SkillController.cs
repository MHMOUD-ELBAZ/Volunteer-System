
namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExceptionFilter]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }


    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<SkillDto>> GetAll()
    {
        var skills = _skillService.GetAllSkills();
        return Ok(skills);
    }


    [HttpGet("{id}")]
    public ActionResult<SkillDto> GetById(int id)
    {
        var skill = _skillService.GetSkillById(id);
        if (skill == null)
        {
            return NotFound($"Skill with ID {id} not found.");
        }

        return Ok(skill);
    }


    [HttpGet("{id}/opportunities")]
    public ActionResult<SkillWithOpportunitiesDto> GetWithOpportunities(int id)
    {
        var skill = _skillService.GetSkillWithOpportunities(id);
        if (skill == null)
        {
            return NotFound($"Skill with ID {id} not found.");
        }

        return Ok(skill);
    }


    [HttpGet("{id}/volunteers")]
    public ActionResult<SkillWithVolunteersDto> GetWithVolunteers(int id)
    {
        var skill = _skillService.GetSkillWithVolunteers(id);
        if (skill == null)
        {
            return NotFound($"Skill with ID {id} not found.");
        }

        return Ok(skill);
    }


    [HttpPost]
    [Authorize]
    [OrganizationFilter]
    public IActionResult Create([FromBody] CreateSkillDto skillDto)
    {
        if (skillDto == null)
        {
            return BadRequest("Skill data is null.");
        }

        var createdSkill = _skillService.CreateSkill(skillDto);
        return CreatedAtAction(nameof(GetById), new { id = createdSkill.Id }, createdSkill);
    }


    [HttpPut("{id}")]
    [Authorize]
    [OrganizationFilter]
    public IActionResult Update(int id, [FromBody] SkillDto skillDto)
    {
        if (skillDto == null)
        {
            return BadRequest("Skill data is null.");
        }

        var updatedSkill = _skillService.UpdateSkill(id, skillDto);
        if (updatedSkill == null)
        {
            return NotFound($"Skill with ID {id} not found.");
        }

        return Ok(updatedSkill);
    }


    [HttpDelete("{id}")]
    [Authorize]
    [OrganizationFilter]
    public IActionResult Delete(int id)
    {
        var isDeleted = _skillService.DeleteSkill(id);
        if (!isDeleted)
        {
            return NotFound($"Skill with ID {id} not found.");
        }

        return NoContent();
    }
}
