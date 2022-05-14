using Models.Models;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowCreateBook.xaml
    /// </summary>
    public partial class WindowCreateBook : Window
    {
        public IDataService<Book> _bookDataService;
        
        public Book Book { get; set; }
        public WindowCreateBook()
        {
            InitializeComponent();
        }
        public WindowCreateBook(IDataService<Book> _bookDataService)
        {
            InitializeComponent();
            DataContext = new CreateBookVm(_bookDataService);
        }
    }
}
