using PackingMachine.core.Doman.Model;
using PackingMachine.core.ViewModel.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PackingMachine.core.Store
{
    public class PageStoreMachine2: BaseViewModel
    {

        private ObservableCollection<InforOders> _InforOrder;

        public ObservableCollection<InforOders> InforOrder { get => _InforOrder; set { _InforOrder = value; OnCurrentPageChanged(); } }

        public event Action CurrentPageChanged;

        private void OnCurrentPageChanged()
        {
            CurrentPageChanged?.Invoke();
        }
    }
}
