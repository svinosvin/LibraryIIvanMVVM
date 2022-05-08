using Project.Base;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels.UserVm
{
    public class HistoryVM : ViewModel
    {

        private readonly ICurrentAccountService _currentAccount;

        public HistoryVM(ICurrentAccountService currentAccount)
        {
            _currentAccount = currentAccount;
        }


    }
}
