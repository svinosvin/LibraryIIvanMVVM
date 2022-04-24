using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Models.Models;
using Models.BaseModels;

namespace ViewModels.UserVm
{
    public class MainWindowVM:Base.ViewModel
    {

        ObservableCollection<User> Users = new ObservableCollection<User>();

        private int _Count = -1;
        public int Count { get
            {
                if (_Count == -1)
                    _Count = 0;
                return _Count;
            } 
            set {
                _Count = value;
                OnPropertyChanged();
            } 
        }

        public User User
        {
            get
            {
                return this.User;
            }
            set
            {
                User = value;
                OnPropertyChanged();

            }
        }

       public ICommand Add
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Count++;
                }, (obj1)=> (Count<10) );
            }
        }
       
        public MainWindowVM()
        {

        }
       
           










    }
}
