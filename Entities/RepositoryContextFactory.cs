using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    //public class BloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
    //{
    //    public BloggingContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
    //        optionsBuilder.UseSqlite("Data Source=blog.db");

    //        return new BloggingContext(optionsBuilder.Options);
    //    }
    //}


    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("server=.; database=UltimateMovieApp; Integrated Security = true");

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
