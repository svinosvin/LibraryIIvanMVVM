using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface IAccountDataService : IDataService<User>  
    {
        Task<User> GetByUsername(string username);

    }
}
