using Finace.Service.Interfaces;
using Finace.Service;
using Finace.ViewModels;
using Finace.Views;
using Finace.Views.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Finace
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            // Logging
            services.AddLogging();

            // Pages 
            services.AddSingleton<HomePage>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<DashboardPage>();

            services.AddSingleton<IncomePage>();
            services.AddSingleton<CostPage>();
            services.AddSingleton<TransferPage>();

            // ViewModels
            services.AddSingleton<IMainViewModel, MainViewModel>();
            services.AddSingleton<IHomeViewModel, HomeViewModel>();
            services.AddSingleton<ISettingsViewModel, SettingsViewModel>();
            services.AddSingleton<IDashboardViewModel, DashboardViewModel>();

            // Services
            services.AddSingleton<ITransactionsService, TransactionService>();

            // Main window
            services.AddSingleton<MainWindow>();

            return services;
        }
    }
}
