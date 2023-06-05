using MassTransit;
using Microsoft.Win32;
using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.Api;
using PackingMachine.core.Doman.Model;
using PackingMachine.core.Service;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.core.ViewModel.Components.MessageBox;
using PackingMachine.core.ViewModel.ViewModelBase;
using PackingSystemServiceContainers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static PackingMachine.core.ViewModel.MainSuperViewModel.SupervisorViewModel;

namespace PackingMachine.core.ViewModel.SettingViewModel
{
    public class MainSettingViewModel: BaseViewModel
    {
        #region read excel
        public ICommand ReadExcelCommand { get; set; }
        public ICommand EditCommand { set; get; }
        public ICommand ChangeMassCommand { set; get; }
        public ICommand StartCommand { set; get; }
        public ICommand ExportCommand { set; get; }

        private ObservableCollection<BaseInforOrders> _BaseInforOrder;
        public ObservableCollection<BaseInforOrders> BaseInforOrder { get => _BaseInforOrder; set { _BaseInforOrder=value; OnPropertyChanged(nameof(BaseInforOrder)); } }

        #endregion
        #region Load data to each BaseStore
        private string _PackingUnit;
        public string PackingUnit
        {
            get { return _PackingUnit; }
            set { _PackingUnit=value; OnPropertyChanged(nameof(PackingUnit)); }

        }

