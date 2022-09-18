using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Yatt.Models.Entities;

namespace Yatt.Repo.Data
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("YattDbConnection"),
                 b => b.MigrationsAssembly("Yatt.Api"));//services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        // JOBS
        public DbSet<Vacancy> Vacancies { get; set; } = null!;
        public DbSet<Job> Jobs { get; set; } = null!;
        public DbSet<JobDescription> JobDescriptions { get; set; } = null!;
        public DbSet<JobQualification> JobQualifications { get; set; } = null!;
        public DbSet<JobEducation> JobEducations { get; set; } = null!;
        public DbSet<JobDuty> JobDuties { get; set; } = null!;

        // JOB APPLICATION
        public DbSet<JobApplication> JobApplications { get; set; } = null!; 

    }
}
