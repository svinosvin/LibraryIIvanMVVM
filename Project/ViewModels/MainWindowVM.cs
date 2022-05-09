using Project.Base;
using Project.Services.AccountService;
using Project.Stores;

namespace Project.ViewModels
{
    public class MainWindowVM : Base.ViewModel
    {
        //ObservableCollection<User> Users = new ObservableCollection<User>();

        private readonly NavigationStore _navigationStore;
        private readonly ICurrentAccountService _currentAccount;

        public MainWindowVM(NavigationStore navigationStore, ICurrentAccountService currentAccount)
        {
            _navigationStore = navigationStore;
            _currentAccount = currentAccount;
            navigationStore.CurrentViewModel = new MainUserVM(_currentAccount, _navigationStore);
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
