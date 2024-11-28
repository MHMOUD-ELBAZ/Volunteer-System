
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Models;

[PrimaryKey("VolunteerId", "SkillId")]
[Table("VolunteerSkill")]
public partial class VolunteerSkill
{
    [Key]
    public int SkillId { get; set; }

    [Key]
    public string VolunteerId { get; set; } = null!;

    [ForeignKey("SkillId")]
    [InverseProperty("VolunteerSkills")]
    public virtual Skill Skill { get; set; } = null!;

    [ForeignKey("VolunteerId")]
    [InverseProperty("VolunteerSkills")]
    public virtual Volunteer Volunteer { get; set; } = null!;
}