        private Employee _Employee;
        public Employee Employee
        {
            get { return _Employee; }
            set { _Employee=value; OnPropertyChanged(nameof(Employee)); }

        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName=value; OnPropertyChanged(nameof(LastName)); }

        }
        private string _ProductID;
        public string ProductID
        {
            get { return _ProductID; }
            set
            {
                _ProductID=value;
                OnPropertyChanged(nameof(ProductID));
                if ( _ProductID!=null )
                    GetItemById( );
            }
        }
        private string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName=value; OnPropertyChanged(nameof(ProductName)); }

        }
        private double _ProductMass = 0;
        public double ProductMass
        {
            get { return _ProductMass; }
            set { _ProductMass=value; OnPropertyChanged(nameof(ProductMass)); }

        }
        private int _PlannedQuantity = 0;
        public int PlannedQuantity
        {
            get { return _PlannedQuantity; }
            set { _PlannedQuantity=value; OnPropertyChanged(nameof(PlannedQuantity)); }

        }
        private string _Note;
        public string Note
        {
            get { return _Note; }
            set { _Note=value; OnPropertyChanged(nameof(Note)); }

        }
        private double _PercentValue;
        public double PercentValue
        {
            get { return _PercentValue; }
            set { _PercentValue=value; OnPropertyChanged(nameof(PercentValue)); }
        }
        private double _TotalQuality;
        public double TotalQuality
        {
            get { return _TotalQuality; }
            set { _TotalQuality=value; OnPropertyChanged(nameof(TotalQuality)); }
        }
        private string _ProductTestResult;
        public string ProductTestResult
        {
            get { return _ProductTestResult; }
            set { _ProductTestResult=value; OnPropertyChanged(nameof(ProductTestResult)); }
        }
        private double _WorkingTime;
        public double WorkingTime
        {
            get { return _WorkingTime; }
            set { _WorkingTime=value; OnPropertyChanged(nameof(WorkingTime)); }
        }
        private double _ExecutionTime;
        public double ExecutionTime
        {
            get { return _ExecutionTime; }
            set { _ExecutionTime=value; OnPropertyChanged(nameof(ExecutionTime)); }
        }

        private DateTime _TimeStamp;
        public DateTime TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp=value; OnPropertyChanged(nameof(TimeStamp)); }
        }
        // get all product Id
        private ObservableCollection<AllItems> _allProductId;
        public ObservableCollection<AllItems> AllProductId { get => _allProductId; set { _allProductId=value; OnPropertyChanged( ); } }
        private bool saveFlag;
        private IApiServices _apiServices;
        public Item ItemById { get; set; }
        private List<string> _boms;
        public List<string> Boms { get => _boms; set { _boms=value; OnPropertyChanged(nameof(Boms)); } }
        // get employee
        private ObservableCollection<Employee> _allEmployee;
        public ObservableCollection<Employee> AllEmployee { get => _allEmployee; set { _allEmployee=value; OnPropertyChanged( ); } }
        // get all packing unit
        private ObservableCollection<PackingUnit> _allPackagingUnit;
        public ObservableCollection<PackingUnit> AllPackagingUnit { get => _allPackagingUnit; set { _allPackagingUnit=value; OnPropertyChanged( ); } }
        // select row
        private BaseInforOrders _SelectRow;
        public BaseInforOrders SelectRow
        {
            get { return _SelectRow; }
            set
            {
                _SelectRow=value;
                OnPropertyChanged(nameof(SelectRow));
                if ( SelectRow!=null )
                {
                    PackingUnit=SelectRow._PackingUnit;
                    LastName=SelectRow._Employee;
                    ProductID=SelectRow._ProductID;
                    ProductName=SelectRow._ProductName;
                    ProductMass=SelectRow._ProductMass;
                    PlannedQuantity=SelectRow._PlannedQuantity;
                    Note=SelectRow._Note;
                }
            }
        }
        #endregion
        #region change mass
        private bool isChangeMassViewDisplayed;
        public bool IsChangeMassViewDisplayed
        {
            get { return isChangeMassViewDisplayed; }
            set { isChangeMassViewDisplayed=value; OnPropertyChanged(nameof(isChangeMassViewDisplayed)); }

        }
        #endregion
        #region Delare changeMass
        private string changeMassProductID;
        public string ChangeMassProductID
        {
            get { return changeMassProductID; }
            set { changeMassProductID=value; OnPropertyChanged(nameof(changeMassProductID)); }
        }

        private double massSampling1;
        public double MassSampling1
        {
            get { return massSampling1; }
            set { massSampling1=value; OnPropertyChanged(nameof(MassSampling1)); }
        }
        private double massSampling2;
        public double MassSampling2
        {
            get { return massSampling2; }
            set { massSampling2=value; OnPropertyChanged(nameof(MassSampling2)); }
        }
        private double massSampling3;
        public double MassSampling3
        {
            get { return massSampling3; }
            set { massSampling3=value; OnPropertyChanged(nameof(MassSampling3)); }
        }
        private double massSampling4;
        public double MassSampling4
        {
            get { return massSampling4; }
            set { massSampling4=value; OnPropertyChanged(nameof(MassSampling4)); }
        }
        private double massSampling5;
        public double MassSampling5
        {
            get { return massSampling5; }
            set { massSampling5=value; OnPropertyChanged(nameof(MassSampling5)); }
        }

        private double averageMassChange;
        public ICommand ChangeMassDeclineCommand { set; get; }
        public ICommand ChangeMassConfirmCommand { set; get; }
        #endregion
        public MessageBoxViewModel MessageBox { get; set; }
        private bool _isDialogOpen = false;
        public bool IsDialogOpen { get => _isDialogOpen; set { _isDialogOpen=value; OnPropertyChanged( ); } }
        private int MessageBoxCount;

        #region
        public ICommand DeleteCommand { set; get; }
        #endregion
        // machine 1
        private PageStoreMachine1 _InforOderStoreMachine1;
        private PageStoreMachine2 _InforOderStoreMachine2;
        private PageStoreMachine3 _InforOderStoreMachine3;
        private PageStoreMachine4 _InforOderStoreMachine4;
        private PageStoreMachine5 _InforOderStoreMachine5;
        private PageStoreMachine6 _InforOderStoreMachine6;

        private ObservableCollection<InforOders> _InforOrderMachine1;
        public ObservableCollection<InforOders> InforOrderMachine1 { get => _InforOrderMachine1; set { _InforOrderMachine1=value; OnPropertyChanged(nameof(InforOrderMachine1)); } }
        private ObservableCollection<InforOders> _InforOrderMachine2;
        public ObservableCollection<InforOders> InforOrderMachine2 { get => _InforOrderMachine2; set { _InforOrderMachine2=value; OnPropertyChanged(nameof(InforOrderMachine2)); } }
        private ObservableCollection<InforOders> _InforOrderMachine3;
        public ObservableCollection<InforOders> InforOrderMachine3 { get => _InforOrderMachine3; set { _InforOrderMachine3=value; OnPropertyChanged(nameof(InforOrderMachine3)); } }

        private ObservableCollection<InforOders> _InforOrderMachine4;
        public ObservableCollection<InforOders> InforOrderMachine4 { get => _InforOrderMachine4; set { _InforOrderMachine4=value; OnPropertyChanged(nameof(InforOrderMachine4)); } }

        private ObservableCollection<InforOders> _InforOrderMachine5;
        public ObservableCollection<InforOders> InforOrderMachine5 { get => _InforOrderMachine5; set { _InforOrderMachine5=value; OnPropertyChanged(nameof(InforOrderMachine5)); } }

        private ObservableCollection<InforOders> _InforOrderMachine6;
        public ObservableCollection<InforOders> InforOrderMachine6 { get => _InforOrderMachine6; set { _InforOrderMachine6=value; OnPropertyChanged(nameof(InforOrderMachine6)); } }

        private bool _isConnectedMachine1 = false;
        public bool IsConnectedMachine1 { get => _isConnectedMachine1; set { _isConnectedMachine1=value; OnPropertyChanged( ); } }
        private bool _isConnectedMachine2 = false;
        public bool IsConnectedMachine2 { get => _isConnectedMachine2; set { _isConnectedMachine2=value; OnPropertyChanged( ); } }
        private bool _isConnectedMachine3 = false;
        public bool IsConnectedMachine3 { get => _isConnectedMachine3; set { _isConnectedMachine3=value; OnPropertyChanged( ); } }
        private bool _isConnectedMachine4 = false;
        public bool IsConnectedMachine4 { get => _isConnectedMachine4; set { _isConnectedMachine4=value; OnPropertyChanged( ); } }
        private bool _isConnectedMachine5 = false;
        public bool IsConnectedMachine5 { get => _isConnectedMachine5; set { _isConnectedMachine5=value; OnPropertyChanged( ); } }
        private bool _isConnectedMachine6 = false;
        public bool IsConnectedMachine6 { get => _isConnectedMachine6; set { _isConnectedMachine6=value; OnPropertyChanged( ); } }
        private IBusControl _busControl;
        private List<ItemRasConfigurationMessage> _items;
        public List<ItemRasConfigurationMessage> Items { get => _items; set { _items=value; OnPropertyChanged(nameof(Items)); } }
        public ObservableCollection<RasConfigurationMessage> rasCofigMess;
        public ObservableCollection<RasConfigurationMessage> RasCofigMess { get => rasCofigMess; set { rasCofigMess=value; OnPropertyChanged( ); } }


        public static ReceiveMachineMessage MachineMessage;

        public MainSettingViewModel (IBusControl busControl,IApiServices apiServices,PageStoreMachine1 InforOderStoreMachine1,PageStoreMachine2 InforOderStoreMachine2,
            PageStoreMachine3 InforOderStoreMachine3,PageStoreMachine4 InforOderStoreMachine4,PageStoreMachine5 InforOderStoreMachine5,PageStoreMachine6 InforOderStoreMachine6)
        {
            _busControl=busControl;
            ReadExcelCommand=new RelayCommand(async ( ) => await DataGridSetting( ));
            // api 
            AllProductId=new ObservableCollection<AllItems>( );
            _apiServices=apiServices;

            EditCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Edit( )));
            ChangeMassCommand=new RelayCommand(async ( ) => await Task.Run(( ) => ChangeMass( )));
            ChangeMassDeclineCommand=new RelayCommand(async ( ) => await Task.Run(( ) => ChangeMassDecline( )));
            ChangeMassConfirmCommand=new RelayCommand(async ( ) => await Task.Run(( ) => ChangeMassConfirm( )));
            DeleteCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Delete( )));
            StartCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Start( )));
            ExportCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Export( )));
            IsChangeMassViewDisplayed=false;
            MessageBox=new MessageBoxViewModel( );
            MessageBox.Confirm+=DialogConfirm;
            MessageBox.Cancel+=DialogClose;
            GetAllItems( );
            GetEmployee( );
            GetAllPackingUnit( );
            //GetItemById();
            // machine 1
            _InforOderStoreMachine1=InforOderStoreMachine1;
            _InforOderStoreMachine2=InforOderStoreMachine2;
            _InforOderStoreMachine3=InforOderStoreMachine3;
            _InforOderStoreMachine4=InforOderStoreMachine4;
            _InforOderStoreMachine5=InforOderStoreMachine5;
            _InforOderStoreMachine6=InforOderStoreMachine6;
            InforOrderMachine1=new ObservableCollection<InforOders>( );
            InforOrderMachine2=new ObservableCollection<InforOders>( );
            InforOrderMachine3=new ObservableCollection<InforOders>( );
            InforOrderMachine4=new ObservableCollection<InforOders>( );
            InforOrderMachine5=new ObservableCollection<InforOders>( );
            InforOrderMachine6=new ObservableCollection<InforOders>( );
            MachineMessage=new ReceiveMachineMessage(GetMachineMessage);
        }
        private void GetMachineMessage (MachineMessage Message)
        {
            switch ( Message.MachineId )
            {
                case "DG1":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            //StatusMachine1Text = "Đã kết nối với máy 1";
                            IsConnectedMachine1=true;
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            IsConnectedMachine1=false;
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            IsConnectedMachine1=false;
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            IsConnectedMachine1=true;
                            break;
                    }
                    break;
                case "DG2":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            //StatusMachine1Text = "Đã kết nối với máy 1";
                            IsConnectedMachine2=true;
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            IsConnectedMachine2=false;
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            IsConnectedMachine2=false;
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            IsConnectedMachine2=true;
                            break;

                    }
                    break;
                case "DG3":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            //StatusMachine1Text = "Đã kết nối với máy 1";
                            IsConnectedMachine3=true;
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            IsConnectedMachine3=false;
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            IsConnectedMachine3=false;
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            IsConnectedMachine3=true;
                            break;
                    }
                    break;
                case "DG4":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            //StatusMachine1Text = "Đã kết nối với máy 1";
                            IsConnectedMachine4=true;
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            IsConnectedMachine4=false;
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            IsConnectedMachine4=false;
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            IsConnectedMachine4=true;
                            break;
                    }
                    break;
                case "DG5":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            //StatusMachine1Text = "Đã kết nối với máy 1";
                            IsConnectedMachine5=true;
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            IsConnectedMachine5=false;
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            IsConnectedMachine5=false;
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            IsConnectedMachine5=true;
                            break;
                    }
                    break;
                case "DG6":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            //StatusMachine1Text = "Đã kết nối với máy 1";
                            IsConnectedMachine6=true;
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            IsConnectedMachine6=false;
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            IsConnectedMachine6=false;
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            IsConnectedMachine6=true;
                            break;
                    }
                    break;
            }
        }
        private void Start ( )
        {
            MessageBox.ContentText="Bạn muốn bắt đầu đóng gói?";
            IsDialogOpen=true;
            MessageBoxCount=4;
        }
        private void Delete ( )
        {
            if ( SelectRow!=null )
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    int i = BaseInforOrder.IndexOf(SelectRow);
                    BaseInforOrder.RemoveAt(i);
                    BaseInforOrder.Remove(SelectRow);
                });
            }
        }
        private void DialogClose ( )
        {
            IsDialogOpen=false;
        }
        private void DialogConfirm ( )
        {
            switch ( MessageBoxCount )
            {
                case 1:
                    break;
                case 2:
                    DialogConfirmChangeMass( );
                    IsDialogOpen=false;
                    MessageBoxCount=0;
                    break;
                case 3:
                    IsDialogOpen=false;
                    break;
                case 4:
                    DialogConfirmStart( );
                    IsDialogOpen=false;
                    MessageBoxCount=0;
                    break;
            }
        }
        private void Export ( )
        {
            int j, i;
            j=BaseInforOrder.Count( );
            InforOrderMachine1.Clear( );
            InforOrderMachine2.Clear( );
            InforOrderMachine3.Clear( );
            InforOrderMachine4.Clear( );
            InforOrderMachine5.Clear( );
            InforOrderMachine6.Clear( );


            if ( j>0 )
            {
                for ( i=0;i<j;i++ )
                {

                    _PackingUnit=BaseInforOrder [i]._PackingUnit;
                    LastName=BaseInforOrder [i]._Employee;
                    ProductID=BaseInforOrder [i]._ProductID;
                    _ProductName=BaseInforOrder [i]._ProductName;
                    _ProductMass=BaseInforOrder [i]._ProductMass;
                    _PlannedQuantity=BaseInforOrder [i]._PlannedQuantity;
                    GetItemById( );
                    List<string> _boms = Boms;
                    int _ActualQuantity = 0;
                    int _ErrorProduct = 0;

                    _Note=BaseInforOrder [i]._Note;
                    switch ( _PackingUnit )
                    {
                        case "Máy 1":
                            InforOrderMachine1.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,_TotalQuality,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            break;
                        case "Máy 2":
                            InforOrderMachine2.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,_TotalQuality,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            break;
                        case "Máy 3":
                            InforOrderMachine3.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,_TotalQuality,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            break;
                        case "Máy 4":
                            InforOrderMachine4.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,_TotalQuality,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            break;
                        case "Máy 5":
                            InforOrderMachine5.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,_TotalQuality,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            break;
                        case "Máy 6":
                            InforOrderMachine6.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,_TotalQuality,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            break;
                    }
                }
            }
            ProductID="";
            ProductName="";
            ProductMass=0;
            PlannedQuantity=0;
            Note="";
            // export 6 sheet excel for each employee
            Export6SheetExcel( );
        }
        private void DialogConfirmStart ( )
        {
            int j, i;
            j=BaseInforOrder.Count( );
            InforOrderMachine1.Clear( );
            InforOrderMachine2.Clear( );
            InforOrderMachine3.Clear( );
            InforOrderMachine4.Clear( );
            InforOrderMachine5.Clear( );
            InforOrderMachine6.Clear( );

            int TotalQuality1 = 0;
            int TotalQuality2 = 0;
            int TotalQuality3 = 0;
            int TotalQuality4 = 0;
            int TotalQuality5 = 0;
            int TotalQuality6 = 0;
            {
                for ( i=0;i<j;i++ )
                {
                    _PackingUnit=BaseInforOrder [i]._PackingUnit;
                    LastName=BaseInforOrder [i]._Employee;
                    ProductID=BaseInforOrder [i]._ProductID;
                    _ProductName=BaseInforOrder [i]._ProductName;
                    _ProductMass=BaseInforOrder [i]._ProductMass;
                    _PlannedQuantity=BaseInforOrder [i]._PlannedQuantity;
                    GetItemById( );
                    List<string> _boms = Boms;
                    int _ActualQuantity = 0;
                    int _ErrorProduct = 0;
                    _Note=BaseInforOrder [i]._Note;
                    switch ( _PackingUnit )
                    {
                        case "Máy 1":
                            TotalQuality1+=PlannedQuantity;
                            InforOrderMachine1.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,TotalQuality1,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            break;
                        case "Máy 2":
                            InforOrderMachine2.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,TotalQuality2,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            TotalQuality2+=PlannedQuantity;
                            break;
                        case "Máy 3":
                            InforOrderMachine3.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,TotalQuality3,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            TotalQuality3+=PlannedQuantity;
                            break;
                        case "Máy 4":
                            InforOrderMachine4.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,TotalQuality4,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            TotalQuality4+=PlannedQuantity;
                            break;
                        case "Máy 5":
                            InforOrderMachine5.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,TotalQuality5,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            TotalQuality5+=PlannedQuantity;
                            break;
                        case "Máy 6":
                            InforOrderMachine6.Add(new InforOders(_PackingUnit,LastName,_ProductID,_ProductName,_boms,_ProductMass,_PlannedQuantity,_ActualQuantity,_ErrorProduct,_Note,_PercentValue,TotalQuality6,_ProductTestResult,_WorkingTime,_TimeStamp,_ExecutionTime));
                            TotalQuality6+=PlannedQuantity;
                            break;
                    }
                }
            }
            if ( InforOrderMachine1!=null )
            {
                _InforOderStoreMachine1.InforOrder=InforOrderMachine1;
                SendDataToMachine1( );
            }
            if ( InforOrderMachine2!=null )
            {
                _InforOderStoreMachine2.InforOrder=InforOrderMachine2;
                SendDataToMachine2( );
            }
            if ( InforOrderMachine3!=null )
            {
                _InforOderStoreMachine3.InforOrder=InforOrderMachine3;
                SendDataToMachine3( );
            }
            if ( InforOrderMachine4!=null )
            {
                _InforOderStoreMachine4.InforOrder=InforOrderMachine4;
                SendDataToMachine4( );
            }
            if ( InforOrderMachine5!=null )
            {
                _InforOderStoreMachine5.InforOrder=InforOrderMachine5;
                SendDataToMachine5( );
            }
            if ( InforOrderMachine6!=null )
            {
                _InforOderStoreMachine6.InforOrder=InforOrderMachine6;
                SendDataToMachine6( );
            }

            BaseInforOrder.Clear( );
            ProductID="";
            ProductName="";
            ProductMass=0;
            PlannedQuantity=0;
            Note="";

        }
        private async Task Export6SheetExcel ( )
        {
            await RunCommandAsync(saveFlag,async ( ) =>
            {
                //await Task.Delay(1);
                ExportExcelService exportExcel = new ExportExcelService( );
                var serviceResponse = await exportExcel.Export6Sheet(InforOrderMachine1,InforOrderMachine2,
                    InforOrderMachine3,InforOrderMachine4,InforOrderMachine5,InforOrderMachine6);
                if ( serviceResponse.Success )
                {
                    MessageBox.ContentText="Xuất Excel thành công!";
                    MessageBoxCount=3;
                    IsDialogOpen=true;
                }
            });
        }


        private async void SendDataToMachine1 ( )
        {
            var endpoint = await _busControl.GetSendEndpoint(new Uri("http://127.0.0.1:8181/send-config"));
            int j = 0;
            j=_InforOderStoreMachine1.InforOrder.Count( );
            Items=new List<ItemRasConfigurationMessage>( );
            for ( int i = 0;i<j;i++ )
            {
                int SetpointTotal = _InforOderStoreMachine1.InforOrder [i]._PlannedQuantity;
                string ProductId = _InforOderStoreMachine1.InforOrder [i]._ProductID;
                string ProductName = _InforOderStoreMachine1.InforOrder [i]._ProductName;
                List<string> boms = new List<string>( );
                boms.Add("No");
                double ProductMass = _InforOderStoreMachine1.InforOrder [i]._ProductMass;
                string Standard = "50 gói / 1 thùng";
                int CompletedProduct = _InforOderStoreMachine1.InforOrder [i]._ActualQuantity;
                int ErrorProduct = _InforOderStoreMachine1.InforOrder [i]._ErrorProduct;
                Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
            }
            RasConfigurationMessage mess = new RasConfigurationMessage { MachineId="DG1",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
            await endpoint.Send<RasConfigurationMessage>(mess);
        }
        private async void SendDataToMachine2 ( )
        {
            var endpoint = await _busControl.GetSendEndpoint(new Uri("http://127.0.0.1:8181/send-config"));
            int j = 0;
            j=_InforOderStoreMachine2.InforOrder.Count( );
            Items=new List<ItemRasConfigurationMessage>( );
            for ( int i = 0;i<j;i++ )
            {
                int SetpointTotal = _InforOderStoreMachine2.InforOrder [i]._PlannedQuantity;
                string ProductId = _InforOderStoreMachine2.InforOrder [i]._ProductID;
                string ProductName = _InforOderStoreMachine2.InforOrder [i]._ProductName;
                List<string> boms = new List<string>( );
                boms.Add("No");
                double ProductMass = _InforOderStoreMachine2.InforOrder [i]._ProductMass;
                string Standard = "50 gói / 1 thùng";
                int CompletedProduct = _InforOderStoreMachine2.InforOrder [i]._ActualQuantity;
                int ErrorProduct = _InforOderStoreMachine2.InforOrder [i]._ErrorProduct;
                Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
            }
            RasConfigurationMessage mess = new RasConfigurationMessage { MachineId="DG2",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
            await endpoint.Send<RasConfigurationMessage>(mess);
        }
        private async void SendDataToMachine3 ( )
        {
            var endpoint = await _busControl.GetSendEndpoint(new Uri("http://127.0.0.1:8181/send-config"));
            int j = 0;
            j=_InforOderStoreMachine3.InforOrder.Count( );
            Items=new List<ItemRasConfigurationMessage>( );
            for ( int i = 0;i<j;i++ )
            {
                int SetpointTotal = _InforOderStoreMachine3.InforOrder [i]._PlannedQuantity;
                string ProductId = _InforOderStoreMachine3.InforOrder [i]._ProductID;
                string ProductName = _InforOderStoreMachine3.InforOrder [i]._ProductName;
                List<string> boms = new List<string>( );
                boms.Add("No");
                double ProductMass = _InforOderStoreMachine3.InforOrder [i]._ProductMass;
                string Standard = "50 gói / 1 thùng";
                int CompletedProduct = _InforOderStoreMachine3.InforOrder [i]._ActualQuantity;
                int ErrorProduct = _InforOderStoreMachine3.InforOrder [i]._ErrorProduct;
                Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
            }
            RasConfigurationMessage mess = new RasConfigurationMessage { MachineId="DG3",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
            await endpoint.Send<RasConfigurationMessage>(mess);
        }
        private async void SendDataToMachine4 ( )
        {
            var endpoint = await _busControl.GetSendEndpoint(new Uri("http://127.0.0.1:8181/send-config"));
            int j = 0;
            j=_InforOderStoreMachine4.InforOrder.Count( );
            Items=new List<ItemRasConfigurationMessage>( );
            for ( int i = 0;i<j;i++ )
            {
                int SetpointTotal = _InforOderStoreMachine4.InforOrder [i]._PlannedQuantity;
                string ProductId = _InforOderStoreMachine4.InforOrder [i]._ProductID;
                string ProductName = _InforOderStoreMachine4.InforOrder [i]._ProductName;
                List<string> boms = new List<string>( );
                boms.Add("No");
                double ProductMass = _InforOderStoreMachine4.InforOrder [i]._ProductMass;
                string Standard = "50 gói / 1 thùng";
                int CompletedProduct = _InforOderStoreMachine4.InforOrder [i]._ActualQuantity;
                int ErrorProduct = _InforOderStoreMachine4.InforOrder [i]._ErrorProduct;
                Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
            }
            RasConfigurationMessage mess = new RasConfigurationMessage { MachineId="DG4",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
            await endpoint.Send<RasConfigurationMessage>(mess);
        }
        private async void SendDataToMachine5 ( )
        {
            var endpoint = await _busControl.GetSendEndpoint(new Uri("http://127.0.0.1:8181/send-config"));
            int j = 0;
            j=_InforOderStoreMachine5.InforOrder.Count( );
            Items=new List<ItemRasConfigurationMessage>( );
            for ( int i = 0;i<j;i++ )
            {
                int SetpointTotal = _InforOderStoreMachine5.InforOrder [i]._PlannedQuantity;
                string ProductId = _InforOderStoreMachine5.InforOrder [i]._ProductID;
                string ProductName = _InforOderStoreMachine5.InforOrder [i]._ProductName;
                List<string> boms = new List<string>( );
                boms.Add("No");
                double ProductMass = _InforOderStoreMachine5.InforOrder [i]._ProductMass;
                string Standard = "50 gói / 1 thùng";
                int CompletedProduct = _InforOderStoreMachine5.InforOrder [i]._ActualQuantity;
                int ErrorProduct = _InforOderStoreMachine5.InforOrder [i]._ErrorProduct;
                Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
            }
            RasConfigurationMessage mess = new RasConfigurationMessage { MachineId="DG5",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
            await endpoint.Send<RasConfigurationMessage>(mess);
        }
        private async void SendDataToMachine6 ( )
        {
            var endpoint = await _busControl.GetSendEndpoint(new Uri("http://127.0.0.1:8181/send-config"));
            int j = 0;
            j=_InforOderStoreMachine6.InforOrder.Count( );
            Items=new List<ItemRasConfigurationMessage>( );
            for ( int i = 0;i<j;i++ )
            {
                int SetpointTotal = _InforOderStoreMachine6.InforOrder [i]._PlannedQuantity;
                string ProductId = _InforOderStoreMachine6.InforOrder [i]._ProductID;
                string ProductName = _InforOderStoreMachine6.InforOrder [i]._ProductName;
                List<string> boms = new List<string>( );
                boms.Add("No");
                double ProductMass = _InforOderStoreMachine6.InforOrder [i]._ProductMass;
                string Standard = "50 gói / 1 thùng";
                int CompletedProduct = _InforOderStoreMachine6.InforOrder [i]._ActualQuantity;
                int ErrorProduct = _InforOderStoreMachine6.InforOrder [i]._ErrorProduct;
                Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
            }
            RasConfigurationMessage mess = new RasConfigurationMessage { MachineId="DG6",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
            await endpoint.Send<RasConfigurationMessage>(mess);
        }

        private void ChangeMass ( )
        {
            if ( ProductID!=""&&ProductName!="" )
            {
                IsChangeMassViewDisplayed=true;
                ChangeMassProductID=ProductID;
            }
            else
            {
                IsChangeMassViewDisplayed=false;
            }
        }
        private void ChangeMassDecline ( )
        {
            IsChangeMassViewDisplayed=false;
            changeMassProductID="";
            MassSampling1=0;
            MassSampling2=0;
            MassSampling3=0;
            MassSampling4=0;
            MassSampling5=0;
        }
        private void ChangeMassConfirm ( )
        {
            MessageBox.ContentText="Bạn có muốn thay đổi khối lượng gói hàng?";
            IsDialogOpen=true;
            IsChangeMassViewDisplayed=false;
            MessageBoxCount=2;
        }
        private void DialogConfirmChangeMass ( )
        {
            averageMassChange=(massSampling1+massSampling2+massSampling3+massSampling4+massSampling5)/5;
            string roundAverageMass;
            roundAverageMass=averageMassChange.ToString("0.0000");
            averageMassChange=Convert.ToDouble(roundAverageMass);
            ProductMass=averageMassChange;
            //IsChangeMassViewDisplayed = false;
            changeMassProductID="";
            MassSampling1=0;
            MassSampling2=0;
            MassSampling3=0;
            MassSampling4=0;
            MassSampling5=0;
        }
        private void Edit ( )
        {
            if ( SelectRow!=null )
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    SelectRow._PackingUnit=PackingUnit;
                    SelectRow._Employee=LastName;
                    SelectRow._ProductID=ProductID;
                    SelectRow._ProductName=ProductName;
                    SelectRow._ProductMass=ProductMass;
                    SelectRow._PlannedQuantity=PlannedQuantity;
                    SelectRow._Note=Note;
                    // binding to datagrid
                    int i = BaseInforOrder.IndexOf(SelectRow);
                    BaseInforOrder.Insert(i+1,SelectRow);
                    BaseInforOrder.RemoveAt(i);
                    // save to BaseInforOrdersBoms 

                    /* BaseInforOrdersBoms editInforBoms = new BaseInforOrdersBoms(PackingUnit, LastName, ProductID, ProductName, Boms, ProductMass, PlannedQuantity, Note);
                     BaseInforOrderBoms.Insert(i + 1, editInforBoms);
                     BaseInforOrderBoms.RemoveAt(i);*/
                    i=0;
                    ProductID="";
                    ProductName="";
                    ProductMass=0;
                    PlannedQuantity=0;
                    Note="";
                });
            }
        }
        public async void GetAllItems ( )
        {
            AllProductId=await _apiServices.GetAllItems("");
        }
        public async void GetAllPackingUnit ( )
        {
            AllPackagingUnit=await _apiServices.GetAllPackingUnits("");
        }
        public async void GetEmployee ( )
        {
            await Task.Delay(1);
            AllEmployee=await _apiServices.GetEmployee("");
        }
        public async void GetItemById ( )
        {
            // check text in combobox with productId on server. If two equal -> auto load name and standardWeight into textbox
            foreach ( var Product in AllProductId )
            {
                if ( ProductID==Product.Id.ToString( ) )
                {
                    await RunCommandAsync(saveFlag,async ( ) =>
                    {
                        await Task.Delay(1);
                        ItemById=await _apiServices.GetItemById("",ProductID.ToString( ));
                        ProductName=ItemById.name;
                        ProductMass=ItemById.standardWeight;
                        Boms=new List<string>( );
                        int a = 0;
                        a=ItemById.boms.Count( );
                        for ( int b = 0;b<a;b++ )
                        {
                            string ingredientName = ItemById.boms [b].name;
                            Boms.Add(ingredientName);
                        }
                    });
                }
            }
        }
        private async Task DataGridSetting ( )
        {
            BaseInforOrder=new ObservableCollection<BaseInforOrders>( );
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            OpenFileDialog d = new OpenFileDialog( );
            if ( d.ShowDialog( )==true )
            {
                filePath=d.FileName;
            }
            ExportExcelService _service = new ExportExcelService( );
            var result = await _service.Import(filePath,BaseInforOrder);
        }
    }
}
