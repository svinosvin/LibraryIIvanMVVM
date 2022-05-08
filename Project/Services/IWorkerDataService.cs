using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IWorkerDataService : IDataService<Worker>
    {
        Task<Worker> GetByUsername(string username);
        Task<Worker> GetByEmail(string email);

    }
}
