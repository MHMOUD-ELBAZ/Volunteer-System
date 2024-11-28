using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Business.DTOs.Skill;

namespace Demo.Business.DTOs.Opportunity;

public class OpportunityDto
{
    public int Id { get; set; }

    public string? OrganizationId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime? DatePosted { get; set; } 

    public bool? IsOnline { get; set; }

    public DateTime? Deadline { get; set; }

    public ICollection<SkillDto> Skills { get; set; }  
}
