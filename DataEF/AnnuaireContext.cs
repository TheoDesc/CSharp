using Microsoft.EntityFrameworkCore;

namespace DataEF
{
    public class AnnuaireContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Salarie> Salaries { get; set; }


        public AnnuaireContext(DbContextOptions<AnnuaireContext> options)
            : base(options)
        {
        }
    }
}