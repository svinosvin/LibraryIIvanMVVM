using Project.Commands;
using Project.Commands.Helpers;
using Project.RegExp;
using Project.Services;
using Project.Services.AccountService;
using Project.Services.AunthenticationService;
using Project.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModels
{
    public class RegistrationVM : Base.ViewModel
    {
       
        private string _username;
        private string _email;
        private string _telnumber = "+375";
        private string _password;
        private string _reapetPassword;
        
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string Telephone
        {
            get { return _telnumber; }
            set
            {
                if (value.Length < 5 || String.IsNullOrEmpty(value))
                {
                    _telnumber = "+375";
                }
                else
                {
                    _telnumber = value;
                }
                    OnPropertyChanged();

            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string RepeatPassword
        {
            get { return _reapetPassword; }
            set
            {
                _reapetPassword = value;
                OnPropertyChanged();
            }
        }


        private readonly NavigationStore _navigationStore;
        private readonly ICurrentAccountService _currentAccount;

        

        public RegistrationVM( ICurrentAccountService currentAccount, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _currentAccount = currentAccount;
        }

        public ICommand Register
        {
            get
            {
                return new RelayCommand( async x =>
                {

                    if (await CreateAccount()) MessageBox.Show("Аккаунт успешно создан!");

                });
                

            }
        }
        public ICommand toLogin
        {
            get
            {
              
                return new NavigationCommand<LoginVM>(new NavigationService<LoginVM>(_navigationStore, () => new LoginVM(_currentAccount, _navigationStore)));

            }
        }


        public async Task<bool> CreateAccount()
        {
            if (Password != RepeatPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return false;
            }
            if (!RegExpCheck.CheckLogin(Username)) return false;
            if (!RegExpCheck.CheckPassword(Password)) return false;
            if (!RegExpCheck.CheckEmail(Email)) return false;
            if (!RegExpCheck.CheckPhone(Telephone)) return false;
            

            if (await _currentAccount.Register(Username, Password, RepeatPassword, Email, Telephone, Models.BaseModels.AccountsVariation.User) != RegisterResult.Succes)
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
            RepeatPassword = "";
            Telephone = "";
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(RepeatPassword));
            OnPropertyChanged(nameof(Telephone));



        }

    }
}
