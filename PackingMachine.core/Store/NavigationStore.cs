using PackingMachine.core.ViewModel.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Store
{
    public class NavigationStore
    {
        private int _selectButton;
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
        public int SelectButton
        {
            get => _selectButton;
            set
            {
                _selectButton = value;
                OnCurrentButtonChanged();
            }
        }
        public void Close()
        {
            CurrentViewModel = null;
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
        public event Action CurrentButtonChanged;

        private void OnCurrentButtonChanged()
        {
            CurrentButtonChanged?.Invoke();
        }
    }
}
