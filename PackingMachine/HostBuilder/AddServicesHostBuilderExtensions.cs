using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackingMachine.core.Service;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.Core.Services;

namespace PackingMachine.HostBuilder
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices (this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<NavigationStore>( );
                //services.AddSingleton<NavigationStore>( );
                services.AddSingleton<PageStoreMachine1>( );
                services.AddSingleton<PageStoreMachine2>( );
                services.AddSingleton<PageStoreMachine3>( );
                services.AddSingleton<PageStoreMachine4>( );
                services.AddSingleton<PageStoreMachine5>( );
                services.AddSingleton<PageStoreMachine6>( );
                services.AddSingleton<IExcelExportService,ExportExcelService>( );
                services.AddTransient<IApiServices,ApiServices>( );
            });

            return host;
        }
    }
}
