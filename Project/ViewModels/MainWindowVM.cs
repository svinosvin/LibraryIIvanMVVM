using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Models.Models;
using Project.Base;
using Project.Commands;
using Project.Services;
using Project.Stores;
using Project.ViewModels.UserVm;

namespace Project.ViewModels
{
    public class MainWindowVM:Base.ViewModel
    {
        //ObservableCollection<User> Users = new ObservableCollection<User>();

        private readonly NavigationStore _navigationStore;

        public ViewModel CurrentViewModel { 
            get { return _navigationStore.CurrentViewModel;}
            set { CurrentViewModel = value; } 
        }


        //public ICommand Add
        // {
        //     get
        //     {
        //         return new DelegateCommand((obj) =>
        //         {
        //             Count++;
        //         }, (obj1)=> (Count<10) );
        //     }
        // }
      

        public ICommand openHome
        {
            get
            {
                return new NavigationCommand<HomeVM>(new NavigationService<HomeVM>( _navigationStore, () => new HomeVM()));
            }
        }
        public ICommand openProfile
        {
            get
            {
                return new NavigationCommand<ProfileVm>(new NavigationService<ProfileVm>(_navigationStore, () => new ProfileVm()));
            }
            
            
        }



        public MainWindowVM(NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
            navigationStore.CurrentViewModel = new LoginVM(navigationStore);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
