
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

    public class Worker : Person
    {
      
        private PositionAtWork _position;
       
        public PositionAtWork Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }     


    }
}
