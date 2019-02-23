using Microsoft.EntityFrameworkCore;

namespace Host.Database
{
    public sealed class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
    }
}