using Models.Models;
using Project.Base;
using Project.Commands.Helpers;
using Project.Services;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModels.AdminVm
{
    
    public class UsersVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private readonly IAccountDataService _accountDataService;

        public List<User> Users { get; set; }

        private User _selectedItem;
        public User SelectedItem { get { return _selectedItem; } set{ _selectedItem = value;OnPropertyChanged(); } }
        public UsersVM(ICurrentAccountService currentAccount, IAccountDataService accountDataService)
        {
            _currentAccount = currentAccount;
            _accountDataService = accountDataService;
            FullUsers();
            SelectedItem = Users.FirstOrDefault();
        }

        public async Task FullUsers()
        {
            Users =  _accountDataService.GetAll().Result.ToList();
           
        }
        public ICommand DeleteUser
        {
            get
            {
                return new RelayCommand(async (x) =>
                {

                    await _accountDataService.Delete(SelectedItem.Id);
                    FullUsers();
                    OnPropertyChanged(nameof(Users));
                    MessageBox.Show("Успешно удалено!");
                }, (x) => SelectedItem != null); ;
            }
        }

    }
    
}
