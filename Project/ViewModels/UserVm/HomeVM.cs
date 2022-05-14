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
using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Project.Base;
using Project.Commands.Helpers;
using Project.Services;
using Project.Services.AccountService;

namespace Project.ViewModels.UserVm
{
    public interface IAsyncInitialization
    {
        /// <summary>
        /// The result of the asynchronous initialization of this instance.
        /// </summary>
        Task Initialization { get; }
    }
    public class HomeVM : ViewModel, IAsyncInitialization
    {

        private readonly ICurrentAccountService _currentAccount;
        private readonly IAccountDataService _accountDataService;
        private readonly IDataService<Book> _bookDataService;

        private AppDbContextFactory _contextFactory;
        private User _user;

        private Book _selectedbook;

        private string _textbox = "";
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        } 
        public string isFavourite
        {
            get
            {
                return User?.Favourites?.FirstOrDefault(x => x.BookId == SelectedBook.Id) != null ? "Ecть" : "Нет";

            }
        }

        private int _listElement;
        public int ListElement {
            get
            {
                return _listElement;
            } set {
                _listElement = value;
                refreshBooks();
                OnPropertyChanged();
            } }

        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }
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
                Comment = "";
                OnPropertyChanged();
                OnPropertyChanged(nameof(isFavourite));     
                OnPropertyChanged(nameof(Comment));
                OnPropertyChanged(nameof(AllComments));
            }
        }
        public ObservableCollection<Book> Books { get; set; }
        public ICollectionView BooksView { get; set; }
       

        public HomeVM(ICurrentAccountService currentAccount, IDataService<Book> bookdataService, IAccountDataService accountDataService)
        {
            _currentAccount = currentAccount;
            _bookDataService = bookdataService;
            _accountDataService = accountDataService;
            _contextFactory = new AppDbContextFactory(); 
            User = (User)(_currentAccount.CurrentAccount);
            Initialization = InitializeAsync();
          

        }







       
        public async Task UpdateUser()
        {
            await _accountDataService.Update(User.Id, User);
        }
        public async Task UpdateBook()
        {
            await _bookDataService.Update(SelectedBook.Id, SelectedBook);
        }
        public Task Initialization { get; private set; }
        private async Task InitializeAsync()
        {
            // Asynchronously initialize this instance.
            await FullBooks();
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



        public ICommand RequestToWorker
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    if (User.Histories.FirstOrDefault(x=>x.BookId==SelectedBook.Id) == null)
                    {
                        User.Histories.Add(new HistoryTransactions
                        {
                            Accept = false,
                            Book = SelectedBook,
                            Begin = DateTime.Today,
                            End = DateTime.Today.AddDays(30),
                            
                        });
                        MessageBox.Show("Запрос отправлен");
                        await UpdateUser();
                    }
                    else
                    {
                       
                        MessageBox.Show("Вы уже отправляли запрос на получение");

                    }
                   


                }, (x) => SelectedBook != null);
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
    public ICommand AddToFavourites
    {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    if (User.Favourites.ToList().FirstOrDefault(x=>x.BookId == SelectedBook.Id) == null)
                    {
                        User.Favourites.Add(new Favourite { UserId = User.Id, BookId = SelectedBook.Id});
                        MessageBox.Show("Добавлено в закладки");
                    }
                    else
                    {
                        Favourite f = User.Favourites.FirstOrDefault(x => x.BookId == SelectedBook.Id);
                        using (ApplicationDbContext _context = _contextFactory.CreateDbContext())
                        {

                            _context.Favourites.Remove(f);
                            _context.SaveChanges();
                        }
                        User.Favourites.Remove(f);
                        MessageBox.Show("Удалено из закладок");
                        

                    }
                    await UpdateUser();
                    OnPropertyChanged(nameof(User));
                    OnPropertyChanged(nameof(isFavourite));


                }, (x) => SelectedBook != null);
            }
        }

    public  ICommand AddComment
    {
            get
            {
                return new RelayCommand(async (x) =>
                {

                    SelectedBook.Reviews.Add(new Reviews { UserId = User.Id , BookId = SelectedBook.Id, Text = Comment});
                    Comment = "";
                    OnPropertyChanged(nameof(SelectedBook));
                    OnPropertyChanged(nameof(AllComments));
                    BooksView.Refresh();
                    await UpdateBook();
                }, (x) => SelectedBook != null && !String.IsNullOrWhiteSpace(Comment) && SelectedBook.Reviews.FirstOrDefault(x=>x.UserId == User.Id)==null);
            }
        }

     
    }
}
