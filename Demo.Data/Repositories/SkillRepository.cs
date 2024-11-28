using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repositories;

public class SkillRepository : Repository<Skill>, ISkillRepository
{
    public SkillRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Skill? GetSkill(int id)
    {
        return _dbContext.Skills.Find(id);
    }

    public Skill? GetSkillWithOpportunities(int id)
    {
        return _dbContext.Skills.Include(s => s.OpportunitySkills).FirstOrDefault(s => s.Id == id);
    }

    public Skill? GetSkillWithVolunteers(int id)
    {
        return _dbContext.Skills.Include(s => s.VolunteerSkills).FirstOrDefault(s => s.Id == id);
    }
}
