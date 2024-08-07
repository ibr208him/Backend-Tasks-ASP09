using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = LENOVO; database= ASP9-Task2; trusted_connection = true;TrustServerCertificate=true");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(o => o.CreatedAt).HasComputedColumnSql("GETDATE()");
        }
    }
}
