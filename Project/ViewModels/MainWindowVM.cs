using Models.Models;
using Project.Base;
using Project.Services;
using Project.Services.AccountService;
using Project.Stores;

namespace Project.ViewModels
{
    public class MainWindowVM : Base.ViewModel
    {
        //ObservableCollection<User> Users = new ObservableCollection<User>();

        private readonly NavigationStore _navigationStore;
        private readonly ICurrentAccountService _currentAccount;
        private readonly IAccountDataService _accountDataService;
        private readonly IDataService<Book> _bookDataService;
        private readonly IWorkerDataService _workerDataService;



        public MainWindowVM(NavigationStore navigationStore, ICurrentAccountService currentAccount, IAccountDataService accountDataService, IDataService<Book> bookDataService, IWorkerDataService workerDataService)
        {
            _navigationStore = navigationStore;
            _currentAccount = currentAccount;
            _accountDataService = accountDataService;
            _bookDataService = bookDataService;
            _workerDataService = workerDataService;
             navigationStore.CurrentViewModel = new MainAdmVM(_currentAccount, _navigationStore, _accountDataService,_bookDataService,_workerDataService);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public ViewModel CurrentViewModel
        {
            get { return _navigationStore.CurrentViewModel; }
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









        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
