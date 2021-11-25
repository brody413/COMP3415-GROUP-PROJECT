using LakeshoreHotelApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LakeshoreHotelApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //Define models so our contollers can use them
        //public DbSet<ModelName> ModelNames { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Room> rooms { get; set; }

        //override model creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Room>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rooms);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
