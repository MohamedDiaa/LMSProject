using Microsoft.EntityFrameworkCore;
using LMS.api.Model;
using LMS.api.Data;

public class LMSContext : DbContext
{
    public LMSContext(DbContextOptions<LMSContext> options)
        : base(options)
    {
    }

    public DbSet<Activity> Activity => Set<Activity>();
    public DbSet<Course> Course => Set<Course>();
    public DbSet<Module> Module => Set<Module>();
    public DbSet<Role> Role => Set<Role>();
    public DbSet<Student> Student => Set<Student>();
    public DbSet<Teacher> Teacher => Set<Teacher>();
    public DbSet<User> User => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CourseConfiguration());

        modelBuilder.Entity<Activity>().HasOne(a => a.Module)
            .WithMany(m => m.Activities)
            .HasForeignKey(a => a.ModuleID);
    }
}
