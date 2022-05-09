﻿using Project.Base;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels.AdminVm
{
    public class BooksVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;

        public BooksVM(ICurrentAccountService currentAccount)
        {
            _currentAccount = currentAccount;
        }
    }
}
