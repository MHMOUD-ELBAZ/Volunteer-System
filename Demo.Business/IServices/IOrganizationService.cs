global using Demo.Business.DTOs.Organization;


namespace Demo.Business.IServices;

public interface IOrganizationService
{
    void Add(RegisterOrganizationDto organization,string id);

    OrganizationDetailsDto? GetOrganizationById(string id);
    OrganizationWithApplicationsDto? GetOrganizationWithApplications(string id);
    OrganizationWithOpportunitiesDto? GetOrganizationWithOpportunities(string id);
    IEnumerable<OrganizationDto> GetAllOrganizations();
    OrganizationDto? UpdateOrganization(string id, UpdateOrganizationDto organizationDto);
    bool DeleteOrganization(string id);
}



