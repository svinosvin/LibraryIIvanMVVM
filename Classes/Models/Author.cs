using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Author : DefaultClass 
    {
       

        public string? Name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
                OnPropertyChanged();
            }
        }
        public string? Surname
        {
            get
            {
                if (Surname == null)
                    return "";
                return Surname;
            }
            set
            {
                Surname = value;
                OnPropertyChanged();
            }
        }
        public string? Firstname
        {
            get
            {
                if (Firstname == null)
                    return "";
                return Firstname;
            }
            set
            {
                Firstname = value;
                OnPropertyChanged();
            }
        }
        public string? Description
        {
            get
            {
                if (Description == null)
                    return "";
                return Description;
            }
            set
            {
                Description = value;
                OnPropertyChanged();
            }
        }

        public ICollection<Book>? Books { get; set; }

    }
}
