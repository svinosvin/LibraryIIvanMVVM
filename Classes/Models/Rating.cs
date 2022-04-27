using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Rating : DefaultClass
    {
        public int _rate;
        public int Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                _rate = value;
                OnPropertyChanged();
            }
        }
        public ICollection<Book>? Books { get; set; }

    }
}
