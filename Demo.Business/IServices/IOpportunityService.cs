using Demo.Business.DTOs.Opportunity;

namespace Demo.Business.IServices;

public interface IOpportunityService
{
    OpportunityDto? GetById(int id);
    OpportunityWithOrganizationDto? GetOpportunityWithOrganization(int id);
    OpportunityWithApplicationsDto? GetOpportunityWithApplications(int id);
    IEnumerable<OpportunityWithOrganizationDto> GetAllOpportunities();
    OpportunityDto Create(CreateOpportunityDto opportunityDto, string organizationId);
    OpportunityDto? Update(int id, CreateOpportunityDto updatedOpportunityDto, string organizationId);
    bool Delete(int id, string organizationId);
}
