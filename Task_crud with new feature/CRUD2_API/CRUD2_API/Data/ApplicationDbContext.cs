using CRUD2_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD2_API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
         .HasIndex(p => p.Name)
         .IsUnique();
        }
        public DbSet<Product> Products {  get; set; }
    }
}
