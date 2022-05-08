using Models.BaseModels;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.AunthenticationService
{
    public interface IWorkerAuthenticationService
    {
        Task<RegisterResult> AddWorker(string username,string email, string telephone, string password, string confirmPassword, AccountsVariation variation);
        Task<Worker> LoginWorker(string username, string password, string confirmPassword);
    }
}
