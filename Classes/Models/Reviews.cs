using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Reviews : DefaultClass
    {
        private int _setrating = -1;

        private string? _text;

        private Book? _book;

        private User? _user;

        public Book Book
        {
            get { 
                if (_book == null)
                    _book = new Book();
                return _book;
            }
            set
            {
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
        public string Text
        {
            get
            {
                if (_text == null)
                    return "";
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public int Rating
        {
            get
            {
                return _setrating;
            }
            set
            {
                _setrating = value;
                OnPropertyChanged();
            }
        }


    }
}
