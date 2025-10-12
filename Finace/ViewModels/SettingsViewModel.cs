using Finace.Service;
using Finace.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Finace.ViewModels
{
    public interface ISettingsViewModel
    {
    }

    public class SettingsViewModel : ISettingsViewModel
    {
        private readonly ILogger<MainViewModel> _logger;
        private readonly IConfiguration _config;
        private readonly ITransactionsService _service;

        private string _title;
        public string Title { get { return _title; } set { _title = value; } }

        public SettingsViewModel(ILogger<MainViewModel> logger, IConfiguration configuration, ITransactionsService service)
        {
            _title = "345sett";
            _logger = logger;
            _config = configuration;
            _service = service;
        }
    }
}
