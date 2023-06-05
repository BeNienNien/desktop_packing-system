using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackingMachine;
using PackingMachine.core.Service;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.ViewModel;
using PackingMachine.core.ViewModel.MainViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAQCDesktopApplication.HostBuilder
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)     
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            });
            return host;

        }
    }
}
