using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo.Data.Models;

[Table("Review")]
public partial class Review
{
    [Key]
    public int Id { get; set; }

    public int ApplicationId { get; set; }

    [Range(0,5)]
    public int Rating { get; set; } = 0;

    [Column(TypeName = "datetime")]
    public DateTime DateReviewed { get; set; } = DateTime.Now;

    [StringLength(256)]
    public string? Comment { get; set; }

    [ForeignKey(nameof(ApplicationId))]
    [InverseProperty(nameof(Application.Reviews))]
    public virtual Application? Application { get; set; }
}