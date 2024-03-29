﻿using System;
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
        Horror

    }

    public class Book : DefaultClass
    {


        private string _title;
        private string _image;

        private string _description;

        private Author _author;
        private int? _count;
        public TypeofBooks? TypeofBook { get; set; }
        public string? Image
        {
            get
            {
                
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }
        public string? Description
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
        public string? Title
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
       
        public Author? Author
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
        public int? Count
        {
            get
            {

                return _count;
            }
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }


        public ICollection<HistoryTransactions>? Histories { get; set; } = new List<HistoryTransactions>();
        public ICollection<Reviews>? Reviews { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Favourite>? Favourites { get; set; } = new List<Favourite>();


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
