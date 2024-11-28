using Demo.Business.DTOs.Skill;

namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        try
        {
            var skills = _skillService.GetAllSkills();
            return Ok(skills);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}")]
    public ActionResult<SkillDto> GetById(int id)
    {
        try
        {
            var skill = _skillService.GetSkillById(id);
            if (skill == null)
            {
                return NotFound($"Skill with ID {id} not found.");
            }

            return Ok(skill);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/opportunities")]
    public ActionResult<SkillWithOpportunitiesDto> GetWithOpportunities(int id)
    {
        try
        {
            var skill = _skillService.GetSkillWithOpportunities(id);
            if (skill == null)
            {
                return NotFound($"Skill with ID {id} not found.");
            }

            return Ok(skill);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/volunteers")]
    public ActionResult<SkillWithVolunteersDto> GetWithVolunteers(int id)
    {
        try
        {
            var skill = _skillService.GetSkillWithVolunteers(id);
            if (skill == null)
            {
                return NotFound($"Skill with ID {id} not found.");
            }

            return Ok(skill);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    public IActionResult Create([FromBody] CreateSkillDto skillDto)
    {
        try
        {
            if (skillDto == null)
            {
                return BadRequest("Skill data is null.");
            }

            var createdSkill = _skillService.CreateSkill(skillDto);
            return CreatedAtAction(nameof(GetById), new { id = createdSkill.Id }, createdSkill);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] SkillDto skillDto)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var isDeleted = _skillService.DeleteSkill(id);
            if (!isDeleted)
            {
                return NotFound($"Skill with ID {id} not found.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
