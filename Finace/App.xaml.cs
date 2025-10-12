using Finace.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Finace.Options;
using Finace.Service.Interfaces;
using Finace.Service;
using Finace.Views;

namespace Finace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.Configure<Settings>(configuration);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<HomePage>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<DashboardPage>();
            services.AddSingleton<HomePage>();
            services.AddSingleton<IMainViewModel, MainViewModel>();
            services.AddSingleton<ITransactionsService, TransactionService>();
            services.AddSingleton<IHomeViewModel, HomeViewModel>();
            services.AddSingleton<ISettingsViewModel, SettingsViewModel>();
            services.AddSingleton<IDashboardViewModel, DashboardViewModel>();
            services.AddSingleton<MainWindow>();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            // Dispose of services if needed
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
