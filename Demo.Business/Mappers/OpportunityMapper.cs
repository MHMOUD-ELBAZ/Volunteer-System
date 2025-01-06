using Demo.Business.DTOs.Opportunity;
using Demo.Business.DTOs.Skill;

namespace Demo.Business.Mappers;

public class OpportunityMapper
{
    public static OpportunityDto MapToOpportunityDto(Opportunity opportunity)
    {
        var opp = new OpportunityDto
        {
            Id = opportunity.Id,
            Description = opportunity.Description,
            Deadline = opportunity.Deadline,
            DatePosted = opportunity.DatePosted,
            IsOnline = opportunity.IsOnline,
            OrganizationId = opportunity.OrganizationId
        };

        if(opportunity?.OpportunitySkills != null)
        {
            opp.Skills = new List<SkillDto>(); 
            foreach(var os in opportunity.OpportunitySkills)
            {
                if (os.Skill != null)
                    opp.Skills.Add(SkillMapper.MapToSkillDto(os.Skill));
            }               
        }
        return opp;
    }    

    public static OpportunityWithOrganizationDto MapToOpportunityWithOrganizationDto(Opportunity opportunity)
    {
        var result = new OpportunityWithOrganizationDto() 
        {
            Id = opportunity.Id,
            Description = opportunity.Description,
            IsOnline = opportunity.IsOnline,
            Deadline = opportunity.Deadline,
            DatePosted = opportunity.DatePosted,
            OrganizationId = opportunity.OrganizationId,
            Organization = OrganizationMapper.MapToOrganizationDetailsDto(opportunity.Organization)
        };

        if (opportunity?.OpportunitySkills != null)
        {
            result.Skills = new List<SkillDto>();
            foreach (var os in opportunity.OpportunitySkills)
            {
                if (os.Skill != null)
                    result.Skills.Add(SkillMapper.MapToSkillDto(os.Skill));
            }
        }

        return result;
    }

    public static OpportunityWithApplicationsDto MapToOpportunityWithApplicationsDto(Opportunity opportunity)
    {
        return new OpportunityWithApplicationsDto
        {
            Id = opportunity.Id,
            Description = opportunity.Description,
            IsOnline = opportunity.IsOnline,
            Deadline = opportunity.Deadline,
            DatePosted = opportunity.DatePosted,
            OrganizationId = opportunity.OrganizationId,
            Applications = opportunity.Applications.Select(ApplicationMapper.MapToApplicationDto).ToList()
        };
    }

    public static Opportunity MapToOpportunity(CreateOpportunityDto dto, string organizationId)
    {
        return new Opportunity
        {
            OrganizationId = organizationId,
            Description = dto.Description,
            IsOnline = dto.IsOnline,
            Deadline = dto.Deadline
        };
    }    
    
    public static Opportunity MapToOpportunity(OpportunityDto dto)
    {
        return new Opportunity
        {
            Id = dto.Id,
            OrganizationId = dto.OrganizationId,
            Description = dto.Description,
            IsOnline = dto.IsOnline,
            Deadline = dto.Deadline,
            DatePosted = dto.DatePosted
        };
    }
}
