using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.core.ViewModel.ViewModelBase;
using System.Windows.Input;

namespace PackingMachine.core.ViewModel.SettingViewModel
{
    public class SettingViewModel: BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public ViewModel.ViewModelBase.BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand FisrtInforSettingNavigationCommand { get; set; }
        public ICommand MainSettingNavigationCommand { get; set; }

        public SettingViewModel (NavigationStore navigationStore,INavigationService FisrtInforSettingNavigationService,
            INavigationService MainSettingNavigationService)
        {
            _navigationStore=navigationStore;
            FisrtInforSettingNavigationCommand=new NavigateCommand(FisrtInforSettingNavigationService);
            MainSettingNavigationCommand=new NavigateCommand(MainSettingNavigationService);
            _navigationStore.CurrentViewModelChanged+=OnCurrentViewModelChanged;
        }
        private void OnCurrentViewModelChanged ( )
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public override void Dispose ( )
        {
            base.Dispose( );
        }
    }
}
