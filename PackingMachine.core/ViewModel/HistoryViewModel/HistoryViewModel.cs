using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.Api;
using PackingMachine.core.Domain.Model.Api.Shift;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.ViewModel.ViewModelBase;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PackingMachine.core.ViewModel.HistoryViewModel
{
    public class HistoryViewModel: BaseViewModel
    {
        private DateTime _startTime;
        public DateTime startTime
        {
            get { return _startTime; }
            set { _startTime=value; OnPropertyChanged(nameof(startTime)); }
        }
        private DateTime _endTime;
        public DateTime endTime
        {
            get { return _endTime; }
            set { _endTime=value; OnPropertyChanged(nameof(endTime)); }
        }

        private IApiServices _apiServices;

        public ObservableCollection<PackingUnit> packingUnits;
        public ObservableCollection<PackingUnit> PackingUnits
        {
            get => packingUnits;
            set
            {
                packingUnits=value;
                OnPropertyChanged(nameof(PackingUnits));
            }
        }
        //cho nay co xu ly lastname kh
        public ObservableCollection<Employee> allEmployees;
        public ObservableCollection<Employee> AllEmployees
        {
            get => allEmployees;
            set
            {
                allEmployees=value;
                OnPropertyChanged(nameof(allEmployees));
            }
        }
        public ObservableCollection<ItemShift> items;
        public ObservableCollection<ItemShift> Items
        {
            get => items;
            set
            {
                items=value;
                OnPropertyChanged(nameof(items));
            }
        }

        public ObservableCollection<ShiftReport> listShifts;
        public ObservableCollection<ShiftReport> ListShifts
        {
            get => listShifts;


            set
            {
                listShifts=value;
                OnPropertyChanged(nameof(listShifts));
            }
        }



        public ICommand HistoryCommand { get; set; }

        public HistoryViewModel (IApiServices apiService)
        {
            startTime=DateTime.Now.AddDays(-7);
            endTime=DateTime.Now;
            _apiServices=apiService;
            //GetShift( );
            HistoryCommand=new RelayCommand(async ( ) =>
            {
                ListShifts=new ObservableCollection<ShiftReport>( );
                ListShifts.Clear( );
                GetShift( );
            });

        }


        public async void GetShift ( )
        {
            await Task.Delay(1);
            ListShifts=await _apiServices.GetShift("",startTime.ToString("yyyy-MM-dd"),endTime.ToString("yyyy-MM-dd"));
        }
        public async void GetEmployee ( )
        {
            await Task.Delay(1);
            AllEmployees=await _apiServices.GetEmployee("");
        }
        private void OnCurrentViewModelChanged ( )
        {
            OnPropertyChanged( );
        }
        public override void Dispose ( )
        {
            base.Dispose( );
        }
    }
}

