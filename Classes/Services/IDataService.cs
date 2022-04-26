using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
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
