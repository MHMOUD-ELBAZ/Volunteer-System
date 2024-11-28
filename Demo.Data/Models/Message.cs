using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo.Data.Models;

[Table("Message")]
public partial class Message
{
    [Key]
    public int Id { get; set; }

    public int? ChatId { get; set; }

    [StringLength(450)]
    public string? Sender { get; set; }

    [StringLength(450)]
    public string? Receiver { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    public string Content { get; set; } = null!;

    [ForeignKey("ChatId")]
    [InverseProperty("Messages")]
    public virtual Chat? Chat { get; set; }


}