using Microsoft.Extensions.DependencyInjection;
using Project.Stores;
using Project.ViewModels;
using Project.ViewModels.UserVm;
using Project.Views;
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
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<NavigationStore>();
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowVM>()
            });
            _serviceProvider = services.BuildServiceProvider();

        }
        protected override void OnStartup(StartupEventArgs e)
        {


            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();

            MainWindow.Show();
            
        //    base.OnStartup(e);  
        }
    }
}
