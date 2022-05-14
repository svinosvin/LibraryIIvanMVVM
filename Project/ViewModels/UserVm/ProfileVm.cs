using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.AspNet.Identity;
using Models.Models;
using Project.Base;
using Project.Commands.Helpers;
using Project.RegExp;
using Project.Services;
using Project.Services.AccountService;

namespace Project.ViewModels.UserVm
{
    public class ProfileVM : ViewModel
    {

        private readonly ICurrentAccountService _currentAccount;
        private readonly IAccountDataService _accountDataService;
        private readonly IPasswordHasher _hasher;

        private string _prevPass = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Firstname { get; set; } = "";
        public string NewPassword { get; set; } = "";
        public string PrevPassword {
            get { return _prevPass; }
            set
            {
                _prevPass = value;
                OnPropertyChanged();
            }
        }

        public string TelNumber { get; set; } = "";





        public User Account { get; set; }

        public ProfileVM(ICurrentAccountService currentAccount, IAccountDataService accountDataService)
        {
            _currentAccount = currentAccount;
            _accountDataService = accountDataService;
            Account = _currentAccount.CurrentAccount as User;
            _hasher = new PasswordHasher();
            refreshTextboxes();

        }




        public ICommand ChangeUserPassword
        {
            get
            {
                return new RelayCommand(async (x) =>
                {

                    if (await changeUserProfile(1) == true)
                    {
                        MessageBox.Show("Данные успешно измены");
                        refreshTextboxes();
                    }
                }
               , (x) => Account != null);
            }
        }
        public ICommand ChangeUserData
        {
            get
            {
                return new RelayCommand(async (x) =>
                {

                    if (await changePersonProfile(1))
                    {
                        MessageBox.Show("Данные успешно измены");
                        refreshTextboxes();
                    }
                }
               , (x) => Account != null);
            }
        }
        public async Task<bool> changePersonProfile(int k)
        {

            if (!RegExpCheck.CheckEmail(Email)) return false;
            if (!RegExpCheck.CheckPhone(TelNumber)) return false;

            bool result = true;
            if (Email != Account.Person.Email && (Email != Account.Person.Email && await _accountDataService.GetByEmail(Email) != null))
            {
                MessageBox.Show("Такая почта уже существует");
                result = false;
                return result;
            }
                   

            ((User)(_currentAccount.CurrentAccount)).Person = new Person
            {
                Id = Account.PersonId,
                Email = Email,
                TelNumber = TelNumber,
                Name = Name,
                Surname = Surname,
                Firstname = Firstname
            };

            Account = _currentAccount.CurrentAccount as User;
            await _accountDataService.Update(Account.Id, Account);

            NewPassword = "";
            return result;

        }
        public async Task<bool> changeUserProfile(int k)
        {
            bool result = true;
            if (Username != Account.Login)
            {
                MessageBox.Show("Неверный Логин!");
                result = false;
                return result;

            }
            if (!RegExpCheck.CheckPassword(NewPassword)) return false;
            if (_hasher.VerifyHashedPassword(Account.Password, PrevPassword) == PasswordVerificationResult.Failed)
            { 
                MessageBox.Show("Неверный старый пароль!");
                result = false;
                return result;
            }
            else if (_hasher.VerifyHashedPassword(Account.Password, NewPassword) == PasswordVerificationResult.Success)
            {
                MessageBox.Show("Новый и старый пароль совпадают!");
                result = false;
                return result;
            }

          
            string HashedPassword = _hasher.HashPassword(NewPassword);
            Account.Login = Username;
            Account.Password = HashedPassword;
           
            await _accountDataService.Update(Account.Id, Account);
            await _currentAccount.Login(Account.Login, NewPassword, NewPassword, false);
            Account = _currentAccount.CurrentAccount as User;


            PrevPassword = "";
            NewPassword = "";
            OnPropertyChanged(nameof(PrevPassword));
            OnPropertyChanged(nameof(NewPassword));
          
            return result;

        }

        public void refreshTextboxes() {

            Email = Account.Person.Email;
            Firstname = Account.Person.Firstname;
            Surname = Account.Person.Surname;
            Name = Account.Person.Name;
            TelNumber = Account.Person.TelNumber;
           

        }

    }
}
