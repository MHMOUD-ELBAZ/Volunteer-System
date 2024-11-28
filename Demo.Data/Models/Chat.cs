
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Demo.Data.Models;

[Table("Chat")]
public partial class Chat
{
    [Key]
    public int Id { get; set; }

    [StringLength(450)]
    public string? Talker1 { get; set; }

    [StringLength(450)]
    public string? Talker2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [InverseProperty(nameof(Message.Chat))]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [ForeignKey(nameof(Talker1))]
    public virtual ApplicationUser User1 { get; set; }    
    
    [ForeignKey(nameof(Talker2))]
    public virtual ApplicationUser User2 { get; set; }
}