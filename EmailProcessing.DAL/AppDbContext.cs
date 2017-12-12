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
        public DbSet<TypePram> TypeParams { get; set; }
        public DbSet<TypeRequest> RepeRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Insure Identity Entities are accounted for.
            base.OnModelCreating(modelBuilder);
        }
    }
}
