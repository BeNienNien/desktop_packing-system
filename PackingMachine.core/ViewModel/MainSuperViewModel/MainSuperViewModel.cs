using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.core.ViewModel.ViewModelBase;
using System.Windows.Input;

namespace PackingMachine.core.ViewModel.MainSuperViewModel
{
    public class MainSuperViewModel: BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public ViewModel.ViewModelBase.BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand SupervisorCommand { get; set; }
        public ICommand LoadChartCommand { get; set; }
        public MainSuperViewModel (NavigationStore navigationStore,INavigationService CreateSupervisorNavigationService,INavigationService CreateEmployeeProNavigationService)
        {
            _navigationStore=navigationStore;
            SupervisorCommand=new NavigateCommand(CreateSupervisorNavigationService);
            LoadChartCommand=new NavigateCommand(CreateEmployeeProNavigationService);
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
