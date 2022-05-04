using Project.Commands;
using Project.Services;
using Project.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public RegistrationVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public ICommand toLogin
        {
            get
            {
                return new NavigationCommand<LoginVM>(new NavigationService<LoginVM>(_navigationStore, () => new LoginVM(_navigationStore)));

            }
        }

    }
}
