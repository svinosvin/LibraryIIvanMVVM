using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Generic
{
    internal class BookDataService : IDataService<Book>
    {
        private readonly AppDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Book> _nonQueryDataService;

        public BookDataService(AppDbContextFactory contextFactory, NonQueryDataService<Book> nonQueryDataService)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = nonQueryDataService;
        }

        public async Task<Book> Create(Book entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Book> Get(int id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {

                Book entity = await context.Books.Include(x=>x.Author).Include(x=>x.Reviews).FirstOrDefaultAsync(x => x.Id == id); 
                return entity;
            }
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
               
                IEnumerable<Book> entity = await context.Books.Include(x => x.Author)?.Include(x => x.Reviews).ToListAsync();
                return entity;
            }
        }

        public async Task<Book> Update(int id, Book entity)
        {
            return await _nonQueryDataService.Update(id,entity);
        }
    }
}
