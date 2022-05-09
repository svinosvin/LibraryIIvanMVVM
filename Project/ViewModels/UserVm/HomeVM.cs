using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Project.Base;
using Project.Services.AccountService;

namespace Project.ViewModels.UserVm
{
    public class HomeVM : ViewModel
    {

        private readonly ICurrentAccountService _currentAccount;
        private AppDbContextFactory _contextFactory;
       
        public ObservableCollection<Book> Books { get; set;}
        private Book _selectedbook;
        public Book SelectedBook {
        get { return _selectedbook; } 
        set {
               _selectedbook = value;
                OnPropertyChanged();
        } }


        public HomeVM(ICurrentAccountService currentAccount)
        {
            _currentAccount = currentAccount;
            _contextFactory = new AppDbContextFactory();
            FullBooks();
        }

        public async Task FullBooks()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Books = new ObservableCollection<Book>(await context.Books.ToListAsync());
                SelectedBook = Books.First();
                foreach (var item in Books)
                {
                    if (!context.Entry(item)
                     .Collection(c => c.Reviews).IsLoaded)
                    {
                        await context.Entry(item)
                           .Collection(k => k.Reviews)
                           .LoadAsync();
                    }

                }
            }
        }





    }
}
