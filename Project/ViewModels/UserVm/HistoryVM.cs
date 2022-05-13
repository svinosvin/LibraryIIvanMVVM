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
    public class HistoryVM : ViewModel
    {

        private readonly ICurrentAccountService _currentAccount;
        private readonly IAccountDataService _accountDataService;
        private readonly IDataService<Book> _bookDataService;

     
        private AppDbContextFactory _contextFactory;

        public User ActiveUser { get; set; }
        private ICollection<HistoryTransactions> AllHistories;
        public List<HistoryTransactions> ActiveTransactions { get; set; } = new List<HistoryTransactions>();
        public List<HistoryTransactions> NotActiveTransactions { get; set; } = new List<HistoryTransactions>();

        public HistoryVM(ICurrentAccountService currentAccount,  IDataService<Book> bookDataService, IAccountDataService accountDataService)
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


                AllHistories = await _context.Transactions.Include(x => x.Book).ThenInclude(x=>x.Author).Where(x => x.UserId == ActiveUser.Id).ToListAsync();
                ActiveTransactions.AddRange(AllHistories.Where(x => x.Accept == true).OrderBy(x=>x.Begin));
                ActiveTransactions.Sort((x, y) => DateTime.Compare(y.Begin, x.Begin));
                NotActiveTransactions.AddRange(AllHistories.Where(x => x.Accept == false).ToList()) ;
                NotActiveTransactions.Sort((x, y) => DateTime.Compare(y.Begin, x.Begin));

            }

        }







    }
}
