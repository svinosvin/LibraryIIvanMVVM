using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class User : DefaultClass
    {

        private string _name;

        private DateTime _date;
        public string Name {
            get { return this._name; }
            set { 
                _name = value; 
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get { return this._date; }
            set
            { 
                _date = value;
                OnPropertyChanged();
            }
        }



    }
}
