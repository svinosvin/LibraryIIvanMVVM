using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Worker> Workers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set;}
        public DbSet<HistoryTransactions> Transactions { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Rating> Ratings { get; set; }



#pragma warning disable CS8618 
        public ApplicationDbContext(DbContextOptions options) : base(options) => Database.EnsureCreated();
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>().HasMany(x => x.Favourites).WithMany(x=>x.Users);
            modelBuilder.Entity<User>().HasMany(x => x.Histories).WithOne(x => x.User);
            modelBuilder.Entity<User>().HasOne(x => x.Person);
            #endregion

            #region Worker
            modelBuilder.Entity<Worker>().HasOne(x => x.Person);
            #endregion

            #region Book
            modelBuilder.Entity<Book>().HasMany(x => x.Reviews).WithMany(x => x.Books);
            modelBuilder.Entity<Book>().HasMany(x => x.Ratings).WithMany(x => x.Books);
            #endregion

            #region Author
            modelBuilder.Entity<Author>().HasMany(x => x.Books).WithOne(x => x.Author);
            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
