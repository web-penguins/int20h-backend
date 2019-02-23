using Host.Models;
using Microsoft.EntityFrameworkCore;

namespace Host.Database
{
    public sealed class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().Property(p => p.ProductId).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Credential>().Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}