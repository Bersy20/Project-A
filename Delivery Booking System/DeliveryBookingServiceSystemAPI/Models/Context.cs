using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryBookingServiceSystemAPI.Models
{
    public class Context:DbContext
    {
        public Context() : base()
        {

        }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Executive> Executives { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
               new Customer()
               {
                   CustomerId = 1000,
                   CustomerName = "Bersy",
                   Password = "1234",
                   CustomerEmail="bersy@gmail.com",
                   Age = 22,
                   Phone = "7598377137",
                   Address = "1/271, BTR Nagar, Sipcot",
                   City = "Hosur",
                   PinCode = "635126",
                   IsVerified = true
               });
            modelBuilder.Entity<Executive>().HasData(
                new Executive()
                {
                    ExecutiveId = 100,
                    ExecutiveName = "Arun",
                    Password = "1234",
                    ExecutiveEmail = "arun@gmail.com",
                    Age = 22,
                    Phone = "9943354155",
                    Address = "No.81, Annai Nagar",
                    City = "Hosur",
                    PinCode = "601206",
                    IsVerified = true,
                    ExecutiveStatus = "Available"
                });
            modelBuilder.Entity<Admin>().HasData(
                new Admin()
                {
                    AdminId = 1,
                    AdminName = "Admin",
                    Password = "Admin",
                    AdminEmail = "admin@gmail.com",
                    Age = 32,
                    Phone = "9443354155",
                    Address = "No.61, Anna Nagar",
                    City = "Chennai",
                    PinCode = "600006",
                   
                });
        }
    }
}
