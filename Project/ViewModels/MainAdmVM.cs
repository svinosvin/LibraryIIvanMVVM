using Models.Models;
using Project.Base;
using Project.Commands;
using Project.Services;
using Project.Services.AccountService;
using Project.Stores;
using Project.ViewModels.AdminVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModels
{
    public class MainAdmVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private readonly NavigationStore _mainNavigationStore;
        private readonly NavigationStore _localNavigationStore;
        private readonly IAccountDataService _accountDataService;
        private readonly IDataService<Book> _bookDataService;
        private readonly IWorkerDataService _workerDataService;



        public MainAdmVM(ICurrentAccountService currentAccount, NavigationStore mainNavigationStore, IAccountDataService accountDataService, IDataService<Book> bookDataService, IWorkerDataService workerDataService)
        {
            _workerDataService = workerDataService;
            _currentAccount = currentAccount;
            _mainNavigationStore = mainNavigationStore;  
            _accountDataService = accountDataService;
            _bookDataService = bookDataService;
            _localNavigationStore = new NavigationStore();
            _localNavigationStore.CurrentViewModel = new BooksVM(_currentAccount,_bookDataService);
            _localNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public ViewModel CurrentLocalViewModel
        {
            get { return _localNavigationStore.CurrentViewModel; }
            set { CurrentLocalViewModel = value; }
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentLocalViewModel));
        }
        public ICommand openProfile
        {
            get
            {
                return new NavigationCommand<ProfileVM>(new NavigationService<ProfileVM>(_localNavigationStore, () => new ProfileVM(_currentAccount)));
            }
        }
        public ICommand openUsers
        {
            get
            {
                return new NavigationCommand<UsersVM>(new NavigationService<UsersVM>(_localNavigationStore, () => new UsersVM(_currentAccount,_accountDataService)));
            }
        }
        public ICommand openWorkers
        {
            get
            {
                return new NavigationCommand<WorkersVM>(new NavigationService<WorkersVM>(_localNavigationStore, () => new WorkersVM(_currentAccount)));
            }
        }
        public ICommand openBooks
        {
            get
            {
                return new NavigationCommand<BooksVM>(new NavigationService<BooksVM>(_localNavigationStore, () => new BooksVM(_currentAccount,_bookDataService)));
            }
        }



        public ICommand ExitAccount
        {
            get
            {
                return new NavigationCommand<LoginVM>(new NavigationService<LoginVM>(_mainNavigationStore, () => new LoginVM(_currentAccount, _mainNavigationStore)));
            }
        }
    }
}
