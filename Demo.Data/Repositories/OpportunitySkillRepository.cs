using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Repositories;

public class OpportunitySkillRepository : Repository<OpportunitySkill>, IOpportunitySkillRepository
{
    public OpportunitySkillRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public OpportunitySkill? Get(string opportunityId, int skillId)
    {
        return _dbContext.OpportunitiesSkills.Find(opportunityId, skillId);
    }

    public IEnumerable<OpportunitySkill> GetOpportunitiesWithSkill(int skillId)
    {
        return _dbContext.OpportunitiesSkills.Include(os => os.Opportunity).Where(os => os.SkillId == skillId);
    }

    public IEnumerable<OpportunitySkill> GetSkillsForOpportunity(int opportunityId)
    {
        return _dbContext.OpportunitiesSkills.Include(os => os.Skill).Where(os => os.OpportunityId == opportunityId);
    }

    public void UpdateOpportunitySkills(Opportunity opportunity, ICollection<int>? skillIds)
    {
        if (opportunity == null)
            return;

        opportunity.OpportunitySkills.Clear();

        if (skillIds != null)
        {
            // Add new OpportunitySkill 
            foreach (var skillId in skillIds)
                opportunity.OpportunitySkills.Add(new OpportunitySkill
                {
                    OpportunityId = opportunity.Id,
                    SkillId = skillId
                });          
        }
    }

    public void DeleteOpportunitySkill(int opportunityId)
    {
        _dbContext.OpportunitiesSkills.FromSqlInterpolated($"DELETE FORM OpportunitySkill WHERE opportunityId = {opportunityId}");
    }
}
