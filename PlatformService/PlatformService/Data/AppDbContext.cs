using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }
//inmem db
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warns => warns.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            base.OnConfiguring(optionsBuilder);
            
        }
        public DbSet<Platform> Platforms{ get; set; }
    }
}
