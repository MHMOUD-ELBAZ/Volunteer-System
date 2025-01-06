using Demo.Business.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Mappers;

public class SkillMapper
{
    public static SkillDto MapToSkillDto(Skill skill)
    {
        return new SkillDto { Name = skill.Name??"", Id = skill.Id, Description = skill.Descritption };
    }

    public static SkillWithOpportunitiesDto MapToSkillWithOpportunitiesDto(Skill skill)
    {
        return new SkillWithOpportunitiesDto
        {
            Id = skill.Id,
            Name = skill.Name?? "",
            Description = skill.Descritption,
            Opportunities = skill.OpportunitySkills.Select(os => OpportunityMapper.MapToOpportunityDto(os.Opportunity)).ToList()
        };
    }

    public static SkillWithVolunteersDto MapToSkillWithVolunteersDto(Skill skill)
    {
        return new SkillWithVolunteersDto
        {
            Id = skill.Id,
            Name = skill.Name,
            Description = skill.Descritption,
            Volunteers = skill.VolunteerSkills.Select(vs => VolunteerMapper.MapToVolunteerDto(vs.Volunteer)).ToList()
        };
    }

    public static Skill MapToSkill(CreateSkillDto skillDto)
    {
        return new Skill
        {
            Name = skillDto.Name,
            Descritption = skillDto.Descritption
        };
    }
}

