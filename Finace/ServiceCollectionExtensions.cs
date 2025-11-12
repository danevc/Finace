using Finace.Service;
using Finace.Service.Interfaces;
using Finace.ViewModels;
using Finace.Views;
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
            services.AddSingleton<BudgetPage>();
            services.AddSingleton<DashboardPage>();

            // ViewModels
            services.AddSingleton<IDashboardViewModel, DashboardViewModel>();
            services.AddSingleton<IBudgetViewModel, BudgetViewModel>();

            // Services
            services.AddSingleton<IStatisticService, StatisticService>();
            services.AddSingleton<ITransactionsService, TransactionService>();

            // Main window
            services.AddSingleton<MainWindow>();

            return services;
        }
    }
}
