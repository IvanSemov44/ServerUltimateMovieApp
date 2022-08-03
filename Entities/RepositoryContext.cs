using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Entities.Configuration;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<MovieUser>
    {

        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MovieOwner)
                .WithMany(u => u.Movies)
                .HasForeignKey(m=>m.MovieOwnerId);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Company>? Companies { get; set; }

        public DbSet<Employee>? Employees { get; set; }

        public DbSet<Movie>? Movies { get; set; }
    }
}
