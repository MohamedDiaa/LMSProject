using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.api.Model;

    public class LMSContext : DbContext
    {
        public LMSContext (DbContextOptions<LMSContext> options)
            : base(options)
        {
        }

        public DbSet<LMS.api.Model.Course> Courses { get; set; } = default!;
    }
