using Demo.Business.DTOs.Opportunity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Skill;

public class SkillWithOpportunitiesDto : SkillDto
{
    public List<OpportunityDto> Opportunities { get; set; } = new List<OpportunityDto>();
}
