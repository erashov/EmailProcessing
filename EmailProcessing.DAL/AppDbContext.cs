using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EmailProcessing.DAL.Entities;

namespace EmailProcessing.DAL
{
    public class AppDbContext : IdentityDbContext<UserEntity>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Setting> Serrings { get; set; }
        public DbSet<ParamSetting> ParamSettings { get; set; }
        public DbSet<ParamType> ParamTypes { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Insure Identity Entities are accounted for.
            base.OnModelCreating(modelBuilder);
        }

    }
}
