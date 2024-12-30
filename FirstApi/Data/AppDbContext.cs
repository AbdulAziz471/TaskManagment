using Microsoft.EntityFrameworkCore;
using FirstApi.Models;

namespace FirstApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        //public DbSet<Issue> Issues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many: Project -> Issues
            //modelBuilder.Entity<Project>()
            //    .HasMany(p => p.Issues)
            //    .WithOne(i => i.Project)
            //    .HasForeignKey(i => i.ProjectId);

            // One-to-Many: Team -> Users
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(u => u.Team)
                .HasForeignKey(u => u.TeamId)
                .OnDelete(DeleteBehavior.SetNull);


            //modelBuilder.Entity<Issue>()
            //        .HasOne(i => i.CreatedByUser) // Navigation property in Issue
            //        .WithMany(u => u.CreatedIssues) // Assuming User has a collection of Issues
            //        .HasForeignKey(i => i.CreatedBy) // ForeignKey property in Issue
            //        .OnDelete(DeleteBehavior.Restrict);



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
            modelBuilder.Entity<Query>()
              .HasOne(q => q.Status)
              .WithMany()
              .HasForeignKey(q => q.StatusId);

            modelBuilder.Entity<Query>()
                .HasOne(q => q.Priority)
                .WithMany()
                .HasForeignKey(q => q.PriorityId);

            modelBuilder.Entity<Query>()
                .HasOne(q => q.CreatedBy)
                .WithMany(u => u.Queries) // Assuming User has a collection of Queries
                .HasForeignKey(q => q.CreatedById)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of user if queries exist

            modelBuilder.Entity<Query>()
                .HasOne(q => q.Project)
                .WithMany(p => p.Queries) // Assuming Project has a collection of Queries
                .HasForeignKey(q => q.ProjectId);
       //     modelBuilder.Entity<Query>()
       //.HasOne(q => q.CreatedBy)
       //.WithMany(u => u.CreatedQueries) // Ensure User model has a CreatedQueries collection
       //.HasForeignKey(q => q.CreatedById)
       //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Query>()
                .HasOne(q => q.AssignedUser)
                .WithMany(u => u.AssignedQueries) // Ensure User model has an AssignedQueries collection
                .HasForeignKey(q => q.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = 1, Title = "High" },
                new Priority { Id = 2, Title = "Medium" },
                new Priority { Id = 3, Title = "Low" }
               );
            // Seed data for Status
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Title = "Open" },
                new Status { Id = 2, Title = "InProgress" },
                new Status { Id = 3, Title = "Completed" }
    );

    // Seed data for Role
    modelBuilder.Entity<Role>().HasData(
              new Role { Id = 1, Title = "admin" },
            new Role { Id = 2, Title = "Developer" },
            new Role { Id = 3, Title = "Project Manager" },
        new Role { Id = 4, Title = "QA Analyst" }
    );


        }
    }
}
