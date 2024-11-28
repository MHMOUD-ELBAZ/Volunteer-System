using Demo.Data.Models;


namespace Demo.Data.IRepositories;

public interface ISkillRepository : IRepository<Skill>
{
    public Skill? GetSkill(int id);
    public Skill? GetSkillWithOpportunities(int id);
    public Skill? GetSkillWithVolunteers(int id);

}
