using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.api.Model;
using LMS.api.Data;

public class LMSContext : DbContext
{
    public LMSContext(DbContextOptions<LMSContext> options)
        : base(options)
    {
    }

    public DbSet<Activity> Activitys { get; set; }
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<Module> Modules { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<Teacher> Teachers { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CourseConfiguration());

        modelBuilder.Entity<Activity>().HasOne(a => a.Module)
            .WithMany(m => m.Activities)
            .HasForeignKey(a => a.ModuleID);
    }

}
