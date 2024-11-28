using Demo.Business.DTOs.Skill;
using Demo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Mappers;

public class VolunteerMapper
{
    public static Volunteer MapToVolunteer(RegisterVolunteerDto dto, string id)
    {
        return new Volunteer
        {
            VolunteerId = id,
            Address = dto.Address,
            Bio = dto.Bio,
            Age = dto.Age
        };
    }
    
    public static VolunteerDto MapToVolunteerDto(Volunteer volunteer)
    {
        var result = new VolunteerDto
        {
            VolunteerId = volunteer.VolunteerId,
            Address = volunteer.Address,
            Bio = volunteer.Bio,
            Age = volunteer.Age,
            Rating = volunteer.Rating,
            Name = volunteer.User?.UserName?? string.Empty,
            Phone = volunteer.User?.PhoneNumber?? string.Empty,
            Email = volunteer.User?.Email?? string.Empty,
        };

        if (volunteer.User?.Photo != null)
            result.Photo = $"Photos/Volunteer/{volunteer.User.Photo}"; 

        if(volunteer.VolunteerSkills != null)
        {
            foreach (var vs in volunteer.VolunteerSkills)
                if (vs.Skill != null)
                    result.Skills.Add(SkillMapper.MapToSkillDto(vs.Skill));
        }

        return result;
    }
      
    public static VolunteerWithApplicationsDto MapToVolunteerWithAppDto(Volunteer volunteer)
    {
        ICollection<ApplicationDto> applicationDtos = new List<ApplicationDto>();

        foreach (var app in volunteer.Applications)
        {
            ApplicationDto applicationDto = new ApplicationDto()
            {
                Id = app.Id,
                DateSent = app.DateSent,
                Status = app.Status.ToString(),
                OrganizationId = app.OrganizationId,
                OpportunityId = app.OpportunityId,
                VolunteerId = app.VolunteerId,
            };
            applicationDtos.Add(applicationDto);
        }

        VolunteerWithApplicationsDto result = new VolunteerWithApplicationsDto()
        {
            VolunteerId = volunteer.VolunteerId,
            Address = volunteer.Address,
            Age = volunteer.Age,
            Bio = volunteer.Bio,
            Rating = volunteer.Rating,
            Applications = applicationDtos
        };

        return result;
    }
}
