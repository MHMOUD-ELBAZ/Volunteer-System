using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo.Data.Models;

[Table("Opportunity")]
public partial class Opportunity
{
    [Key]
    public int Id { get; set; }

    [StringLength(450)]
    public string? OrganizationId { get; set; }

    public string Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? DatePosted { get; set; } = DateTime.Now; 

    public bool? IsOnline { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Deadline { get; set; }

    [InverseProperty(nameof(Application.Opportunity))]
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    [InverseProperty(nameof(OpportunitySkill.Opportunity))]
    public virtual ICollection<OpportunitySkill> OpportunitySkills { get; set; } = new List<OpportunitySkill>();

    [ForeignKey(nameof(OrganizationId))]
    [InverseProperty(nameof(Organization.Opportunities))]
    public virtual Organization? Organization { get; set; }
}