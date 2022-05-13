using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Favourite : DefaultClass
    {
        
        private Book? _book;
        public Book Book
        {
            get
            {
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

        public User User { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
