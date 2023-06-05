using MaterialDesignThemes.Wpf;
using PackingMachine.core.ViewModel.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PackingMachine.core.ViewModel.Components.MessageBox
{
    public delegate void NotifyWarning();  // delegate
    public class WarningBoxViewModel : BaseViewModel
    {
        private string _contentText;
        private PackIconKind _icon = PackIconKind.User;
        public static WarningBoxViewModel Instance => new WarningBoxViewModel();
        public string ContentText { get => _contentText; set { _contentText = value; OnPropertyChanged(); } }
        public PackIconKind Icon { get => _icon; set { _icon = value; OnPropertyChanged(); } }
        public ICommand CloseWarningCommand { get; set; }
        public event NotifyWarning WarningConfirm;
#pragma warning disable CS8618
        public WarningBoxViewModel()
        {
            CloseWarningCommand = new RelayCommand(() => WarningConfirm?.Invoke());
        }

#pragma warning restore
    }
}
