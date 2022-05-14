using Project.Base;
using Project.Commands;
using Project.Commands.Helpers;
using Project.RegExp;
using Project.Services;
using Project.Services.AccountService;
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
    public class LoginVM : ViewModel
    {

        private bool _isAdmin;
        private string _username;
        private string _password;
        private string _reapetPassword;
        public bool isAdmin { 
            get { return _isAdmin;}
            set { _isAdmin = value; 
                OnPropertyChanged(); 
            } 
        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
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

        private readonly ICurrentAccountService _currentAccount;
        private readonly NavigationStore _navigationStore;
       
        

        public LoginVM(ICurrentAccountService currentAccount, NavigationStore navigationStore)
        {
             currentAccount.Logout();
            _currentAccount = currentAccount;
            _navigationStore = navigationStore;
             
        }

        public ICommand toRegistration
        {
            get
            {
                return new RelayCommand((x)=>
                {
                    (new NavigationCommand<RegistrationVM>(new NavigationService<RegistrationVM>(_navigationStore, () => new RegistrationVM(_currentAccount, _navigationStore)))).Execute(x);

                }
                ) ;


            }
        }
        public ICommand LogIn
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    if(await LogAcc())
                    {
                        (_currentAccount.GetAuthorizeCommand(_navigationStore)).Execute(x);

                    }
                    else
                    {
                       
                    }
                }
                );


            }
        }


        //public ICommand Login
        //{
        //    get
        //    {


        //    }
        //}
        public async Task<bool> LogAcc()
        {
            if (Password != RepeatPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return false;
            }
            if (!RegExpCheck.CheckLogin(Username)) return false;
            if (!RegExpCheck.CheckPassword(Password)) return false;
         


            if (!await _currentAccount.Login(Username,Password,RepeatPassword,_isAdmin))
            {
                MessageBox.Show("Такого аккаунта не существует попробуйте снова");
                return false;
            }

            return true;
        }
        public void refresh()
        {
            Username = "";
            Password = "";
            RepeatPassword = "";
            
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Password));
            
            OnPropertyChanged(nameof(RepeatPassword));
            



        }

    }
}
