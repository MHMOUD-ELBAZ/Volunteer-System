using Demo.Business.Mappers;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    public IEnumerable<ApplicationDto> GetAllApplications()
    {
        var applications = _applicationRepository.GetAll();
        return applications.Select(ApplicationMapper.MapToApplicationDto);
    }


    public ApplicationDto CreateApplication(CreateApplicationDto createApplicationDto)
    {
        var application = ApplicationMapper.MapToApplication(createApplicationDto);
        application.Status = ApplicationStatus.Pending;
        _applicationRepository.Add(application);
        _applicationRepository.Save();

        return ApplicationMapper.MapToApplicationDto(application);
    }

    public ApplicationDto? UpdateApplication(int id,UpdateApplicationDto updateApplicationDto)
    {
        var application = _applicationRepository.GetApplication(id);
        if (application == null) return null;

        application.Status = Enum.Parse<ApplicationStatus>(updateApplicationDto.Status,true);

        _applicationRepository.Update(application);
        _applicationRepository.Save();

        return ApplicationMapper.MapToApplicationDto(application);
    }

    public bool DeleteApplication(int id)
    {
        var application = _applicationRepository.GetApplication(id);
        if (application == null) return false;

        _applicationRepository.Delete(application);
        _applicationRepository.Save();

        return true;
    }
}

