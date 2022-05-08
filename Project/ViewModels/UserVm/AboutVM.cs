using Project.Base;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels.UserVm
{
    public class AboutVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;

        public AboutVM(ICurrentAccountService currentAccount)
        {
            _currentAccount = currentAccount;
        }
    }
}
