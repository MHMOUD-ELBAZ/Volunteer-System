using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Mappers;

public class ApplicationMapper
{
    public static ApplicationDto MapToApplicationDto(Application application)
    {
        return new ApplicationDto
        {
            Id = application.Id,
            OpportunityId = application.OpportunityId,
            VolunteerId = application.VolunteerId,
            Status = application.Status.ToString(),
            DateSent = application.DateSent
        };
    }

    public static ApplicationWithReviewsDto MapToApplicationWithReviewsDto(Application application)
    {
        return new ApplicationWithReviewsDto
        {
            Id = application.Id,
            OpportunityId = application.OpportunityId,
            VolunteerId = application.VolunteerId,
            Status = application.Status.ToString(),
            DateSent = application.DateSent,
            Reviews = application.Reviews.Select(ReviewMapper.MapToReviewDto).ToList()
        };
    }

    public static ApplicationWithOpportunityAndVolunteerDto MapToApplicationWithOpportunityAndVolunteerDto(Application application)
    {
        return new ApplicationWithOpportunityAndVolunteerDto
        {
            Id = application.Id,
            Opportunity = OpportunityMapper.MapToOpportunityDto(application.Opportunity),
            Volunteer = VolunteerMapper.MapToVolunteerDto(application.Volunteer),
            Status = application.Status.ToString(),
            DateSent = application.DateSent
        };
    }

    public static Application MapToApplication(string volunteerId,CreateApplicationDto dto)
    {
        return new Application
        {
            OpportunityId = dto.OpportunityId,
            VolunteerId = volunteerId
        };
    }

    public static ApplicationWithOpportunity? MapToApplicationWithOpportunityDto(Application application)
    {
        return new ApplicationWithOpportunity
        {
            Id = application.Id,
            OpportunityId = application.OpportunityId,
            VolunteerId = application.VolunteerId,
            Status = application.Status.ToString(),
            DateSent = application.DateSent,
            Opportunity = OpportunityMapper.MapToOpportunityDto(application.Opportunity)
        };
    }

    internal static Application MapToApplication(ApplicationDto applicationDto)
    {
        return new Application
        {
            Id = applicationDto.Id,
            OpportunityId = applicationDto.OpportunityId,
            VolunteerId = applicationDto.VolunteerId,
            Status = Enum.Parse<ApplicationStatus>(applicationDto.Status, true),
            DateSent = applicationDto.DateSent
        };
    }
}

