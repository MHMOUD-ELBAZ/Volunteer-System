using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Opportunity;

public class CreateOpportunityDto
{
    public required string OrganizationId { get; set; }

    public required string Description { get; set; } 

    public bool? IsOnline { get; set; }

    public DateTime? Deadline { get; set; }

    public ICollection<int>? SkillIDs { get; set; }
}
