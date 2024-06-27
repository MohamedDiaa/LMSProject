using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.api.Model;
using LMS.api.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class LMSContext : IdentityDbContext
{
    public LMSContext(DbContextOptions<LMSContext> options)
        : base(options)
    {
    }

    public DbSet<LMS.api.Model.Course> Course { get; set; } = default!;

    public DbSet<LMS.api.Model.User> User { get; set; } = default!;

    public DbSet<LMS.api.Model.Module> Module { get; set; } = default!;

    public DbSet<LMS.api.Model.Activity> Activity { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CourseConfiguration());

        modelBuilder.Entity<Activity>().HasOne(a => a.Module)
            .WithMany(m => m.Activities)
            .HasForeignKey(a => a.ModuleID);
    }
}
