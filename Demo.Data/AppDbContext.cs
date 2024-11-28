using Demo.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;



namespace Demo.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {        
        public AppDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder) { }

        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Opportunity> Opportunities { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<VolunteerSkill> VolunteersSkills { get; set; }
        public virtual DbSet<OpportunitySkill> OpportunitiesSkills { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<Volunteer>(entity =>
            {
                entity.ToTable("Volunteer");
            });
        }
    }
}
