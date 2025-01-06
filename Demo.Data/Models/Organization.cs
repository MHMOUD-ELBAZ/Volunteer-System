using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Models;

[Table("Organization")]
public partial class Organization
{
    [Key]
    public string OrganizationId { get; set; } = null!;

    public string? Mission { get; set; }

    [Unicode(false)]
    public string? Website { get; set; }

    public string? MainBranch { get; set; }

    [StringLength(128)]
    public string? BankName { get; set; }

    [Unicode(false)]
    public string? BankAccount { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public virtual ApplicationUser User { get; set; } = null!;

    [InverseProperty(nameof(Opportunity.Organization))]
    public virtual ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();

}