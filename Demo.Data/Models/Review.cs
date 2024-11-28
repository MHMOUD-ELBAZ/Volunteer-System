using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo.Data.Models;

[Table("Review")]
public partial class Review
{
    [Key]
    public int Id { get; set; }

    public int? ApplicationId { get; set; }

    [StringLength(450)]
    public string? VolunteerId { get; set; }

    [StringLength(450)]
    public string? OrganizationId { get; set; }

    [Range(0,5)]
    public int Rating { get; set; } = 0;

    [Column(TypeName = "datetime")]
    public DateTime DateReviewed { get; set; } = DateTime.Now;

    [StringLength(256)]
    public string? Comment { get; set; }

    [ForeignKey(nameof(ApplicationId))]
    [InverseProperty(nameof(Application.Reviews))]
    public virtual Application? Application { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    [InverseProperty(nameof(Organization.Reviews))]
    public virtual Organization? Organization { get; set; }

    [ForeignKey(nameof(VolunteerId))]
    [InverseProperty(nameof(Volunteer.Reviews))]
    public virtual Volunteer? Volunteer { get; set; }
}