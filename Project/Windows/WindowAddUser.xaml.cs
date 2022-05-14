﻿using Project.Services.AccountService;
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
    /// Логика взаимодействия для WindowAddUser.xaml
    /// </summary>
    public partial class WindowAddUser : Window
    {
        private readonly ICurrentAccountService _currentAccount;


        public WindowAddUser()
        {
            InitializeComponent();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
        public WindowAddUser(ICurrentAccountService currentAccount)
        {
            InitializeComponent();
            this._currentAccount = currentAccount;
            DataContext = new CreateNewWorkerVM(_currentAccount);

        }
    }
}
