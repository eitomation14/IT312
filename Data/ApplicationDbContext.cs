using Instructor_Information_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Instructor_Information_System.Data;

public class ApplicationDbContext : IdentityUserContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Instructor>()
            .Property(instructor => instructor.InstructorID)
            .HasColumnName("Id")
            .HasColumnType("TEXT");

        modelBuilder.Entity<ApplicationUser>()
            .Property(user => user.FullName)
            .HasMaxLength(50)
            .IsRequired();
    }
}
