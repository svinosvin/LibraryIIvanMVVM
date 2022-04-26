﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Person: DefaultClass
    {
        public string? _name;
        public string? _surname;
        public string? _firstname;
        public string? _email; 
        public string? _telnumber;
        public DateTime _birthDate;

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
            get {
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
        public string TelNumber
        {
            get
            {
                if (_telnumber == null)
                    return "";
                return _telnumber;
            }
            set
            {
                _telnumber = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get
            {
                if (_email == null)
                    return "";
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get
            {
              
                return _birthDate;
            }
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }
        public string GetAge()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - _birthDate.Year;
            if (_birthDate > now.AddYears(-age)) age--;
            return age.ToString();
        }

    }
}