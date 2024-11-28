using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;



namespace Demo.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Photo { get; set; }

        [Column(TypeName = "varchar(16)")]
        public UserType UserType { get; set; }


    }
}
