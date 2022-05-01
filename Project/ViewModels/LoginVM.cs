using Project.Base;
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
    public class LoginVM : ViewModel
    {
        private readonly NavigationStore _navigationStore;
      
        public LoginVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public ICommand toRegistration
        {
            get
            {
                return new NavigationCommand<RegistrationVM>(new NavigationService<RegistrationVM>(_navigationStore, () => new RegistrationVM()));
            }
        }

    }
}
