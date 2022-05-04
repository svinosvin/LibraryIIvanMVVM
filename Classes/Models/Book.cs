using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{

    public enum TypeofBooks{
        Adventure,
        Classics,
        Detective, 
        Mystery,
        Fantasy,
        Historical_Fiction,
        Horror,
        Literary_Fiction,

    }

    public class Book : DefaultClass
    {
        

      
        public TypeofBooks? TypeofBook { get; set; }
        public string? Title
        {
            get
            {
                if (Title == null)
                    return "";
                return Title;
            }
            set
            {
                Title = value;
                OnPropertyChanged();
            }
        }
        public Author? Author
        {
            get
            {
                if (Author == null)
                    Author = new Author();
                return Author;
            }
            set
            {
                Author = value;
                OnPropertyChanged();
            }
        }
        public int? Count
        {
            get
            {

                return Count;
            }
            set
            {
                Count = value;
                OnPropertyChanged();
            }
        }
        public ICollection<Reviews>? Reviews { get; set; }
        public ICollection<Rating>? Ratings { get; set; }


        //public string AvgRating()
        //{
        //    if (Reviews.Count == 0)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        int rate = 0;
        //        ICollection<Reviews> local = Reviews.Where(x => x.Rating != -1).ToList();
        //        foreach (var item in local)
        //        {
        //            rate += item.Rating;
        //        }
        //        float avg = rate / local.Count;
        //        return $"{avg:f1}";
        //    }
        //}

    }
}
