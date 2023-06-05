using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.core.ViewModel.MainSuperViewModel;
using PackingMachine.core.ViewModel.MainViewModel;
using PackingMachine.core.ViewModel.ReportViewModel;
using PackingMachine.core.ViewModel.SettingViewModel;
using PackingMachine.core.ViewModel.ViewModelBase;
using System;

namespace PackingMachine.core.Service
{
    public class NavigationService<TViewModel>: INavigationService where TViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        public NavigationService (NavigationStore navigationStore,Func<TViewModel> createViewModel)
        {
            _navigationStore=navigationStore;
            _createViewModel=createViewModel;
        }
        public void Navigate ( )
        {
            _navigationStore.CurrentViewModel=_createViewModel( );
        }
        public void SelectViewModel ( )
        {
            switch ( _createViewModel )
            {
                case Func<LoginViewModel>:
                    _navigationStore.SelectButton=1;
                    break;
                case Func<SettingViewModel>:
                    _navigationStore.SelectButton=2;
                    break;
                case Func<MainSuperViewModel>:
                    _navigationStore.SelectButton=2;
                    break;
                case Func<PackingReportViewModel>:
                    _navigationStore.SelectButton=2;
                    break;
            }
        }
    }
}
