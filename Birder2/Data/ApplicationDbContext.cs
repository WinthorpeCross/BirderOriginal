using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Birder2.Models;


namespace Birder2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Observation> Observations { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public DbSet<ConserverationStatus> ConservationStatuses { get; set; }
        public DbSet<BritishStatus> BritishStatuses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ObservationTag> ObservationTags { get; set; }

        public DbSet<SalesOrder> SalesOrders { get; set; }
        



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<SalesOrder>().ToTable("SalesOrder");

            builder.Entity<Observation>().ToTable("Observation");
            builder.Entity<Bird>().ToTable("Bird");
            builder.Entity<ConserverationStatus>().ToTable("ConservationStatus");
            builder.Entity<BritishStatus>().ToTable("BritishStatus");
            builder.Entity<Tag>().ToTable("Tag");
            builder.Entity<ObservationTag>().ToTable("ObservationTag");

            builder.Entity<ObservationTag>()
                    .HasKey(ot => new { ot.TagId, ot.ObervationId });

            builder.Entity<ObservationTag>()
                    .HasOne(ot => ot.Observation)
                    .WithMany(o => o.ObservationTags)
                    .HasForeignKey(ot => ot.ObervationId);

            builder.Entity<ObservationTag>()
                    .HasOne(ot => ot.Tag)
                    .WithMany(t => t.ObservationTags)
                    .HasForeignKey(ot => ot.TagId);

            


        }
    }
}
