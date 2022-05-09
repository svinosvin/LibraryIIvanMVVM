using Project.Base;
using Project.Services.AccountService;
using Project.Stores;
using Project.ViewModels.WorkerVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class MainWorkerVM : Base.ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private readonly NavigationStore _mainNavigationStore;
        private readonly NavigationStore _localNavigationStore;


        public MainWorkerVM(ICurrentAccountService currentAccount, NavigationStore mainNavigationStore)
        {
            _currentAccount = currentAccount;
            _mainNavigationStore = mainNavigationStore;
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
    }
}
