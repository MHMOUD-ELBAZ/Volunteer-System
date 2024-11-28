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
            OrganizationId = application.OrganizationId,
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
            OrganizationId = application.OrganizationId,
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
            OrganizationId = application.OrganizationId,
            Status = application.Status.ToString(),
            DateSent = application.DateSent
        };
    }

    public static Application MapToApplication(CreateApplicationDto dto)
    {
        return new Application
        {
            OpportunityId = dto.OpportunityId,
            VolunteerId = dto.VolunteerId,
            OrganizationId = dto.OrganizationId
        };
    }

}

