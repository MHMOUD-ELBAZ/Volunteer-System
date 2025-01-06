using Demo.Business.Mappers;
using Demo.Business.Exceptions;

namespace Demo.Business.Services;


public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public ApplicationDto? GetApplicationById(int id)
    {
        var application = _applicationRepository.GetApplication(id);
        return application != null ? ApplicationMapper.MapToApplicationDto(application) : null;
    }

    public ApplicationWithReviewsDto? GetApplicationWithReviews(int id)
    {
        var application = _applicationRepository.GetApplicationWithReviews(id);
        return application != null ? ApplicationMapper.MapToApplicationWithReviewsDto(application) : null;
    }


    public ApplicationWithOpportunityAndVolunteerDto? GetWithOpportunityAndVolunteer(int id)
    {
        var application = _applicationRepository.GetWithOpportunityAndVolunteer(id);
        return application != null ? ApplicationMapper.MapToApplicationWithOpportunityAndVolunteerDto(application) : null;
    }

    public ApplicationDto CreateApplication(string volunteerId, CreateApplicationDto createApplicationDto)
    {
        var application = ApplicationMapper.MapToApplication(volunteerId,createApplicationDto);
        application.Status = ApplicationStatus.Pending;
        _applicationRepository.Add(application);
        _applicationRepository.Save();

        return ApplicationMapper.MapToApplicationDto(application);
    }

    public ApplicationDto? UpdateApplication(UpdateApplicationDto updateApplicationDto, string organizationId)
    {
        var application = _applicationRepository.GetWithOpportunity(updateApplicationDto.Id);
        
        if (application == null) return null;
        if(application?.Opportunity?.OrganizationId != organizationId)
            throw new ForbiddenAccessException("You are not authorized to update this application, only opportunity poster can do.");

        
        application.Status = Enum.Parse<ApplicationStatus>(updateApplicationDto.Status,true);
        _applicationRepository.Update(application);
        _applicationRepository.Save();

        return ApplicationMapper.MapToApplicationDto(application);
    }

    public bool DeleteApplication(int applicationId, string volunteerId)
    {
        var application = _applicationRepository.GetApplication(applicationId);

        if (application is null)
            throw new NotFoundException($"Application with ID {applicationId} not found.");

        if (application.VolunteerId != volunteerId)
            throw new ForbiddenAccessException("You are not authorized to delete this application.");

        _applicationRepository.Delete(application);
        _applicationRepository.Save();

        return true;
    }

    public ApplicationWithOpportunity? GetWithOpportunity(int id)
    {
        var application = _applicationRepository.GetWithOpportunity(id);
        return application != null ? ApplicationMapper.MapToApplicationWithOpportunityDto(application) : null;
    }

    public IEnumerable<ApplicationDto> GetApplicationsForVolunteer(string volunteerId)
    {
        return _applicationRepository.GetApplicationsForVolunteer(volunteerId).Select(ApplicationMapper.MapToApplicationDto);
    }

    public IEnumerable<ApplicationDto> GetApplicationsForOrganization(string organizationId)
    {
        return _applicationRepository.GetApplicationsForOrganization(organizationId).Select(ApplicationMapper.MapToApplicationDto);
    }
}

