using DataContext;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;
using Project.Services;
using Project.Services.AccountService;
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
            services.AddSingleton<AppDbContextFactory>();
            services.AddSingleton<IAccountDataService, AccountDataService>();
            services.AddSingleton<IWorkerDataService, WorkerDataService>();
            services.AddSingleton<IWorkerAuthenticationService, WorkerAuthenticationService>();
            services.AddSingleton<ICurrentAccountService, CurrentAccountService>();
            services.AddSingleton<NonQueryDataService<Book>>();
            services.AddSingleton<IDataService<Book>, BookDataService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();


            services.AddSingleton<LoginVM>();
            services.AddSingleton<RegistrationVM>();

            services.AddSingleton<MainUserVM>();
            services.AddSingleton<MainAdmVM>();
            services.AddSingleton<MainWorkerVM>();
            services.AddSingleton<MainWindowVM>();


            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowVM>()
            });


            return services.BuildServiceProvider();
        }
    }
}
