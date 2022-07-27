using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        }

        public DbSet<Company>? Companies { get; set; }

        public DbSet<Employee>? Employees { get; set; }

        public DbSet<Movie>? Movies { get; set; }
    }
}
