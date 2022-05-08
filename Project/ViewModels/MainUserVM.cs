using Project.Base;
using Project.Commands;
using Project.Services;
using Project.Services.AccountService;
using Project.Stores;
using Project.ViewModels.UserVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModels
{
    public class MainUserVM : Base.ViewModel
    {   private readonly ICurrentAccountService _currentAccount;
        private readonly NavigationStore _mainNavigationStore;
        private readonly NavigationStore _localNavigationStore;
        

        public MainUserVM(ICurrentAccountService currentAccount, NavigationStore mainNavigationStore)
        {
            _currentAccount = currentAccount;
            _mainNavigationStore = mainNavigationStore;
            _localNavigationStore = new NavigationStore();
            _localNavigationStore.CurrentViewModel = new HomeVM(_currentAccount);
            _localNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public ViewModel CurrentLocalViewModel
        {
            get { return _localNavigationStore.CurrentViewModel; }
            set { CurrentLocalViewModel = value; }
        }


        public ICommand openBooks
        {
            get
            {
                return new NavigationCommand<HomeVM>(new NavigationService<HomeVM>(_localNavigationStore, () => new HomeVM(_currentAccount)));
            }
        }
        public ICommand openProfile
        {
            get
            {
                return new NavigationCommand<ProfileVM>(new NavigationService<ProfileVM>(_localNavigationStore, () => new ProfileVM(_currentAccount)));
            }
        }
        public ICommand openHistory
        {
            get
            {
                return new NavigationCommand<HistoryVM>(new NavigationService<HistoryVM>(_localNavigationStore, () => new HistoryVM(_currentAccount)));
            }
        }
        public ICommand openFavourites
        {
            get
            {
                return new NavigationCommand<FavouritesVM>(new NavigationService<FavouritesVM>(_localNavigationStore, () => new FavouritesVM(_currentAccount)));
            }
        }
        public ICommand openAbout
        {
            get
            {
                return new NavigationCommand<AboutVM>(new NavigationService<AboutVM>(_localNavigationStore, () => new AboutVM(_currentAccount)));
            }
        }



        public ICommand ExitAccount
        {
            get
            {
                return new NavigationCommand<LoginVM>(new NavigationService<LoginVM>(_mainNavigationStore, () => new LoginVM(_currentAccount,_mainNavigationStore)));
            }
        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentLocalViewModel));
        }
    }
}
