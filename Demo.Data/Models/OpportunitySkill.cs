using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Models;

[PrimaryKey("OpportunityId", "SkillId")]
[Table("OpportunitySkill")]
public partial class OpportunitySkill
{
    [Key]
    public int SkillId { get; set; }

    [Key]
    public int OpportunityId { get; set; }

    [ForeignKey(nameof(OpportunityId))]
    [InverseProperty(nameof(Opportunity.OpportunitySkills))]
    public virtual Opportunity Opportunity { get; set; } = null!;

    [ForeignKey(nameof(SkillId))]
    [InverseProperty(nameof(Skill.OpportunitySkills))]
    public virtual Skill Skill { get; set; } = null!;
}