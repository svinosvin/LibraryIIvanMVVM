using DataContext;
using Project.Base;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels.AdminVm
{
    public class WorkersVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private AppDbContextFactory dbContextFactory;
        public WorkersVM(ICurrentAccountService currentAccount)
        {
            _currentAccount = currentAccount;
        }
    }

}
