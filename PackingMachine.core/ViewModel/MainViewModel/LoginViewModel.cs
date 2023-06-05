using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.core.ViewModel.Components.MessageBox;
using PackingMachine.core.ViewModel.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PackingMachine.core.ViewModel.MainViewModel
{
    public class LoginViewModel: BaseViewModel
    {
        private string _userName;

        public string UserName      
        {
            get { return _userName; }
            set 
            {
                _userName = value; 
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string Password { private get; set; }
      /*  private SecureString _password;
        public SecureString Password  
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }*/
        public bool IsLogined;
        public ICommand ConfirmLoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        // notification
        private bool _isDialogOpen = false;
        public bool IsDialogOpen { get => _isDialogOpen; set { _isDialogOpen = value; OnPropertyChanged(); } }
        public MessageBoxViewModel MessageBox { get; set; }

        public LoginViewModel(NavigationStore navigationStore, INavigationService LoginNavigation)
        {
            IsLogined = false;
            UserName = "";
            //ConfirmLoginCommand = new NavigateCommand(LoginNavigation);
            ConfirmLoginCommand = new RelayCommand(async () => await Task.Run(() => Login()));
            PasswordChangedCommand = new RelayCommand(async () => await Task.Run(() => Login()));
            //PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            MessageBox = new MessageBoxViewModel();
            MessageBox.Confirm += DialogConfirm;
            //MessageBox.Cancel += DialogClose;
            //LoggingCommand = new RelayCommand(async () => await View());
        }
        private void Login()
        {
            IsDialogOpen = true;
            MessageBox.ContentText = "ĐĂNG NHẬP THÀNH CÔNG";
        }
        private void DialogConfirm() {
            UserName = "";
            IsDialogOpen = false;
          //  CollectionViewSource.GetDefaultView().Refresh();
        }
        void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            //Now go ahead and check the user name and password
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public override void Dispose()
        {
            base.Dispose();
        }


    }
}
