using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Classes;

namespace ViewModels.UserVm

{
    public class MainWindowVM:Base.ViewModel
    {

        ObservableCollection<User> Users = new ObservableCollection<User>();
      

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

       
        public MainWindowVM()
        {

        }
       
           










    }
}
