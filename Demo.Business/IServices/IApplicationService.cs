namespace Demo.Business.IServices;

public interface IApplicationService
{
    ApplicationDto? GetApplicationById(int id);
    ApplicationWithReviewsDto? GetApplicationWithReviews(int id);
    ApplicationWithOpportunityAndVolunteerDto? GetWithOpportunityAndVolunteer(int id);
    IEnumerable<ApplicationDto> GetAllApplications();
    ApplicationDto CreateApplication(CreateApplicationDto createApplicationDto);
    ApplicationDto? UpdateApplication(int id, UpdateApplicationDto updateApplicationDto);
    bool DeleteApplication(int id);
}

