using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Book : DefaultClass
    {
        private string? _title;

        private string _rating; 

        private Author? _author;


        private string Rate
        {
            get
            {
                return AvgRating();
            }
        }
        public string Title
        {
            get
            {
                if (_title == null)
                    return "";
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public Author Author
        {
            get
            {
                if (_author == null)
                    _author = new Author();
                return _author;
            }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();


        public string AvgRating()
        {
            if (Reviews.Count == 0)
            {
                return "";
            }
            else
            {
                int rate = 0;
                ICollection<Reviews> local = Reviews.Where(x => x.Rating != -1).ToList();
                foreach (var item in local)
                {
                    rate += item.Rating;
                }
                float avg = rate / local.Count;
                return $"{avg:f1}";
            }
        }

    }
}
