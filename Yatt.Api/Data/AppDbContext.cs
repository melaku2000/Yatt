using Microsoft.EntityFrameworkCore;
using Yatt.Api.Handlers;
using Yatt.Models.Entities;

namespace Yatt.Api.Data
{
    public class AppDbContext : DbContext
    {
        public IConfiguration config { get; }
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
        {
            this.config = config;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList().ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
        }
      
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Domain> Domains { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<Candidate> Candidates { get; set; } = null!;
        public DbSet<UserRole> Roles { get; set; } = null!;
        public DbSet<UserToken> UserTokens { get; set; } = null!;
        public DbSet<UserLanguage> UserLanguages { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<CompanyDetail> CompanyDetails { get; set; } = null!;
        public DbSet<Education> Educations { get; set; } = null!;
        public DbSet<Experiance> Experiances { get; set; } = null!;
        public DbSet<Membership> Memberships { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
    }
}
