using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.DataContext
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext() { }
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("TrongConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(entity => entity.Accounts)
                .WithOne(entity => entity.Role)
                .HasForeignKey(entity => entity.RoleId)
                .IsRequired(false);

            modelBuilder.Entity<Account>()
                .HasOne(entity => entity.Profile)
                .WithOne(entity => entity.Account)
                .HasForeignKey<Profile>(entity => entity.AccountId)
                .IsRequired(false);

            modelBuilder.Entity<Major>()
                .HasOne(entity => entity.Profile)
                .WithOne(entity => entity.Major)
                .HasForeignKey<Profile>(entity => entity.MajorId)
                .IsRequired(false);
        }
    }
}
