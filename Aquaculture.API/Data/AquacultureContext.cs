using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquaculture.API.Data
{
    public class AquacultureContext : DbContext
    {
        public AquacultureContext(DbContextOptions<AquacultureContext> options) : base(options)
        {
        }

        public DbSet<Farm> Farms { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>().Property(p => p.GpsPosition).HasColumnType("decimal(19,4)");
        }
    }
}
