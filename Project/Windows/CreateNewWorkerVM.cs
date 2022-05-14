using DataContext;
using Models.BaseModels;
using Models.Models;
using Project.Base;
using Project.Commands.Helpers;
using Project.RegExp;
using Project.Services.AccountService;
using Project.Services.AunthenticationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.Windows
{
    public class CreateNewWorkerVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        public int TypeWorker { get; set; } = 0;

        public List<string> Elements { get; set; } = new List<string> { "Админ", "Библиотекарь" };

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public string RepPassword { get; set; } = "";

        public string Email { get; set; } = "";

        public string TelNumber { get; set; } = "";
        private AppDbContextFactory dbcontex;

        public CreateNewWorkerVM(ICurrentAccountService currentAccount)
        {
            dbcontex = new AppDbContextFactory();
            _currentAccount = currentAccount;
        }

        public ICommand AddWorker
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    if (await CreateWorker()) MessageBox.Show("Успешно добавлен!");
                    refresh();

                });
            }
        }



        public async Task<bool> CreateWorker()
        {
            if (Password != RepPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return false;
            }
            if (!RegExpCheck.CheckLogin(Username)) return false;
            if (!RegExpCheck.CheckPassword(Password)) return false;
            if (!RegExpCheck.CheckEmail(Email)) return false;
            if (!RegExpCheck.CheckPhone(TelNumber)) return false;
            AccountsVariation acc;
            if ((PositionAtWork)TypeWorker == 0)
                acc = AccountsVariation.Admin;
            else
                acc = AccountsVariation.Worker;


            if (await _currentAccount.Register(Username, Password, RepPassword, Email, TelNumber, acc) != RegisterResult.Succes)
            {
                MessageBox.Show("Аккаунт с такими данными существует");
                return false;
            }

            return true;
        }
        public void refresh()
        {
            Username = "";
            Password = "";
            Email = "";
            RepPassword = "";
            TelNumber = "";
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(RepPassword));
            OnPropertyChanged(nameof(TelNumber));



        }
    } 
}
