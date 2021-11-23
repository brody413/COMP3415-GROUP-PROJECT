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
                .HasOne(p => p.Customers)
                .WithMany(c => c.Rooms)
                .HasForeignKey(p => p.RoomNumber)
                .HasConstraintName("FK_Rooms_RoomNumber");

/*            builder.Entity<Customer>()
                .HasOne(p => p.Rooms)
                .WithMany(c => c.Customers)
                .HasForeignKey(p => p.Id)
                .HasConstraintName("FK_Rooms_RoomNumber");*/
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
