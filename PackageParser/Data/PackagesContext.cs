using Microsoft.EntityFrameworkCore;

namespace PackageParser.Data
{
    public class PackagesContext : DbContext
    {
        public PackagesContext(DbContextOptions<PackagesContext> options)
            : base(options)
        {
        }

        public DbSet<PackagesConfig> PackagesConfigs { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}