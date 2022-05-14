using Models.Models;
using Project.Base;
using Project.Commands;
using Project.Services;
using Project.Services.AccountService;
using Project.Stores;
using Project.ViewModels.WorkerVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModels
{
    public class MainWorkerVM : Base.ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private readonly NavigationStore _mainNavigationStore;
        private readonly NavigationStore _localNavigationStore;
        private readonly IAccountDataService _accountDataService;
        private readonly IDataService<Book> _bookDataService;
        private readonly IWorkerDataService _workerDataService;

        public MainWorkerVM(ICurrentAccountService currentAccount, NavigationStore mainNavigationStore, IAccountDataService accountDataService, IDataService<Book> bookDataService, IWorkerDataService workerDataService)
        {
            _currentAccount = currentAccount;
            _mainNavigationStore = mainNavigationStore;
            _accountDataService = accountDataService;
            _bookDataService = bookDataService;
            _workerDataService = workerDataService;
            _localNavigationStore = new NavigationStore();
            _localNavigationStore.CurrentViewModel = new RequestsVM(_currentAccount);
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
        public ICommand openRequest
        {
            get
            {
                return new NavigationCommand<RequestsVM>(new NavigationService<RequestsVM>(_localNavigationStore, () => new RequestsVM(_currentAccount)));
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
