using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackingMachine.core.ViewModel.HistoryViewModel;
using PackingMachine.core.ViewModel.MainSuperViewModel;
using PackingMachine.core.ViewModel.ReportViewModel;
using PackingMachine.core.ViewModel.SettingViewModel;
using PackingMachine.core.ViewModel.WarningViewModel;
using PackingMachine.HostBuilder;
using QAQCDesktopApplication.HostBuilder;
using System.Windows;

namespace PackingMachine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: Application
    {
        private readonly IHost _host;

        public App ( )
        {
            _host=CreateHostBuilder( ).Build( );
        }

        public static IHostBuilder CreateHostBuilder (string [] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                        .AddServices( )
                        .AddViewModels( )
                        .AddViews( )
                        .AddGRPCRead( );
        }
        protected override async void OnStartup (StartupEventArgs e)
        {
            _host.Start( );
            Window window = _host.Services.GetRequiredService<MainWindow>( );
            window.Show( );
            var supervisorVM = _host.Services.GetRequiredService<SupervisorViewModel>( );

            var SettingVM = _host.Services.GetRequiredService<SettingViewModel>( );
            var FirstSettingVM = _host.Services.GetRequiredService<FirstInforSettingViewModel>( );
            var MainSettingVM = _host.Services.GetRequiredService<MainSettingViewModel>( );
            var MainSuperVM = _host.Services.GetRequiredService<MainSuperViewModel>( );
            var EmployeeProVM = _host.Services.GetRequiredService<EmployeeProViewModel>( );
            var PackingReportProVM = _host.Services.GetRequiredService<PackingReportViewModel>( );
            var WarningVM = _host.Services.GetRequiredService<WarningViewModel>( );
            var HistoryVM = _host.Services.GetRequiredService<HistoryViewModel>( );
            await _host.Services.GetRequiredService<IBusControl>( ).StartAsync( );
            base.OnStartup(e);
        }
        protected override async void OnExit (ExitEventArgs e)
        {
            await _host.StopAsync( );
            _host.Dispose( );

            base.OnExit(e);
        }
    }
}
