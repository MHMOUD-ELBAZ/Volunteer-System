using Demo.Business.DTOs.Opportunity;

namespace Demo.Business.Services.Interfaces
{
    public interface IOpportunityService
    {
        OpportunityDto? GetById(int id);
        OpportunityWithOrganizationDto? GetOpportunityWithOrganization(int id);
        OpportunityWithApplicationsDto? GetOpportunityWithApplications(int id);
        IEnumerable<OpportunityDto> GetAllOpportunities();
        OpportunityDto Create(CreateOpportunityDto opportunityDto);
        OpportunityDto? Update(int id, CreateOpportunityDto opportunityDto);
        bool Delete(int id);
    }
}
