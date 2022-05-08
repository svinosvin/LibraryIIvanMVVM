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
    public class WorkerDataService : IWorkerDataService
    {
        private readonly AppDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Worker> _nonQueryDataService;

        public WorkerDataService(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Worker>(contextFactory);
        }
        public async Task<Worker> Create(Worker entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Worker> Get(int id)
        {

            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Worker entity = await context.Workers.Include(x=>x.Person).FirstOrDefaultAsync(x => x.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Worker>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Worker> entity = await context.Workers.Include(x => x.Person).ToListAsync();
                return entity;
            }
        }

        public async Task<Worker> GetByEmail(string email)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Worker user = await context.Workers.Include(x => x.Person).FirstOrDefaultAsync(x => x.Person.Email == email);
                return user;
            }
        }

        public async Task<Worker> GetByUsername(string username)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Worker user = await context.Workers.Include(x => x.Person).FirstOrDefaultAsync(x => x.Login == username);
                return user;
            }
        }
     
        public async Task<Worker> Update(int id, Worker entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
