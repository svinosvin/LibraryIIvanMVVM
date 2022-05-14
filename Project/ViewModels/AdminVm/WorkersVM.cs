using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Project.Base;
using Project.Commands.Helpers;
using Project.Services.AccountService;
using Project.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModels.AdminVm
{
    public class WorkersVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private AppDbContextFactory dbContextFactory;
        public ICollection<Worker> Workers { get; set; }
        public WorkersVM(ICurrentAccountService currentAccount)
        {
            _currentAccount = currentAccount;
            dbContextFactory = new AppDbContextFactory();
            fullList();
            SelectedItem = Workers.First();
        }
        private Worker _selectedItem;
        public Worker SelectedItem
        {
            get { return _selectedItem; }
            set{ _selectedItem = value; OnPropertyChanged(); }
        }

        public async Task fullList()
        {
            using(ApplicationDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                Workers = await dbContext.Workers.Include(x=>x.Person).Where((x)=>(_currentAccount.CurrentAccount as Worker).Id != x.Id).ToListAsync();
              

            }
        }
        public ICommand DeleteWorker
        {
            get
            {
                return new RelayCommand(async(x) =>
                {

                    using (ApplicationDbContext dbContext = dbContextFactory.CreateDbContext())
                    {
                         
                         dbContext.Workers.Remove(SelectedItem);
                        Workers.Remove(SelectedItem);
                         await dbContext.SaveChangesAsync();

                    }
                }, (x) => SelectedItem!=null); ;
            }
        }
        public ICommand AddWorker
        {
            get
            {
                return new RelayCommand((x) =>
                {

                    WindowAddUser addUser = new WindowAddUser(_currentAccount);
                    addUser.Show();

                });
            }
        }
        public ICommand Update
        {
            get
            {
                return new RelayCommand(async(x) =>
                {

                    await fullList();
                    OnPropertyChanged(nameof(Workers));
                    

                });
            }
        }

    }

}
