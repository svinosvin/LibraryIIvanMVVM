using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Project.Base;
using Project.Commands.Helpers;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.Windows
{
    public class CreateBookVm : ViewModel
    {
        private AppDbContextFactory dbcontex;
        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<string> AuthorsFIO {
            get {
                List<string> list = new List<string>();
                foreach (var item in Authors)
                {
                    string s = $"{item.Name} {item.Surname} {item.Firstname}";
                    list.Add(s);
                    
                }
                return new ObservableCollection<string>(list); 
               } 
        }


        private int _listTypeElement;
        public int ListTypeElement
        {
            get
            {
                return _listTypeElement;
            }
            set
            {
                _listTypeElement = value;
                OnPropertyChanged();
            }
        }

        private int _ListAuthorElements;
        public int ListAuthorElements
        {
            get
            {
                return _ListAuthorElements;
            }
            set
            {
                _ListAuthorElements = value;
                OnPropertyChanged();
            }
        }

        public IDataService<Book> _bookDataService;
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Firstname { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public CreateBookVm(IDataService<Book> bookDataService)
        {

            _bookDataService = bookDataService;
            dbcontex = new AppDbContextFactory();
            ListTypeElement = 0;
            ListAuthorElements = 0;
            FullBooks();
        }
        public async Task<bool> FullBooks()
        {
            using (ApplicationDbContext _context = dbcontex.CreateDbContext())
            {
                Authors = new ObservableCollection<Author>(await _context.Authors.ToListAsync());
            }
            return true;

        }
        public ICommand AddAuthor
        {
            get
            {
                return new RelayCommand(async(x) =>
                {
                    using (ApplicationDbContext _context = dbcontex.CreateDbContext())
                    {
                        Author author = new Author
                        {
                            Name = Name,
                            Firstname = Firstname,
                            Surname = Surname
                        };
                        Authors.Add(author);
                        await _context.Authors.AddAsync(author);
                        _context.SaveChanges();
                        OnPropertyChanged(nameof(Authors));
                        refresh();
                        MessageBox.Show("Успешно добавлено!");

                    }

                },(x)=> !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Surname) && !String.IsNullOrWhiteSpace(Firstname));
            }
        }
        public ICommand AddBook
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    using (ApplicationDbContext _context = dbcontex.CreateDbContext())
                    {
                        Book book = new Book
                        {

                            Title = Title,
                            Description = Description,
                            TypeofBook = (TypeofBooks)ListTypeElement
                            
                        };

                        Author author = Authors[ListAuthorElements];
                        author.Books.Add(book);
                        _context.Authors.Update(author);
                        await _context.SaveChangesAsync();
                        MessageBox.Show("Успешно добавлено!");

                    }


                }, (x) => !String.IsNullOrWhiteSpace(Title)); ;
            }
        }
        private void refresh()
        {
            Name = "";
            Title = "";
            Firstname = "";
            Surname = "";
            Description = "";
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Firstname));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(AuthorsFIO));
        }
    }
}
