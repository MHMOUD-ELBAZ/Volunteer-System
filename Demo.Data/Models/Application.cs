
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Models;

[Table("Application")]
public partial class Application
{
    [Key]
    public int Id { get; set; }

    public int? OpportunityId { get; set; }

    [StringLength(450)]
    public string? VolunteerId { get; set; }


    [StringLength(32)]
    [Column(TypeName = "varchar(32)")]
    public ApplicationStatus Status { get; set; } 

    [Column(TypeName = "datetime")]
    public DateTime? DateSent { get; set; } = DateTime.Now; 

    [ForeignKey(nameof(OpportunityId))]
    [InverseProperty(nameof(Opportunity.Applications))]
    public virtual Opportunity? Opportunity { get; set; }

    [InverseProperty(nameof(Review.Application))]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey(nameof(VolunteerId))]
    [InverseProperty(nameof(Volunteer.Applications))]
    public virtual Volunteer? Volunteer { get; set; }
}