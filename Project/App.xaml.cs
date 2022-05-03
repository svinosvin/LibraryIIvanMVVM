using DataContext;
using Microsoft.Extensions.DependencyInjection;
using Project.Services;
using Project.Services.AunthenticationService;
using Project.Services.Generic;
using Project.Stores;
using Project.ViewModels;
using Project.Views;
using System;
using System.Windows;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App()
        {           
            _serviceProvider = createServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);  
        }

        private IServiceProvider createServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IAunthenticationService, AunthenticationService>();
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<AppDbContextFactory>();
            services.AddSingleton<IAccountDataService, AccountDataService>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowVM>()
            });


            return services.BuildServiceProvider();
        }
    }
}
