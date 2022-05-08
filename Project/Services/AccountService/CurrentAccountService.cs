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
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly IAunthenticationService _aunthenticationService;
        private readonly IWorkerAuthenticationService _workerAuthenticationService;

        public CurrentAccountService(IAunthenticationService aunthenticationService, IWorkerAuthenticationService workerAuthenticationService)
        {
            _aunthenticationService = aunthenticationService;
            _workerAuthenticationService = workerAuthenticationService;
        }

        public ITypeOfAccount CurrentAccount { get; private set; }
        public AccountsVariation Variation { get; private set; }
        public bool isLoggedIn => CurrentAccount != null ;


        public async Task<bool> Login(string username, string password, string confirmPassword, bool variation)
        {
            bool result = true;
            try
            {
                if (variation == false)
                {
                    CurrentAccount = await _aunthenticationService.Login(username, password, confirmPassword);
                    Variation = AccountsVariation.User;
                }
                else
                {
                    CurrentAccount = await _workerAuthenticationService.LoginWorker(username, password, confirmPassword);
                    Variation = ((Worker)CurrentAccount).GetAccountsVariation();
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Logout()
        {
            CurrentAccount = null;
            
        }

        public async Task<RegisterResult> Register(string username, string password, string confirmPassword, string email, string telnumber, AccountsVariation variation)
        {
            RegisterResult result = RegisterResult.Succes;

            if (variation == AccountsVariation.User)
            {
                result = await _aunthenticationService.Register(username, password, confirmPassword, email, telnumber);
            }
            else
            {
                result = await _workerAuthenticationService.AddWorker(username, password, confirmPassword, email, telnumber, variation);

            }
            return result;
        }



    }
}
