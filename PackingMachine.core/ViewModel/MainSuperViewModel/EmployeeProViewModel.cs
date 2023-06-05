using MassTransit;
using PackingMachine.core.Components;
using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.Api;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.ViewModel.ViewModelBase;
using PackingSystemServiceContainers;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using static PackingMachine.core.ViewModel.MainSuperViewModel.SupervisorViewModel;

namespace PackingMachine.core.ViewModel.MainSuperViewModel
{
    public class EmployeeProViewModel: BaseViewModel
    {
        private IApiServices _apiServices;

        public static ReceiveValueMessage Sender;
        #region bien


        private ObservableCollection<DataChart> _dataList1;
        public ObservableCollection<DataChart> DataList1 { get => _dataList1; set { _dataList1=value; OnPropertyChanged(nameof(DataList1)); } }

        private ObservableCollection<DataChart> _dataList2;
        public ObservableCollection<DataChart> DataList2 { get => _dataList2; set { _dataList2=value; OnPropertyChanged(nameof(DataList2)); } }
        private ObservableCollection<DataChart> _dataList3;
        public ObservableCollection<DataChart> DataList3 { get => _dataList3; set { _dataList3=value; OnPropertyChanged(nameof(DataList3)); } }
        private ObservableCollection<DataChart> _dataList4;
        public ObservableCollection<DataChart> DataList4 { get => _dataList4; set { _dataList4=value; OnPropertyChanged(nameof(DataList4)); } }
        private ObservableCollection<DataChart> _dataList5;
        public ObservableCollection<DataChart> DataList5 { get => _dataList5; set { _dataList5=value; OnPropertyChanged(nameof(DataList5)); } }
        private ObservableCollection<DataChart> _dataList6;
        public ObservableCollection<DataChart> DataList6 { get => _dataList6; set { _dataList6=value; OnPropertyChanged(nameof(DataList6)); } }
        public ICommand ChartCommand { get; set; }
        private IBusControl _busControl;

        private DateTime _timeStamp;
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp=value; OnPropertyChanged(nameof(TimeStamp)); }
        }
        public Double _executionTime;
        public Double ExecutionTime
        {
            get { return _executionTime; }
            set { _executionTime=value; OnPropertyChanged(nameof(ExecutionTime)); }
        }
        private DateTime dateTimeStart;
        public DateTime DateTimeStart { get => dateTimeStart; set { dateTimeStart=value; OnPropertyChanged( ); } }
        private DateTime dateTimeStop;
        public DateTime DateTimeStop { get => dateTimeStop; set { dateTimeStop=value; OnPropertyChanged( ); } }
        private Employee employee;
        public Employee Employee { get => employee; set { employee=value; OnPropertyChanged( ); } }

        #endregion
        public EmployeeProViewModel (IApiServices apiService)
        {
            _apiServices=apiService;
            ChartCommand=new RelayObjectCommand<StackPanel>((p) => { return p==null ? false : true; },async (p) => LoadChart(p));
            DateTimeStart=DateTime.UtcNow;
            DateTimeStop=DateTime.UtcNow;
            DataList1=new ObservableCollection<DataChart>( );
            //DataList2=new ObservableCollection<DataChart>( );
            //DataList3=new ObservableCollection<DataChart>( );
            //DataList4=new ObservableCollection<DataChart>( );
            //DataList5=new ObservableCollection<DataChart>( );
            //DataList6=new ObservableCollection<DataChart>( );
            Sender=new ReceiveValueMessage(GetCycleMessage);
        }
        public async void LoadChart (StackPanel p)
        {
            if ( DataList1.Count>0 )
            {
                EmployeeChart chart = new EmployeeChart( );
                foreach ( var data in DataList1 )
                {
                    chart.Title="Chuyền 1 "+"Ngày "+(DateTime.Now.Day)+" tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year;
                    chart.ExecutionTime.Add(data.ExecutionTime);
                    chart.TimeStandard.Add(40);
                    chart.Labels.Add(data.TimeStamp.ToString( ));
                }
                p.Children.Add(chart);
            }
            //if ( DataList2.Count>0 )
            //{
            //    EmployeeChart chart = new EmployeeChart( );
            //    foreach ( var data in DataList2 )
            //    {
            //        chart.Title="Chuyền 2 "+"Ngày "+(DateTime.Now.Day)+" tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year;
            //        chart.ExecutionTime.Add(data.ExecutionTime);
            //        chart.TimeStandard.Add(5);
            //        chart.Labels.Add(data.TimeStamp.ToString( ));
            //    }
            //    p.Children.Add(chart);
            //}
            //if ( DataList3.Count>0 )
            //{
            //    EmployeeChart chart = new EmployeeChart( );
            //    foreach ( var data in DataList3 )
            //    {
            //        chart.Title="Chuyền 3 "+"Ngày "+(DateTime.Now.Day)+" tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year;
            //        chart.ExecutionTime.Add(data.ExecutionTime);
            //        chart.TimeStandard.Add(5);
            //        chart.Labels.Add(data.TimeStamp.ToString( ));
            //    }
            //    p.Children.Add(chart);
            //}
            //if ( DataList4.Count>0 )
            //{
            //    EmployeeChart chart = new EmployeeChart( );
            //    foreach ( var data in DataList4 )
            //    {
            //        chart.Title="Chuyền 4 "+"Ngày "+(DateTime.Now.Day)+" tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year;
            //        chart.ExecutionTime.Add(data.ExecutionTime);
            //        chart.TimeStandard.Add(5);
            //        chart.Labels.Add(data.TimeStamp.ToString( ));
            //    }
            //    p.Children.Add(chart);

            //}
            //if ( DataList5.Count>0 )
            //{
            //    EmployeeChart chart = new EmployeeChart( );
            //    foreach ( var data in DataList5 )
            //    {
            //        chart.Title="Chuyền 5 "+"Ngày "+(DateTime.Now.Day)+" tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year;
            //        chart.ExecutionTime.Add(data.ExecutionTime);
            //        chart.TimeStandard.Add(5);
            //        chart.Labels.Add(data.TimeStamp.ToString( ));
            //    }
            //    p.Children.Add(chart);
            //}
            //if ( DataList6.Count>0 )
            //{
            //    EmployeeChart chart = new EmployeeChart( );
            //    foreach ( var data in DataList6 )
            //    {
            //        chart.Title="Chuyền 6 "+"Ngày "+(DateTime.Now.Day)+" tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year;
            //        chart.ExecutionTime.Add(data.ExecutionTime);
            //        chart.TimeStandard.Add(5);
            //        chart.Labels.Add(data.TimeStamp.ToString( ));
            //    }
            //    p.Children.Add(chart);

            //}
        }
        private void GetCycleMessage (ValueMessage Message)
        {
            string MachineId;
            DateTime timeStamp;
            MachineId=Message.MachineId;
            double executionTime;
            switch ( MachineId )
            {
                case "DG1":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    DataList1.Add(new DataChart("DG1",timeStamp,executionTime));
                    break;
                case "DG2":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    DataList2.Add(new DataChart("DG2",timeStamp,executionTime));
                    break;
                case "DG3":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    DataList3.Add(new DataChart("DG3",timeStamp,executionTime));
                    break;
                case "DG4":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    DataList4.Add(new DataChart("DG4",timeStamp,executionTime));
                    break;
                case "DG5":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    DataList5.Add(new DataChart("DG5",timeStamp,executionTime));
                    break;
                case "DG6":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    DataList6.Add(new DataChart("DG6",timeStamp,executionTime));
                    break;
            }
        }

    }
}
