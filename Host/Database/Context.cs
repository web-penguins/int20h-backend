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
    }
}