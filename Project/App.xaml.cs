using Project.Stores;
using Project.ViewModels.UserVm;
using Project.Views.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            NavigationStore ns = new NavigationStore();
            ns.CurrentViewModel = new HomeVM(); 
            MainWindow window = new MainWindow() { DataContext = new MainWindowVM(ns) };

            window.Show();
        //    base.OnStartup(e);  
        }
    }
}
