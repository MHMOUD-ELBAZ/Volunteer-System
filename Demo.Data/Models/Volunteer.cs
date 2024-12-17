
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo.Data.Models;

[Table("Volunteer")]
public class Volunteer
{
    [Key]
    public string VolunteerId { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Bio { get; set; }

    public int Age { get; set; }

    public double? Rating { get; set; } = 0;

    [ForeignKey(nameof(VolunteerId))]
    public virtual ApplicationUser User { get; set; } = null!; 

    [InverseProperty(nameof(Application.Volunteer))]
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();


    [InverseProperty(nameof(VolunteerSkill.Volunteer))]
    public virtual ICollection<VolunteerSkill> VolunteerSkills { get; set; } = new List<VolunteerSkill>();
}