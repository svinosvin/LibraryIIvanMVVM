using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Project.Base;
using Project.Services;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModels.UserVm
{
    public class FavouritesVM : ViewModel
    {

        private readonly ICurrentAccountService _currentAccount;
        private readonly IAccountDataService _accountDataService;
        private readonly IDataService<Book> _bookDataService;

        private AppDbContextFactory _contextFactory;

        public User ActiveUser { get; set; }
        public List<Favourite> AllFavourites { get; set; }
     

        public FavouritesVM(ICurrentAccountService currentAccount, IDataService<Book> bookDataService, IAccountDataService accountDataService)
        {
            _currentAccount = currentAccount;
            _accountDataService = accountDataService;
            _bookDataService = bookDataService;
            _contextFactory = new AppDbContextFactory();
            ActiveUser = _currentAccount.CurrentAccount as User;
            LoadHistory();
        }
        public async Task LoadHistory()
        {

            using (ApplicationDbContext _context = _contextFactory.CreateDbContext())
            {

                AllFavourites = await _context.Favourites.Include(x => x.Book).ThenInclude(x => x.Author).Where(x=> x.UserId == ActiveUser.Id).ToListAsync();
                int k = 0;
            }

        }
    }
}
