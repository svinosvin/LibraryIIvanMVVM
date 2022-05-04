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
    public class AccountDataService : IAccountDataService
    {
        private readonly AppDbContextFactory _contextFactory;
        private readonly NonQueryDataService<User> _nonQueryDataService;
        public AccountDataService(AppDbContextFactory appDbContext)
        {
            _contextFactory = appDbContext;
            _nonQueryDataService = new NonQueryDataService<User>(appDbContext);
        }

        public async Task<User> Create(User entity)
        {


            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<User> Get(int id)
        {

            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                User entity = await context.Users.Include(x=>x.Person).FirstOrDefaultAsync(x => x.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<User> entity = await context.Users.Include(x=>x.Person).ToListAsync();
                return entity;
            }
        }

        public async Task<User> GetByUsername(string username)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                User user = await context.Users.Include(x=>x.Person).FirstOrDefaultAsync(x => x.Login == username);
                return user;
            }
        }

        public async Task<User> Update(int id, User entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
