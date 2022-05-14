using Models.Models;
using Project.Base;
using Project.Services;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels.AdminVm
{
    
    public class UsersVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private readonly IAccountDataService _accountDataService;

        public IEnumerable<User> Users { get; set; }

        public UsersVM(ICurrentAccountService currentAccount, IAccountDataService accountDataService)
        {
            _currentAccount = currentAccount;
            _accountDataService = accountDataService;
            FullUsers();
        }

        public async Task FullUsers()
        {
            Users = await _accountDataService.GetAll();
        }

    }
    
}
