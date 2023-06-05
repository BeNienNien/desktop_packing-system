using MassTransit;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.ViewModel.ViewModelBase;
using PackingSystemServiceContainers;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static PackingMachine.core.ViewModel.MainSuperViewModel.SupervisorViewModel;

namespace PackingMachine.core.ViewModel.WarningViewModel
{
    public class WarningViewModel: BaseViewModel

    {
        public delegate void ReceiveErrorMessage (PackingSystemServiceContainers.ErrorMessage message);
        public static ReceiveErrorMessage ErrorMessage;
        public static ReceiveValueMessage Sender;

        private ObservableCollection<Domain.Model.ErrorMessage> _errorList;
        public ObservableCollection<Domain.Model.ErrorMessage> ErrorList { get => _errorList; set { _errorList=value; OnPropertyChanged(nameof(ErrorList)); } }

        #region khaibaobien
        private DateTime dateTimeStart;
        public DateTime DateTimeStart { get => dateTimeStart; set { dateTimeStart=value; OnPropertyChanged( ); } }
        private DateTime dateTimeStop;
        public DateTime DateTimeStop { get => dateTimeStop; set { dateTimeStop=value; OnPropertyChanged( ); } }
        private string _timeStamp;
        public string TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp=value; OnPropertyChanged(nameof(TimeStamp)); }
        }
        private string _machineID;
        public string MachineID
        {
            get { return _machineID; }
            set { _machineID=value; OnPropertyChanged(nameof(MachineID)); }
        }

        private string _NameEvent;
        public string NameEvent
        {
            get { return _NameEvent; }
            set { _NameEvent=value; OnPropertyChanged(nameof(NameEvent)); }
        }
        private bool _ack;
        public bool ACK
        {
            get { return _ack; }
            set { _ack=value; OnPropertyChanged(nameof(ACK)); }
        }

        #endregion

        private IApiServices _apiServices;
        private IBusControl _busControl;
        public ICommand FilterCommand { get; set; }
        public WarningViewModel (IBusControl busControl,IApiServices apiServices)
        {
            ErrorMessage=new ReceiveErrorMessage(GetErrorMessage);
            ErrorList=new ObservableCollection<Domain.Model.ErrorMessage>( );
            _apiServices=apiServices;
            _busControl=busControl;
            DateTimeStart=DateTime.Now;
            DateTimeStop=DateTime.Now;
            Sender=new ReceiveValueMessage(GetCycleMessage);

        }
        private void GetErrorMessage (PackingSystemServiceContainers.ErrorMessage Message)
        {
            TimeStamp=DateTime.Now.ToString("yyyy-MM-dd");
            switch ( Message.MachineID )
            {
                case "DG1":
                    ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,Message.MachineID,Message.NameEvent,false));
                    break;
                case "DG2":
                    ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,Message.MachineID,Message.NameEvent,false));
                    break;
                case "DG3":
                    ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,Message.MachineID,Message.NameEvent,false));
                    break;
                case "DG4":
                    ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,Message.MachineID,Message.NameEvent,false));
                    break;
                case "DG5":
                    ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,Message.MachineID,Message.NameEvent,false));
                    break;
                case "DG6":
                    ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,Message.MachineID,Message.NameEvent,false));
                    break;
            }
        }
        public double preTime1 = 0;
        public double preTime2 = 0;
        public double preTime3 = 0;
        public double preTime4 = 0;
        public double preTime5 = 0;
        public double preTime6 = 0;
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
                    if ( executionTime>3 )
                    {
                        preTime1++;
                        if ( preTime1>10 )
                        {
                            ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,"Máy 1","Máy 1 đang làm chậm",false));
                        }
                    }
                    else preTime1=0;
                    break;
                case "DG2":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    if ( executionTime>3 )
                    {
                        preTime2++;
                        if ( preTime2>10 )
                        {
                            ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,"Máy 2","Máy 2 đang làm chậm",false));
                        }
                    }
                    else preTime2=0;
                    break;

                case "DG3":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    if ( executionTime>3 )
                    {
                        preTime3++;
                        if ( preTime3>10 )
                        {
                            ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,"Máy 3","Máy 3 đang làm chậm",false));
                        }
                    }
                    else preTime3=0;
                    break;
                case "DG4":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    if ( executionTime>3 )
                    {
                        preTime4++;
                        if ( preTime4>5 )
                        {
                            ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,"Máy 4","Máy 4 đang làm chậm",false));
                        }
                    }
                    else preTime4=0;
                    break;
                case "DG5":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    if ( executionTime>4 )
                    {
                        preTime5++;
                        if ( preTime5>5 )
                        {
                            ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,"Máy 5","Máy 5 đang làm chậm",false));
                        }
                    }
                    else preTime5=0;
                    break;
                case "DG6":
                    timeStamp=Message.Timestamp;
                    executionTime=Message.ExecutionTime;
                    if ( executionTime>4 )
                    {
                        preTime6++;
                        if ( preTime6>5 )
                        {
                            ErrorList.Add(new Domain.Model.ErrorMessage(TimeStamp,"Máy 6","Máy 6 đang làm chậm",false));
                        }
                    }
                    else preTime6=0;
                    break;
            }
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
