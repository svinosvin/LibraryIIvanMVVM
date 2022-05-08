using Models.BaseModels;
using Models.Models;
using Project.Services.AunthenticationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.AccountService
{
 

    public interface ICurrentAccountService
    {
        public ITypeOfAccount CurrentAccount { get;}
        
        public AccountsVariation Variation { get; }
        public bool isLoggedIn { get; }

        public Task<RegisterResult> Register(string username, string password, string confirmPassword, string email, string telnumber, AccountsVariation variation);

        public Task<bool> Login(string username, string password, string confirmPassword,bool variation);

        public void Logout();

    }
}
