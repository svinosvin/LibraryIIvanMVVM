
using Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public enum PositionAtWork
    {
        Librarian,
        Admin
    }

    public class Worker : DefaultClass, ITypeOfAccount
    {

        private string? _login;

        private string? _password;

        private Person _person;

        private PositionAtWork _position;


   
        public string? Login
        {
            get
            {
                if (_login == null)
                    return "";
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string? Password
        {
            get
            {
                if (_password == null)
                    return "";
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public Person Person
        {
            get
            {
                if (_person == null)
                    _person = new Person();
                return _person;
            }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }
        public PositionAtWork Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }
        public int PersonId { get; set;}

        public AccountsVariation GetAccountsVariation()
        {
            if (_position == PositionAtWork.Librarian) return AccountsVariation.Worker;
            else return AccountsVariation.Admin;
        }
    }
}
