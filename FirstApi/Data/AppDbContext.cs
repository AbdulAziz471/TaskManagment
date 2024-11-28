using Microsoft.EntityFrameworkCore;
using FirstApi.Models;

namespace FirstApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many: Project -> Issues
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Issues)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectId);

            // One-to-Many: Team -> Users
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(u => u.Team)
                .HasForeignKey(u => u.TeamId)
                .OnDelete(DeleteBehavior.SetNull);

            // Many-to-Many: User -> Project
            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserProject",
                    j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.ToTable("UserProject")
                );

            // Many-to-Many: User -> Role
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.ToTable("UserRole") // Name of the join table
                );
        }
    }
}
