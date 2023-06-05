using MaterialDesignThemes.Wpf;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.core.ViewModel.Components.MessageBox;
using PackingMachine.core.ViewModel.ViewModelBase;
using System.Windows.Input;

namespace PackingMachine.core.ViewModel.MainViewModel
{
    public class MainViewModel: BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        private bool _isLoginSelected;
        public bool IsLoginSelected
        {
            get { return _isLoginSelected; }
            set { _isLoginSelected=value; OnPropertyChanged(nameof(IsLoginSelected)); }

        }
        private bool _isSettingSelected;
        public bool IsSettingSelected
        {
            get { return _isSettingSelected; }
            set { _isSettingSelected=value; OnPropertyChanged(nameof(IsSettingSelected)); }

        }
        private bool _isMainSuperSelected;
        public bool IsMainSuperSelected
        {
            get { return _isMainSuperSelected; }
            set { _isMainSuperSelected=value; OnPropertyChanged(nameof(IsMainSuperSelected)); }

        }
        private bool _isReportSelected;
        public bool IsReportSelected
        {
            get { return _isReportSelected; }
            set { _isReportSelected=value; OnPropertyChanged(nameof(IsReportSelected)); }

        }
        private bool _isHistorySelected;
        public bool IsHistorySelected
        {
            get { return _isHistorySelected; }
            set { _isHistorySelected=value; OnPropertyChanged(nameof(IsHistorySelected)); }

        }
        private bool _isWarningSelected;
        public bool IsWarningSelected
        {
            get { return _isWarningSelected; }
            set { _isWarningSelected=value; OnPropertyChanged(nameof(IsWarningSelected)); }

        }
        private bool _isHelpSelected;
        public bool IsHelpSelected
        {
            get { return _isHelpSelected; }
            set { _isHelpSelected=value; OnPropertyChanged(nameof(IsHelpSelected)); }

        }
        public ICommand LoginCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand MainSuperCommand { get; set; }
        public ICommand PackingReportCommand { get; set; }
        public ICommand WarningCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        // custome message box 
        private bool _isDialogOpen = false;
        public bool IsDialogOpen { get => _isDialogOpen; set { _isDialogOpen=value; OnPropertyChanged( ); } }

        public MessageBoxViewModel MessageBox { get; set; }
        // contructor
        private string _user;
        public string User { get => _user; set { _user=value; OnPropertyChanged(nameof(User)); } }
        public int SelectButton { get; set; }
        private string _login;
        public string Login { get => _login; set { _login=value; OnPropertyChanged(nameof(Login)); } }
        private string _ID;
        public string ID { get => _ID; set { _ID=value; OnPropertyChanged(nameof(ID)); } }
        public MainViewModel (NavigationStore navigationStore,INavigationService LoginNavigation,INavigationService SettingNavigation,INavigationService MainSuperNavigationService,INavigationService PackingReportNavigation,INavigationService WarningNavigation,INavigationService HistoryNavigation)
        {
            _navigationStore=navigationStore;
            LoginCommand=new NavigateCommand(LoginNavigation);
            SettingCommand=new NavigateCommand(SettingNavigation);
            MainSuperCommand=new NavigateCommand(MainSuperNavigationService);
            PackingReportCommand=new NavigateCommand(PackingReportNavigation);
            WarningCommand=new NavigateCommand(WarningNavigation);
            HistoryCommand=new NavigateCommand(HistoryNavigation);
            _navigationStore.CurrentViewModelChanged+=OnCurrentViewModelChanged;
            _navigationStore.CurrentButtonChanged+=_navigationStore_CurrentButtonChanged;
            // message box
            MessageBox=new MessageBoxViewModel( )
            {
                // ContentText = "You are Confirm",
                Icon=PackIconKind.User,
            };
            MessageBox.Confirm+=Confirm;
            MessageBox.Cancel+=Close;
            User="UserName";
            Login="ĐĂNG NHẬP";
        }
        private void Close ( )
        {
            MessageBox.ContentText="close";
            // IsDialogOpen = false;
        }
        private void Confirm ( )
        {
            MessageBox.ContentText="confirm";
        }

        private void OnCurrentViewModelChanged ( )
        {
            IsLoginSelected=false;
            IsSettingSelected=false;
            IsMainSuperSelected=false;
            IsReportSelected=false;
            IsHistorySelected=false;
            IsWarningSelected=false;
            IsHelpSelected=false;
            if ( CurrentViewModel is LoginViewModel ) IsLoginSelected=true;
            if ( CurrentViewModel is SettingViewModel.SettingViewModel ) IsSettingSelected=true;
            if ( CurrentViewModel is MainSuperViewModel.MainSuperViewModel ) IsMainSuperSelected=true;
            if ( CurrentViewModel is ReportViewModel.PackingReportViewModel ) IsReportSelected=true;
            if ( CurrentViewModel is HistoryViewModel.HistoryViewModel ) IsHistorySelected=true;
            if ( CurrentViewModel is WarningViewModel.WarningViewModel ) IsWarningSelected=true;
            /*
            if (CurrentViewModel is HelpViewModel.HelpViewModel) IsHelpSelected = true;*/
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        private void _navigationStore_CurrentButtonChanged ( )
        {
            SelectButton=_navigationStore.SelectButton;
            SwitchAnimationButton(SelectButton);
        }
        public void SwitchAnimationButton (int selectButton)
        {
            switch ( selectButton )
            {
                case 1:

                    User="UserName";
                    Login="ĐĂNG NHẬP";
                    ID="ID";
                    break;
                case 2:

                    User="Le Thi My Lien";
                    Login="ĐĂNG XUẤT";
                    ID="MLien";
                    break;
            }
        }

    }
}
