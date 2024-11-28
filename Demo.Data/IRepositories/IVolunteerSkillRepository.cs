using Demo.Data.Models;


namespace Demo.Data.IRepositories;

public interface IVolunteerSkillRepository : IRepository<VolunteerSkill>
{
    public VolunteerSkill? Get(string volunteerId,  int skillId);
    public IEnumerable<VolunteerSkill> GetSkillsForVolunteer(string volunteerId);
    public IEnumerable<VolunteerSkill> GetVolunteersWithSkill(int skillId);
}
