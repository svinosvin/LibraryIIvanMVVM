﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Base;
using Project.Services.AccountService;

namespace Project.ViewModels.UserVm
{
    public class ProfileVM : ViewModel
    {

        private readonly ICurrentAccountService _currentAccount;

        public ProfileVM(ICurrentAccountService currentAccount)
        {
            _currentAccount = currentAccount;
        }
    }
}
