using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.api.Model;
using LMS.api.Data;

public class LMSContext : DbContext
    {
        public LMSContext (DbContextOptions<LMSContext> options)
            : base(options)
        {
        }

        public DbSet<LMS.api.Model.Course> Course { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CourseConfiguration());

        modelBuilder.Entity<Activity>().HasOne(a => a.Module)
            .WithMany(m => m.Activities)
            .HasForeignKey(a => a.ModuleID);

        modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title ="Dot net 2024", Description = "bla bla" },
                new Course { Id = 2, Title = "JS", Description = "bla bla" },
                new Course { Id = 3, Title = "Cooking", Description = "bla bla" }
            );
    }

public DbSet<LMS.api.Model.User> User { get; set; } = default!;
}
