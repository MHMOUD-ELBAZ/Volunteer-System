using Demo.Data.Models;


namespace Demo.Data.IRepositories;

public interface IOpportunitySkillRepository : IRepository<OpportunitySkill>
{
    public OpportunitySkill? Get(string opportunityId, int skillId);

    public IEnumerable<OpportunitySkill> GetSkillsForOpportunity(int opportunityId);
    public IEnumerable<OpportunitySkill> GetOpportunitiesWithSkill(int skillId);

    public void UpdateOpportunitySkills(Opportunity opportunity, ICollection<int>? skillIds);

    public void DeleteOpportunitySkill(int opportunityId);
}
