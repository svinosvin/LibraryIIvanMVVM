using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Models.Models;
using Project.Base;
using Project.Commands.Helpers;
using Project.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace Project.ViewModels.WorkerVm
{
    public class RequestsVM : ViewModel
    {
        private readonly ICurrentAccountService _currentAccount;
        private AppDbContextFactory appDbContext;
        public ICollection<HistoryTransactions> Histories { get; set; }
        private HistoryTransactions _transaction;
        public HistoryTransactions Transaction
        {
            get
            {
                return _transaction;
            }
            set
            {
                _transaction = value;
                OnPropertyChanged();
            }
        }
        public RequestsVM(ICurrentAccountService currentAccount)
        {
            appDbContext = new AppDbContextFactory();
            _currentAccount = currentAccount;
            FullReviews();
        }
        public async Task FullReviews()
        {
            using (ApplicationDbContext _context = appDbContext.CreateDbContext())
            {
                List<HistoryTransactions> hist = await _context.Transactions.Include(x => x.Book).ThenInclude(x => x.Author).Include(x => x.User).ThenInclude(x => x.Person).ToListAsync();
                Histories = hist.Where(x => x.Accept == false).ToList();

            }
        }



        public ICommand Accept
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    using (ApplicationDbContext _context = appDbContext.CreateDbContext())
                    {
                        Transaction.Accept = true;
                        Histories.Remove(Transaction);
                        _context.Transactions.Update(Transaction);
                        _context.SaveChanges();
                        await FullReviews();
                        OnPropertyChanged(nameof(Histories));
                        MessageBox.Show("Успешно принято!");

                    }

                }, (x) => Transaction != null
                );


            }
        }
        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(async (x) =>
                {
                    using (ApplicationDbContext _context = appDbContext.CreateDbContext())
                    {
                        _context.Transactions.Remove(Transaction);
                        Histories.Remove(Transaction);
                        MessageBox.Show("Заявка успешно отклонена!");
                        OnPropertyChanged(nameof(Histories));

                    }

                }, (x) => Transaction != null
                );


            }
        }
        public ICommand Word
        {
            get
            {
                return new RelayCommand( (x) =>
                {
                    Button_Word();

                } 
                );


            }
        }
        public ICommand Txt
        {
            get
            {
                return new RelayCommand((x) =>
                {
                    Button_Txt();

                }
                );


            }
        }
        public ICommand Print
        {
            get
            {
                return new RelayCommand((x) =>
                {
                    Button_Print();

                }
                );


            }
        }
        public ICommand Excel
        {
            get
            {
                return new RelayCommand((x) =>
                {
                    Button_Excel();

                }
                );


            }
        }

        public ICommand Xml
        {
            get
            {
                return new RelayCommand((x) =>
                {
                    Button_Xaml();
                    MessageBox.Show("Успешно добавленно!");
                },
                 (x)=>Transaction!=null);


            }
        }

        private void Button_Word()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                app.Visible = true;
                Microsoft.Office.Interop.Word.Document worddoc;
                object wordobj = System.Reflection.Missing.Value;
                worddoc = app.Documents.Add(ref wordobj);

                if (Histories != null && Histories.Count>0)
                {
                    string info = "";

                    foreach (var item in Histories)
                    {

                        info += $"|{item.User.Login} {item.User.Person.Email} Почта: {item.User.Person.TelNumber}|\n";
                        info += $"Книга:|Название:{item.Book.Title}, Кол-во книг на складе:{item.Book.Count}, Осталось:{(item.End.Date - DateTime.Today).Days} Дней|\n";
                    }
                    app.Selection.TypeText(info);
                }
                else
                {
                    app.Selection.TypeText("Активных заявок больше нет!");
                }


            }
            catch
            {
                MessageBox.Show("Что-то пошло не так!");
            }


        }
        private void Button_Txt()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                using (StreamWriter stream = new StreamWriter(fs))
                {
                    if (Histories != null && Histories.Count > 0)
                    {
                        string info = "";

                        stream.WriteLine(info);

                        foreach (var item in Histories)
                        {
                            info += $"|{item.User.Login} {item.User.Person.Email} Почта: {item.User.Person.TelNumber}|\n";
                            info += $"Книга:|Название:{item.Book.Title}, Кол-во книг на складе:{item.Book.Count}, Осталось:{(item.End.Date - DateTime.Today).Days} Дней|\n";
                            stream.WriteLine(info);
                        }

                        MessageBox.Show("Успешно сохранено!");
                    }
                    else
                    {
                        stream.WriteLine("Заявок больше нет!");
                    }


                }
            }

        }
        private void Button_Print()
        {


            string result = "Строка 1\n\n";
            result += "Строка 2\nСтрока 3";
            // объект для печати
            PrintDocument printDocument = new PrintDocument();
            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler;
            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();
            // установка объекта печати для его настройки

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == true)
                printDocument.Print(); ; // печатаем

        }
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            if (Histories != null && Histories.Count > 0)
            {
                string info = "";

                foreach (var item in Histories)
                {

                    info += $"|{item.User.Login} {item.User.Person.Email} Почта: {item.User.Person.TelNumber}|\n";
                    info += $"Книга:|Название:{item.Book.Title}, Кол-во книг на складе:{item.Book.Count}, Осталось:{(item.End.Date - DateTime.Today).Days} Дней|\n";
                }
                e.Graphics.DrawString(info, new Font("Arial", 14), System.Drawing.Brushes.Black, 0, 0);
                MessageBox.Show("Успешно сохранено!");
            }
            else
            {
                e.Graphics.DrawString("Заявок больше нет", new Font("Arial", 14), System.Drawing.Brushes.Black, 0, 0);
            }

        }
        private void Button_Excel()
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "Имя";
            ExcelApp.Cells[1, 2] = "Фамилия";
            ExcelApp.Cells[1, 3] = "Email";
            ExcelApp.Cells[1, 4] = "Login";
            ExcelApp.Cells[1, 5] = "Книга";
            ExcelApp.Cells[1, 6] = "Telephone";
            ExcelApp.Cells[1, 7] = "Кем принято";
            ExcelApp.Cells[1, 8] = "Email1";



            for (int i = 0; i < Histories.Count; i++)
            {
                ExcelApp.Cells[i + 2, 1] = Histories.ElementAt(i).User.Person.Name;
                ExcelApp.Cells[i + 2, 2] = Histories.ElementAt(i).User.Person.Surname;
                ExcelApp.Cells[i + 2, 3] = Histories.ElementAt(i).User.Person.Email;
                ExcelApp.Cells[i + 2, 4] = Histories.ElementAt(i).User.Login;
                ExcelApp.Cells[i + 2, 5] = Histories.ElementAt(i).User.Person.TelNumber;
                ExcelApp.Cells[i + 2, 6] = Histories.ElementAt(i).User.Person.TelNumber;
                ExcelApp.Cells[i + 2, 7] = (_currentAccount.CurrentAccount as Worker).Login;
                ExcelApp.Cells[i + 2, 8] = (_currentAccount.CurrentAccount as Worker).Person.Email;



            }


            ExcelApp.Visible = true;
        }
        private void Button_Xaml()
        {
            XmlDocument Document = new XmlDocument();
            XmlDeclaration declaration = Document.CreateXmlDeclaration("1.0", "UTF-8","yes");
            XmlComment comment = Document.CreateComment("SelectedUserInfo");
            XmlElement root = Document.CreateElement("Persons");
            XmlElement person = Document.CreateElement("Person");

            XmlAttribute username = Document.CreateAttribute("username");
            XmlAttribute name = Document.CreateAttribute("name");
            XmlAttribute surname = Document.CreateAttribute("surname");
            XmlAttribute email = Document.CreateAttribute("email");
            XmlAttribute telnumber = Document.CreateAttribute("telnumber");

            User user = Transaction.User;

            username.Value = user.Login;
            name.Value = user.Person.Name;
            surname.Value = user.Person.Surname;
            email.Value = user.Person.Email;
            telnumber.Value = user.Person.TelNumber;

            Document.AppendChild(declaration);
            Document.AppendChild(comment);
            Document.AppendChild(root);
            root.AppendChild(person);
            person.Attributes.Append(username);
            person.Attributes.Append(name);
            person.Attributes.Append(surname);
            person.Attributes.Append(email);
            person.Attributes.Append(telnumber);
            Document.Save("XmlFile.xml");





        }

    }
}   
