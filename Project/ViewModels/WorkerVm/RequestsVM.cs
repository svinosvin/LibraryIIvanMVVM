﻿using Project.Base;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels.WorkerVm
{
    public class RequestsVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;

        public RequestsVM(ICurrentAccountService currentAccount)
        {
            _currentAccount = currentAccount;
        }
    }
}
