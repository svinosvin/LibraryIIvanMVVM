using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlite(@"Data Source=Library.db");
            return new ApplicationDbContext(options.Options);
        }
    }
}
