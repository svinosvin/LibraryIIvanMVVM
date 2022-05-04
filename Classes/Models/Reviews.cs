using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Reviews : DefaultClass
    {
       

        private string _text;

        private User _user;


        public ICollection<Book>? Books { get; set; }
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

    }
}
