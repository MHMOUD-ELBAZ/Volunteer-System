using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories;

public class VolunteerSkillRepository : Repository<VolunteerSkill>, IVolunteerSkillRepository
{
    public VolunteerSkillRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public VolunteerSkill? Get(string volunteerId, int skillId)
    {
        return _dbContext.VolunteersSkills.Find(volunteerId, skillId);  
    }

    public IEnumerable<VolunteerSkill> GetSkillsForVolunteer(string volunteerId)
    {
        return _dbContext.VolunteersSkills.Include(vs => vs.Skill).Where(vs => vs.VolunteerId == volunteerId);
    }

    public IEnumerable<VolunteerSkill> GetVolunteersWithSkill(int skillId)
    {
        return _dbContext.VolunteersSkills.Include(vs => vs.Volunteer).ThenInclude(v => v.User).Where(vs => vs.SkillId == skillId);
    }
}
