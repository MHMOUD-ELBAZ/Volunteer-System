using Demo.Business.DTOs.Skill;

namespace Demo.Business.IServices;
public interface ISkillService
{
    SkillDto? GetSkillById(int id);
    SkillWithOpportunitiesDto? GetSkillWithOpportunities(int id);
    SkillWithVolunteersDto? GetSkillWithVolunteers(int id);
    IEnumerable<SkillDto> GetAllSkills();
    SkillDto CreateSkill(CreateSkillDto skillDto);
    SkillDto? UpdateSkill(int id, SkillDto skillDto);
    bool DeleteSkill(int id);
}

