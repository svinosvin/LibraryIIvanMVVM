using Models.BaseModels;
using Models.Models;
using Project.Base;
using Project.Commands.Helpers;
using Project.Services;
using Project.Services.AccountService;
using Project.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Project.ViewModels.AdminVm
{
    public class BooksVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private readonly IDataService<Book> _bookDataService;

        public ObservableCollection<Book> Books { get; set; }
        public ICollectionView BooksView { get; set; }
        private string _textbox = "";
        private Book _selectedbook;
        public string AllComments
        {
            get
            {
                string k = "";
                foreach (var item in SelectedBook.Reviews)
                {
                    k += $"UserID{item.UserId}\n {item.Text}\n\n";
                }
                return k;
            }
        }
        public string TextBox
        {
            get
            {
                return _textbox;
            }
            set
            {
                _textbox = value;
                refreshBooks();
                OnPropertyChanged();
            }
        }
        public Book SelectedBook
        {
            get { return _selectedbook; }
            set
            {
                _selectedbook = value;
              
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllComments));


            }
        }
        private int _listElement;
        public int ListElement
        {
            get
            {
                return _listElement;
            }
            set
            {
                _listElement = value;
                refreshBooks();
                OnPropertyChanged();
            }
        }

        public ICommand ClearText
        {
            get
            {
                return new RelayCommand((x) =>
                {
                    TextBox = "";
                });
            }
        }
        public BooksVM(ICurrentAccountService currentAccount, IDataService<Book> bookDataService)
        {
            _currentAccount = currentAccount;
            _bookDataService = bookDataService;
            
          
            FullBooks();
        }
        public async Task<bool> FullBooks()
        {

            IEnumerable<Book> books = await _bookDataService.GetAll();
            Books = new ObservableCollection<Book>(books);
            BindingOperations.EnableCollectionSynchronization(Books, new object());
            BooksView = CollectionViewSource.GetDefaultView(Books);
            SelectedBook = Books.First();

            return true;

        }
        public void refreshBooks()
        {

            BooksView.Filter = (x) =>
            {
                if (x is Book book)
                {
                    switch (ListElement)
                    {

                        case 0:
                            return
                                book.Title.ToLower().Contains(_textbox.ToLower(), StringComparison.Ordinal) == true;

                        case (int)(TypeofBooks.Adventure) + 1:
                            return (
                              (book.Title.ToLower().Contains(_textbox.ToLower())
                               && book.TypeofBook == TypeofBooks.Adventure)) == true;


                        case (int)(TypeofBooks.Classics) + 1:
                            return (
                                (book.Title.ToLower().Contains(_textbox.ToLower())
                               && book.TypeofBook == TypeofBooks.Classics)) == true;


                        case (int)(TypeofBooks.Detective) + 1:
                            return (
                                   (book.Title.ToLower().Contains(_textbox.ToLower())
                               && book.TypeofBook == TypeofBooks.Detective)) == true;



                        case (int)(TypeofBooks.Mystery) + 1:
                            return (
                                (book.Title.ToLower().Contains(_textbox.ToLower())
                               && book.TypeofBook == TypeofBooks.Mystery)) == true;


                        case (int)(TypeofBooks.Fantasy) + 1:
                            return (
                               (book.Title.ToLower().Contains(_textbox.ToLower())
                               && book.TypeofBook == TypeofBooks.Fantasy)) == true;


                        case (int)(TypeofBooks.Historical_Fiction) + 1:
                            return (
                             (book.Title.ToLower().Contains(_textbox.ToLower())
                               && book.TypeofBook == TypeofBooks.Historical_Fiction)) == true;

                        default: return book.Title.ToLower().Contains(_textbox.ToLower());
                    }

                }
                return false;
            };
            BooksView.Refresh();

        }
        public ICommand AddBook
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    WindowCreateBook window = new WindowCreateBook(_bookDataService);
                    window.Show();
                    
                    
                }); ;
            }
        }
        public ICommand RemoveBook
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    await _bookDataService.Delete(SelectedBook.Id);
                    Books.Remove(SelectedBook);
                    MessageBox.Show("Успешно удалено!");
                },(x=>SelectedBook!=null));
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    IEnumerable<Book> books = await _bookDataService.GetAll();
                    Books = new ObservableCollection<Book>(books);
                    OnPropertyChanged(nameof(Books));
                    refreshBooks();
                });
            }
        }
    }

}
