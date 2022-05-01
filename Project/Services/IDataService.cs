using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IDataService<T> where T : DefaultClass
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(int id);
        public Task<T> Create(T entity);
        public Task<T> Update(int id, T entity);
        public Task<bool> Delete(int id);

    }
}
