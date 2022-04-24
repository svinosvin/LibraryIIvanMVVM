using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class HistoryTransactions : DefaultClass
    {
        private Book? _book;
        private User? _user;
        public Book Book {
            get {
                if(_book == null)
                    _book = new Book();
                return _book;
            }
            set {
                _book = value;
                OnPropertyChanged();
            }
        }
        public User User
        {
            get
            {
                if (_user == null)
                    _user = new User();
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        

    }
}
