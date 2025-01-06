namespace Demo.Business.IServices;

public interface IApplicationService
{
    ApplicationDto? GetApplicationById(int id);
    ApplicationWithReviewsDto? GetApplicationWithReviews(int id);
    ApplicationWithOpportunityAndVolunteerDto? GetWithOpportunityAndVolunteer(int id);
    ApplicationWithOpportunity? GetWithOpportunity(int id);
    IEnumerable<ApplicationDto> GetApplicationsForVolunteer(string volunteerId);
    IEnumerable<ApplicationDto> GetApplicationsForOrganization(string organizationId);
    ApplicationDto CreateApplication(string volunteerId,CreateApplicationDto createApplicationDto);
    ApplicationDto? UpdateApplication(UpdateApplicationDto updateApplicationDto, string organizationId);
    bool DeleteApplication(int applicationId, string volunteerId);
}

