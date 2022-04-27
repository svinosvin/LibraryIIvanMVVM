using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Author : DefaultClass 
    {
        public string? _name;
        public string? _surname;
        public string? _firstname;
        public string? _description;

        public string Name
        {
            get
            {
                if (_name == null)
                    return "";
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get
            {
                if (_surname == null)
                    return "";
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Firstname
        {
            get
            {
                if (_firstname == null)
                    return "";
                return _firstname;
            }
            set
            {
                _firstname = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get
            {
                if (_description == null)
                    return "";
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public ICollection<Book>? Books;

    }
}
