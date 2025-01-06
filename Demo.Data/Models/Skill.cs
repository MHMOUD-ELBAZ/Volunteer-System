using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Models;

[Table("Skill")]
[Index("Name", Name = "UQ__Skill__737584F6A761AFFF", IsUnique = true)]
public partial class Skill
{
    [Key]
    public int Id { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string Name { get; set; }

    public string Descritption { get; set; }

    [InverseProperty(nameof(OpportunitySkill.Skill))]
    public virtual ICollection<OpportunitySkill> OpportunitySkills { get; set; } = new List<OpportunitySkill>();

    [InverseProperty(nameof(VolunteerSkill.Skill))]
    public virtual ICollection<VolunteerSkill> VolunteerSkills { get; set; } = new List<VolunteerSkill>();
}