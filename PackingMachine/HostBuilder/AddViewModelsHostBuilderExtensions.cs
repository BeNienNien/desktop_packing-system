using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackingMachine.core.Service;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.core.ViewModel.HistoryViewModel;
using PackingMachine.core.ViewModel.MainSuperViewModel;
using PackingMachine.core.ViewModel.MainViewModel;
using PackingMachine.core.ViewModel.ReportViewModel;
using PackingMachine.core.ViewModel.SettingViewModel;
using PackingMachine.core.ViewModel.WarningViewModel;
using System;

namespace PackingMachine.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels (this IHostBuilder host)
        {
            host.ConfigureServices(services =>

            {

                services.AddSingleton<LoginViewModel>((IServiceProvider serviceprovider) =>
                {
                    var store = serviceprovider.GetRequiredService<NavigationStore>( );
                    return new LoginViewModel(store,CreateSettingNavigationService(serviceprovider,store));
                });

                services.AddSingleton<SettingViewModel>((IServiceProvider serviceprovider) =>
                {
                    var goodExportStore = serviceprovider.GetRequiredService<NavigationStore>( );
                    return new SettingViewModel(goodExportStore,
                        FisrtInforSettingNavigationService(serviceprovider,goodExportStore),
                        MainSettingNavigationService(serviceprovider,goodExportStore));
                });
                services.AddSingleton<FirstInforSettingViewModel>( );
                services.AddSingleton<MainSettingViewModel>( );
                services.AddSingleton<PackingReportViewModel>( );
                services.AddSingleton<WarningViewModel>( );
                services.AddSingleton<HistoryViewModel>( );
                services.AddSingleton<MainSuperViewModel>((IServiceProvider serviceprovider) =>
                {
                    var MainSuper = serviceprovider.GetRequiredService<NavigationStore>( );
                    return new MainSuperViewModel(MainSuper,
                        CreateSupervisorNavigationService(serviceprovider,MainSuper),
                        CreateEmployeeProNavigationService(serviceprovider,MainSuper));
                });
                services.AddSingleton<SupervisorViewModel>( );
                services.AddSingleton<EmployeeProViewModel>( );

                services.AddSingleton<MainViewModel>((IServiceProvider serviceprovider) =>
                {
                    var Mainstore = serviceprovider.GetRequiredService<NavigationStore>( );
                    return new MainViewModel(Mainstore,CreateLoginNavigationService(serviceprovider,Mainstore),CreateSettingNavigationService(serviceprovider,Mainstore),MainSuperNavigationService(serviceprovider,Mainstore),CreatePackingReportNavigationService(serviceprovider,Mainstore),CreateWarningNavigationService(serviceprovider,Mainstore),CreateHistoryNavigationService(serviceprovider,Mainstore));
                });
            });

            return host;
        }
        private static INavigationService CreateLoginNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<LoginViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<LoginViewModel>( ));
        }
        private static INavigationService CreateSettingNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<SettingViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<SettingViewModel>( ));
        }
        private static INavigationService FisrtInforSettingNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<FirstInforSettingViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<FirstInforSettingViewModel>( ));
        }
        private static INavigationService MainSettingNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<MainSettingViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<MainSettingViewModel>( ));
        }
        private static INavigationService CreateSupervisorNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<SupervisorViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<SupervisorViewModel>( ));
        }
        private static INavigationService MainSuperNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<MainSuperViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<MainSuperViewModel>( ));
        }

        private static INavigationService CreateEmployeeProNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<EmployeeProViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<EmployeeProViewModel>( ));
        }

        private static INavigationService CreatePackingReportNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<PackingReportViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<PackingReportViewModel>( ));
        }
        private static INavigationService CreateWarningNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<WarningViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<WarningViewModel>( ));
        }
        private static INavigationService CreateHistoryNavigationService (IServiceProvider serviceProvider,NavigationStore store)
        {
            return new NavigationService<HistoryViewModel>(store,
                ( ) => serviceProvider.GetRequiredService<HistoryViewModel>( ));
        }
    }
}
