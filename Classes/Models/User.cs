using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.Models
{
    
    public class User : DefaultClass
    {     

        private string? _login;

        private string? _password;

        private Person? _person;

        public string Login
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
        public string Password
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
        public int PersonId { get; set; }
        public ICollection<BookMark>? BookMarks { get; set; }
        public ICollection<HistoryTransactions>? Histories { get; set; }


    }
}
