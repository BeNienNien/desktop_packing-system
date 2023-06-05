using MassTransit;
using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.Api;
using PackingMachine.core.Doman.Model;
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
using System.Windows.Threading;
using EventMachine = PackingSystemServiceContainers.ErrorMessage;

namespace PackingMachine.core.ViewModel.MainSuperViewModel
{
    public class SupervisorViewModel: BaseViewModel
    {
        #region using together

        // command
        public ICommand ChangeInforOrderMachine1Command { get; set; }
        public ICommand ConfirmCommand { set; get; }
        public ICommand DeleteCommand { set; get; }
        public ICommand EditCommand { set; get; }
        public ICommand ChangeMassCommand { set; get; }
        public ICommand StartCommand { set; get; }
        public ICommand DeclineChangeOrderDetailsCommand { set; get; }
        public ICommand MoveUpCommand { set; get; }
        public ICommand MoveDownCommand { set; get; }

        private int j, i;
        private int MachineChangeInforOrder;
        private ObservableCollection<ChangeInforOder> _ChangeInforMachine;
        public ObservableCollection<ChangeInforOder> ChangeInforMachine { get => _ChangeInforMachine; set { _ChangeInforMachine=value; OnPropertyChanged(nameof(ChangeInforMachine)); } }

        private ObservableCollection<EventMachine> listEvent;
        public ObservableCollection<EventMachine> ListEvent { get => listEvent; set { listEvent=value; OnPropertyChanged( ); } }
        // ingredient 
        public ICommand IngredientCommand { get; set; }
        public ICommand CloseIngredientCommand { get; set; }

        public static event Action ActionLoad;
        public static event Action ChangeEvent;

        private bool isIngredientViewDisplayed;
        public bool IsIngredientViewDisplayed
        {
            get { return isIngredientViewDisplayed; }
            set { isIngredientViewDisplayed=value; OnPropertyChanged(nameof(IsIngredientViewDisplayed)); }
        }
        private int _Cardinal;
        public int Cardinal
        {
            get { return _Cardinal; }
            set { _Cardinal=value; OnPropertyChanged(nameof(Cardinal)); }
        }
        private string _IngredientID;
        public string IngredientID
        {
            get { return _IngredientID; }
            set { _IngredientID=value; OnPropertyChanged(nameof(IngredientID)); }
        }
        private string _IngredientName;
        public string IngredientName
        {
            get { return _IngredientName; }
            set { _IngredientName=value; OnPropertyChanged(nameof(IngredientName)); }
        }

        private ObservableCollection<Ingredient> _Ingredients;
        public ObservableCollection<Ingredient> Ingredients { get => _Ingredients; set { _Ingredients=value; OnPropertyChanged(nameof(Ingredients)); } }
        private bool isChangeInforDisplay;
        public bool IsChangeInforDisplay { get => isChangeInforDisplay; set { isChangeInforDisplay=value; OnPropertyChanged( ); } }

        private string changeInforMachineHeader;
        public string ChangeInforMachineHeader
        {
            get { return changeInforMachineHeader; }
            set { changeInforMachineHeader=value; OnPropertyChanged(nameof(ChangeInforMachineHeader)); }

        }
        //Working day
        private string workingDay;
        public string WorkingDay
        {
            get { return workingDay; }
            set { workingDay=value; OnPropertyChanged(nameof(WorkingDay)); }
        }
        // change mass
        // isChangeMassViewDisplayed = true -> visible change Mass View is enable
        public ICommand ChangeMassDeclineCommand { set; get; }
        public ICommand ChangeMassConfirmCommand { set; get; }
        private bool isChangeMassViewDisplayed;
        public bool IsChangeMassViewDisplayed
        {
            get { return isChangeMassViewDisplayed; }
            set { isChangeMassViewDisplayed=value; OnPropertyChanged(nameof(isChangeMassViewDisplayed)); }

        }
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
        private ChangeInforOder _SelectRow;
        public ChangeInforOder SelectRow
        {
            get { return _SelectRow; }
            set
            {
                _SelectRow=value;
                OnPropertyChanged(nameof(SelectRow));
                if ( SelectRow!=null )
                {
                    Employee=SelectRow._employee;
                    ProductID=SelectRow._productID;
                    ProductName=SelectRow._productName;
                    ProductMass=SelectRow._productMass;
                    PlannedQuantity=SelectRow._plannedQuantity;
                    ActualQuantity=SelectRow._actualQuantity;
                    ErrorProduct=SelectRow._errorProduct;
                    Note=SelectRow._note;
                }
            }
        }
        private string _employee;
        public string Employee
        {
            get { return _employee; }
            set { _employee=value; OnPropertyChanged(nameof(Employee)); }
        }
        private string _productID;
        public string ProductID
        {
            get { return _productID; }
            set { _productID=value; OnPropertyChanged(nameof(ProductID)); }
        }
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set { _productName=value; OnPropertyChanged(nameof(ProductName)); }
        }
        private double _productMass;
        public double ProductMass
        {
            get { return _productMass; }
            set { _productMass=value; OnPropertyChanged(nameof(ProductMass)); }
        }
        private int _plannedQuantity;
        public int PlannedQuantity
        {
            get { return _plannedQuantity; }
            set { _plannedQuantity=value; OnPropertyChanged(nameof(PlannedQuantity)); }
        }
        private int _actualQuantity;
        public int ActualQuantity
        {
            get { return _actualQuantity; }
            set { _actualQuantity=value; OnPropertyChanged(nameof(ActualQuantity)); }
        }
        private int _errorProduct;
        public int ErrorProduct
        {
            get { return _errorProduct; }
            set { _errorProduct=value; OnPropertyChanged(nameof(ErrorProduct)); }
        }
        private string _note;
        public string Note
        {
            get { return _note; }
            set { _note=value; OnPropertyChanged(nameof(Note)); }
        }
        private double _percentValue;
        public double PercentValue
        {
            get { return _percentValue; }
            set { _percentValue=value; OnPropertyChanged(nameof(PercentValue)); }
        }
        private double _totalQuality;
        public double TotalQuality
        {
            get { return _totalQuality; }
            set { _totalQuality=value; OnPropertyChanged(nameof(TotalQuality)); }
        }
        private double _workingTime;
        public double WorkingTime
        {
            get { return _workingTime; }
            set { _workingTime=value; OnPropertyChanged(nameof(WorkingTime)); }
        }
        private double _executionTime;
        public double ExecutionTime
        {
            get { return _executionTime; }
            set { _executionTime=value; OnPropertyChanged(nameof(ExecutionTime)); }
        }
        private DateTime _timeStamp;
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp=value; OnPropertyChanged(nameof(TimeStamp)); }
        }
        private string _productTestResult;
        public string ProductTestResult
        {
            get { return _productTestResult; }
            set { _productTestResult=value; OnPropertyChanged(nameof(ProductTestResult)); }
        }

        // take data from server
        private ObservableCollection<AllItems> _allProductId;
        public ObservableCollection<AllItems> AllProductId { get => _allProductId; set { _allProductId=value; OnPropertyChanged( ); } }
        // api load infor order from server to local
        private IApiServices _apiServices;
        private bool saveFlag;
        public Item ItemById { get; set; }
        // custome message box 
        // notification
        private bool _isDialogOpen = false;
        public bool IsDialogOpen { get => _isDialogOpen; set { _isDialogOpen=value; OnPropertyChanged( ); } }
        // xác nhận đơn hàng
        public MessageBoxViewModel MessageBox { get; set; }
        private int MessageBoxCount;

        // Warning 
        private bool _isWarningDialogOpen = false;
        public bool IsWarningDialogOpen { get => _isWarningDialogOpen; set { _isWarningDialogOpen=value; OnPropertyChanged( ); } }
        public WarningBoxViewModel WarningBox { get; set; }
        // Read data from Raspberry
        public delegate void ReceiveValueMessage (ValueMessage Message);
        public static ReceiveValueMessage Sender;

        public delegate void ReceiveMachineMessage (MachineMessage Message);
        public static ReceiveMachineMessage MachineMessage;
        private List<string> _boms;
        public List<string> Boms { get => _boms; set { _boms=value; OnPropertyChanged(nameof(Boms)); } }
        #endregion
        #region machine 1
        private PageStoreMachine1 _InforOderStoreMachine1;
        private ObservableCollection<InforOders> _supervisorMachine1;
        public ObservableCollection<InforOders> SupervisorMachine1 { get => _supervisorMachine1; set { _supervisorMachine1=value; OnPropertyChanged(nameof(SupervisorMachine1)); } }
        private ObservableCollection<InforOders> _ChangedInforOrderMachine1;
        public ObservableCollection<InforOders> ChangedInforOrderMachine1 { get => _ChangedInforOrderMachine1; set { _ChangedInforOrderMachine1=value; OnPropertyChanged(nameof(ChangedInforOrderMachine1)); } }
        // change infor oders
        private ObservableCollection<ChangeInforOder> _ChangeInforMachine1;
        public ObservableCollection<ChangeInforOder> ChangeInforMachine1 { get => _ChangeInforMachine1; set { _ChangeInforMachine1=value; OnPropertyChanged( ); } }
        #endregion

        #region khaibaobien
        private string _employeeMachine1;
        public string EmployeeMachine1
        {
            get { return _employeeMachine1; }
            set { _employeeMachine1=value; OnPropertyChanged(nameof(EmployeeMachine1)); }
        }
        //progressBar1
        private double _currentProgress1;
        public double CurrentProgress1
        {
            get { return _currentProgress1; }
            set
            {
                _currentProgress1=value;
                OnPropertyChanged(nameof(CurrentProgress1));
            }
        }
        private double _percentValue1;
        public double PercentValue1
        {
            get { return _percentValue1; }
            set { _percentValue1=value; OnPropertyChanged(nameof(PercentValue1)); }
        }
        private double _totalQuality1;
        public double TotalQuality1
        {
            get { return _totalQuality1; }
            set { _totalQuality1=value; OnPropertyChanged(nameof(TotalQuality1)); }
        }
        // machine status
        private bool _isMachine1Connected;
        public bool IsMachine1Connected { get => _isMachine1Connected; set { _isMachine1Connected=value; OnPropertyChanged( ); } }
        private string _statusMachine1Text;
        public string StatusMachine1Text
        {
            get { return _statusMachine1Text; }
            set { _statusMachine1Text=value; OnPropertyChanged(nameof(StatusMachine1Text)); }
        }

        #endregion

        #region machine 2
        private PageStoreMachine2 _InforOderStoreMachine2;
        private ObservableCollection<InforOders> _supervisorMachine2;
        public ObservableCollection<InforOders> SupervisorMachine2 { get => _supervisorMachine2; set { _supervisorMachine2=value; OnPropertyChanged(nameof(SupervisorMachine2)); } }
        private ObservableCollection<InforOders> _ChangedInforOrderMachine2;
        public ObservableCollection<InforOders> ChangedInforOrderMachine2 { get => _ChangedInforOrderMachine2; set { _ChangedInforOrderMachine2=value; OnPropertyChanged(nameof(ChangedInforOrderMachine2)); } }
        // change infor oders
        private ObservableCollection<ChangeInforOder> _ChangeInforMachine2;
        public ObservableCollection<ChangeInforOder> ChangeInforMachine2 { get => _ChangeInforMachine2; set { _ChangeInforMachine2=value; OnPropertyChanged( ); } }
        public ICommand ChangeInforOrderMachine2Command { get; set; }
        private string _employeeMachine2;
        public string EmployeeMachine2
        {
            get { return _employeeMachine2; }
            set { _employeeMachine2=value; OnPropertyChanged(nameof(EmployeeMachine2)); }
        }
        //progressBar1
        private double _currentProgress2;
        public double CurrentProgress2
        {
            get { return _currentProgress2; }
            set
            {
                _currentProgress2=value;
                OnPropertyChanged(nameof(CurrentProgress2));
            }
        }
        private double _percentValue2;
        public double PercentValue2
        {
            get { return _percentValue2; }
            set { _percentValue2=value; OnPropertyChanged(nameof(PercentValue2)); }
        }
        private double _totalQuality2;
        public double TotalQuality2
        {
            get { return _totalQuality2; }
            set { _totalQuality2=value; OnPropertyChanged(nameof(TotalQuality2)); }
        }
        // machine status
        private bool _isMachine2Connected;
        public bool IsMachine2Connected { get => _isMachine2Connected; set { _isMachine2Connected=value; OnPropertyChanged( ); } }
        private string _statusMachine2Text;
        public string StatusMachine2Text
        {
            get { return _statusMachine2Text; }
            set { _statusMachine2Text=value; OnPropertyChanged(nameof(StatusMachine2Text)); }
        }
        #endregion
        #region machine 3
        private PageStoreMachine3 _InforOderStoreMachine3;
        private ObservableCollection<InforOders> _supervisorMachine3;
        public ObservableCollection<InforOders> SupervisorMachine3 { get => _supervisorMachine3; set { _supervisorMachine3=value; OnPropertyChanged(nameof(SupervisorMachine3)); } }
        private ObservableCollection<InforOders> _ChangedInforOrderMachine3;
        public ObservableCollection<InforOders> ChangedInforOrderMachine3 { get => _ChangedInforOrderMachine3; set { _ChangedInforOrderMachine3=value; OnPropertyChanged(nameof(ChangedInforOrderMachine3)); } }
        // change infor oders
        private ObservableCollection<ChangeInforOder> _ChangeInforMachine3;
        public ObservableCollection<ChangeInforOder> ChangeInforMachine3 { get => _ChangeInforMachine3; set { _ChangeInforMachine3=value; OnPropertyChanged( ); } }
        public ICommand ChangeInforOrderMachine3Command { get; set; }
        private string _employeeMachine3;
        public string EmployeeMachine3
        {
            get { return _employeeMachine3; }
            set { _employeeMachine3=value; OnPropertyChanged(nameof(EmployeeMachine3)); }
        }
        //progressBar1
        private double _currentProgress3;
        public double CurrentProgress3
        {
            get { return _currentProgress3; }
            set
            {
                _currentProgress3=value;
                OnPropertyChanged(nameof(CurrentProgress3));
            }
        }
        private double _percentValue3;
        public double PercentValue3
        {
            get { return _percentValue3; }
            set { _percentValue3=value; OnPropertyChanged(nameof(PercentValue3)); }
        }
        private double _totalQuality3;
        public double TotalQuality3
        {
            get { return _totalQuality3; }
            set { _totalQuality3=value; OnPropertyChanged(nameof(TotalQuality3)); }
        }
        // machine status
        private bool _isMachine3Connected;
        public bool IsMachine3Connected { get => _isMachine3Connected; set { _isMachine3Connected=value; OnPropertyChanged( ); } }
        private string _statusMachine3Text;
        public string StatusMachine3Text
        {
            get { return _statusMachine3Text; }
            set { _statusMachine3Text=value; OnPropertyChanged(nameof(StatusMachine3Text)); }
        }
        #endregion
        #region machine 4
        private PageStoreMachine4 _InforOderStoreMachine4;
        private ObservableCollection<InforOders> _supervisorMachine4;
        public ObservableCollection<InforOders> SupervisorMachine4 { get => _supervisorMachine4; set { _supervisorMachine4=value; OnPropertyChanged(nameof(SupervisorMachine4)); } }
        private ObservableCollection<InforOders> _ChangedInforOrderMachine4;
        public ObservableCollection<InforOders> ChangedInforOrderMachine4 { get => _ChangedInforOrderMachine4; set { _ChangedInforOrderMachine4=value; OnPropertyChanged(nameof(ChangedInforOrderMachine4)); } }
        // change infor oders
        private ObservableCollection<ChangeInforOder> _ChangeInforMachine4;
        public ObservableCollection<ChangeInforOder> ChangeInforMachine4 { get => _ChangeInforMachine4; set { _ChangeInforMachine4=value; OnPropertyChanged( ); } }
        public ICommand ChangeInforOrderMachine4Command { get; set; }
        private string _employeeMachine4;
        public string EmployeeMachine4
        {
            get { return _employeeMachine4; }
            set { _employeeMachine4=value; OnPropertyChanged(nameof(EmployeeMachine4)); }
        }
        //progressBar1
        private double _currentProgress4;
        public double CurrentProgress4
        {
            get { return _currentProgress4; }
            set
            {
                _currentProgress4=value;
                OnPropertyChanged(nameof(CurrentProgress4));
            }
        }
        private double _percentValue4;
        public double PercentValue4
        {
            get { return _percentValue4; }
            set { _percentValue4=value; OnPropertyChanged(nameof(PercentValue4)); }
        }
        private double _totalQuality4;
        public double TotalQuality4
        {
            get { return _totalQuality4; }
            set { _totalQuality4=value; OnPropertyChanged(nameof(TotalQuality4)); }
        }
        // machine status
        private bool _isMachine4Connected;
        public bool IsMachine4Connected { get => _isMachine4Connected; set { _isMachine4Connected=value; OnPropertyChanged( ); } }
        private string _statusMachine4Text;
        public string StatusMachine4Text
        {
            get { return _statusMachine4Text; }
            set { _statusMachine4Text=value; OnPropertyChanged(nameof(StatusMachine4Text)); }
        }
        #endregion
        #region machine 5
        private PageStoreMachine5 _InforOderStoreMachine5;
        private ObservableCollection<InforOders> _supervisorMachine5;
        public ObservableCollection<InforOders> SupervisorMachine5 { get => _supervisorMachine5; set { _supervisorMachine5=value; OnPropertyChanged(nameof(SupervisorMachine5)); } }
        private ObservableCollection<InforOders> _ChangedInforOrderMachine5;
        public ObservableCollection<InforOders> ChangedInforOrderMachine5 { get => _ChangedInforOrderMachine5; set { _ChangedInforOrderMachine5=value; OnPropertyChanged(nameof(ChangedInforOrderMachine5)); } }
        // change infor oders
        private ObservableCollection<ChangeInforOder> _ChangeInforMachine5;
        public ObservableCollection<ChangeInforOder> ChangeInforMachine5 { get => _ChangeInforMachine5; set { _ChangeInforMachine5=value; OnPropertyChanged( ); } }
        public ICommand ChangeInforOrderMachine5Command { get; set; }
        private string _employeeMachine5;
        public string EmployeeMachine5
        {
            get { return _employeeMachine5; }
            set { _employeeMachine5=value; OnPropertyChanged(nameof(EmployeeMachine5)); }
        }
        //progressBar1
        private double _currentProgress5;
        public double CurrentProgress5
        {
            get { return _currentProgress5; }
            set
            {
                _currentProgress5=value;
                OnPropertyChanged(nameof(CurrentProgress5));
            }
        }
        private double _percentValue5;
        public double PercentValue5
        {
            get { return _percentValue5; }
            set { _percentValue5=value; OnPropertyChanged(nameof(PercentValue5)); }
        }
        private double _totalQuality5;
        public double TotalQuality5
        {
            get { return _totalQuality5; }
            set { _totalQuality5=value; OnPropertyChanged(nameof(TotalQuality5)); }
        }
        // machine status
        private bool _isMachine5Connected;
        public bool IsMachine5Connected { get => _isMachine5Connected; set { _isMachine5Connected=value; OnPropertyChanged( ); } }
        private string _statusMachine5Text;
        public string StatusMachine5Text
        {
            get { return _statusMachine5Text; }
            set { _statusMachine5Text=value; OnPropertyChanged(nameof(StatusMachine5Text)); }
        }
        #endregion
        #region machine 6
        private PageStoreMachine6 _InforOderStoreMachine6;
        private ObservableCollection<InforOders> _supervisorMachine6;
        public ObservableCollection<InforOders> SupervisorMachine6 { get => _supervisorMachine6; set { _supervisorMachine6=value; OnPropertyChanged(nameof(SupervisorMachine6)); } }
        private ObservableCollection<InforOders> _ChangedInforOrderMachine6;
        public ObservableCollection<InforOders> ChangedInforOrderMachine6 { get => _ChangedInforOrderMachine6; set { _ChangedInforOrderMachine6=value; OnPropertyChanged(nameof(ChangedInforOrderMachine6)); } }
        // change infor oders
        private ObservableCollection<ChangeInforOder> _ChangeInforMachine6;
        public ObservableCollection<ChangeInforOder> ChangeInforMachine6 { get => _ChangeInforMachine6; set { _ChangeInforMachine6=value; OnPropertyChanged( ); } }
        public ICommand ChangeInforOrderMachine6Command { get; set; }
        private string _employeeMachine6;
        public string EmployeeMachine6
        {
            get { return _employeeMachine6; }
            set { _employeeMachine6=value; OnPropertyChanged(nameof(EmployeeMachine6)); }
        }
        //progressBar1
        private double _currentProgress6;
        public double CurrentProgress6
        {
            get { return _currentProgress6; }
            set
            {
                _currentProgress6=value;
                OnPropertyChanged(nameof(CurrentProgress6));
            }
        }
        private double _percentValue6;
        public double PercentValue6
        {
            get { return _percentValue6; }
            set { _percentValue6=value; OnPropertyChanged(nameof(PercentValue6)); }
        }
        private double _totalQuality6;
        public double TotalQuality6
        {
            get { return _totalQuality6; }
            set { _totalQuality6=value; OnPropertyChanged(nameof(TotalQuality6)); }
        }
        // machine status
        private bool _isMachine6Connected;
        public bool IsMachine6Connected { get => _isMachine6Connected; set { _isMachine6Connected=value; OnPropertyChanged( ); } }
        private string _statusMachine6Text;
        public string StatusMachine6Text
        {
            get { return _statusMachine6Text; }
            set { _statusMachine6Text=value; OnPropertyChanged(nameof(StatusMachine6Text)); }
        }
        #endregion
        private IBusControl _busControl;
        private List<ItemRasConfigurationMessage> _items;
        public List<ItemRasConfigurationMessage> Items { get => _items; set { _items=value; OnPropertyChanged(nameof(Items)); } }
        public SupervisorViewModel (IBusControl busControl,PageStoreMachine1 InforOrderStoreMachine1,PageStoreMachine2 InforOderStoreMachine2,PageStoreMachine3 InforOderStoreMachine3,PageStoreMachine4 InforOderStoreMachine4,PageStoreMachine5 InforOderStoreMachine5,PageStoreMachine6 InforOderStoreMachine6,IApiServices apiServices)
        {
            #region using together
            MachineChangeInforOrder=0;
            // load data
            try
            {
                SetTimer( );
            }
            catch
            {
                Console.WriteLine("cann't load data");
            }
            WorkingDay=DateTime.Now.ToString("dd/MM/yyyy");
            IsChangeInforDisplay=false;
            ChangeInforOrderMachine1Command=new RelayCommand(async ( ) => ChangedInforMachine1( ));
            ConfirmCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Confirm( )));
            DeleteCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Delete( )));
            EditCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Edit( )));
            StartCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Start( )));
            DeclineChangeOrderDetailsCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Decline( )));
            ChangeMassCommand=new RelayCommand(async ( ) => await Task.Run(( ) => ChangeMass( )));
            // Move up/down
            MoveUpCommand=new RelayCommand(async ( ) => await Task.Run(( ) => MoveUp( )));
            MoveDownCommand=new RelayCommand(async ( ) => await Task.Run(( ) => MoveDown( )));
            // change mass 
            ChangeMassDeclineCommand=new RelayCommand(async ( ) => await Task.Run(( ) => ChangeMassDecline( )));
            ChangeMassConfirmCommand=new RelayCommand(async ( ) => await Task.Run(( ) => ChangeMassConfirm( )));
            // Ingredient
            Ingredients=new ObservableCollection<Ingredient>( );
            IsIngredientViewDisplayed=false;
            IngredientCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Ingredient( )));
            CloseIngredientCommand=new RelayCommand(async ( ) => await Task.Run(( ) => CloseIngredient( )));
            // api 
            AllProductId=new ObservableCollection<AllItems>( );
            _apiServices=apiServices;
            ListEvent=new( );
            GetAllItems( );
            // message box 
            // Xác nhận đơn hàng
            MessageBox=new MessageBoxViewModel( );
            MessageBox.Confirm+=DialogConfirm;
            MessageBox.Cancel+=DialogClose;

            // WarningBox
            WarningBox=new WarningBoxViewModel( );
            WarningBox.WarningConfirm+=WarningConfirm;
            // Read data from Raspberry
            Sender=new ReceiveValueMessage(GetCycleMessage);
            MachineMessage=new ReceiveMachineMessage(GetMachineMessage);
            #endregion
            _busControl=busControl;
            #region supervisor machine 1
            _InforOderStoreMachine1=InforOrderStoreMachine1;
            ChangeInforOrderMachine1Command=new RelayCommand(async ( ) => ChangedInforMachine1( ));
            ChangedInforOrderMachine1=new ObservableCollection<InforOders>( );
            StatusMachine1Text="Không hoạt động";
            IsMachine1Connected=false;
            #endregion
            #region supervisor machine 2
            _InforOderStoreMachine2=InforOderStoreMachine2;
            ChangeInforOrderMachine2Command=new RelayCommand(async ( ) => ChangedInforMachine2( ));
            ChangedInforOrderMachine2=new ObservableCollection<InforOders>( );
            StatusMachine2Text="Không hoạt động";
            IsMachine2Connected=false;
            #endregion
            #region supervisor machine 3
            _InforOderStoreMachine3=InforOderStoreMachine3;
            ChangeInforOrderMachine3Command=new RelayCommand(async ( ) => ChangedInforMachine3( ));
            ChangedInforOrderMachine3=new ObservableCollection<InforOders>( );
            StatusMachine3Text="Không hoạt động";
            IsMachine3Connected=false;
            #endregion
            #region supervisor machine 4
            _InforOderStoreMachine4=InforOderStoreMachine4;
            ChangeInforOrderMachine4Command=new RelayCommand(async ( ) => ChangedInforMachine4( ));
            ChangedInforOrderMachine4=new ObservableCollection<InforOders>( );
            StatusMachine4Text="Không hoạt động";
            IsMachine4Connected=false;
            #endregion
            #region supervisor machine 5
            _InforOderStoreMachine5=InforOderStoreMachine5;
            ChangeInforOrderMachine5Command=new RelayCommand(async ( ) => ChangedInforMachine5( ));
            ChangedInforOrderMachine5=new ObservableCollection<InforOders>( );
            StatusMachine5Text="Không hoạt động";
            IsMachine5Connected=false;
            #endregion
            #region supervisor machine 6
            _InforOderStoreMachine6=InforOderStoreMachine6;
            ChangeInforOrderMachine6Command=new RelayCommand(async ( ) => ChangedInforMachine6( ));
            ChangedInforOrderMachine6=new ObservableCollection<InforOders>( );
            StatusMachine6Text="Không hoạt động";
            IsMachine6Connected=false;
            #endregion

        }
        #region storeEvent
        public void StoreEvent (EventMachine eventMachine,string nameMachine)
        {
            PackingSystemServiceContainers.ErrorMessage eventMachine1 = new PackingSystemServiceContainers.ErrorMessage( );
            eventMachine1.NameEvent=eventMachine.NameEvent;
            eventMachine1.TimeStamp=eventMachine.TimeStamp;
            eventMachine1.MachineID=eventMachine.MachineID;
            // _databaseServices.InsertEventAsync(eventMachine1);
            //nếu không có database em có thể gọi 1 sự kiện ở viewmodel này qua cái viewmodel khác đc không

        }
        #endregion
        // notification
        private void DialogConfirm ( )
        {
            switch ( MessageBoxCount )
            {
                case 1:
                    DialogChangeOdersConfirm( );
                    IsDialogOpen=false;
                    MessageBoxCount=0;
                    break;
                case 2:
                    DialogConfirmChangeMass( );
                    IsDialogOpen=false;
                    MessageBoxCount=0;
                    break;
                case 3:
                    DialogConfirmEdit( );
                    IsDialogOpen=false;
                    MessageBoxCount=0;
                    break;
                case 4:
                    DialogConfirmStart( );
                    IsDialogOpen=false;
                    MessageBoxCount=0;
                    break;
            }
        }
        private void DialogClose ( )
        {
            IsDialogOpen=false;
        }
        private void WarningConfirm ( )
        {
            IsWarningDialogOpen=false;
        }
        protected void dispatcherTimer_Tick (object sender,EventArgs e)
        {
            RebindData( );
        }
        private void RebindData ( )
        {
            //Load dữ liệu vào trang supervisor trong trường hợp nhấn giám sát trước khi nhập thông tin đơn hàng cho từng máy
            #region machine 1
            if ( EmployeeMachine1==null&&_InforOderStoreMachine1.InforOrder!=null&&_InforOderStoreMachine1.InforOrder.Count>0 )
            {
                SupervisorMachine1=_InforOderStoreMachine1.InforOrder;
                EmployeeMachine1=_InforOderStoreMachine1.InforOrder [0]._Employee;
                int i, j;
                j=_InforOderStoreMachine1.InforOrder.Count;
                if ( j>0 )
                    for ( i=0;i<j;i++ )
                    {
                        TotalQuality1+=_InforOderStoreMachine1.InforOrder [i]._PlannedQuantity;
                    }
                CurrentProgress1=PercentValue1;
            }
            #endregion
            #region machine 2
            if ( EmployeeMachine2==null&&_InforOderStoreMachine2.InforOrder!=null&&_InforOderStoreMachine2.InforOrder.Count>0 )
            {
                SupervisorMachine2=_InforOderStoreMachine2.InforOrder;
                EmployeeMachine2=_InforOderStoreMachine2.InforOrder [0]._Employee;
                int i, j;
                j=_InforOderStoreMachine2.InforOrder.Count;
                if ( j>0 )
                    for ( i=0;i<j;i++ )
                    {

                        TotalQuality2+=_InforOderStoreMachine2.InforOrder [i]._PlannedQuantity;
                    }
                CurrentProgress2=PercentValue2;
            }
            #endregion
            #region machine 3
            if ( EmployeeMachine3==null&&_InforOderStoreMachine3.InforOrder!=null&&_InforOderStoreMachine3.InforOrder.Count>0 )
            {
                SupervisorMachine3=_InforOderStoreMachine3.InforOrder;
                EmployeeMachine3=_InforOderStoreMachine3.InforOrder [0]._Employee;
                int i, j;
                j=_InforOderStoreMachine3.InforOrder.Count;
                if ( j>0 )
                    for ( i=0;i<j;i++ )
                    {

                        TotalQuality3+=_InforOderStoreMachine3.InforOrder [i]._PlannedQuantity;
                    }
                CurrentProgress3=PercentValue3;
            }
            #endregion
            #region machine 4
            if ( EmployeeMachine4==null&&_InforOderStoreMachine4.InforOrder!=null&&_InforOderStoreMachine4.InforOrder.Count>0 )
            {
                SupervisorMachine4=_InforOderStoreMachine4.InforOrder;
                EmployeeMachine4=_InforOderStoreMachine4.InforOrder [0]._Employee;
                int i, j;
                j=_InforOderStoreMachine4.InforOrder.Count;
                if ( j>0 )
                    for ( i=0;i<j;i++ )
                    {

                        TotalQuality4+=_InforOderStoreMachine4.InforOrder [i]._PlannedQuantity;
                    }
                CurrentProgress4=PercentValue4;
            }
            #endregion
            #region machine 5
            if ( EmployeeMachine5==null&&_InforOderStoreMachine5.InforOrder!=null&&_InforOderStoreMachine5.InforOrder.Count>0 )
            {
                SupervisorMachine5=_InforOderStoreMachine5.InforOrder;
                EmployeeMachine5=_InforOderStoreMachine5.InforOrder [0]._Employee;
                int i, j;
                j=_InforOderStoreMachine5.InforOrder.Count;
                if ( j>0 )
                    for ( i=0;i<j;i++ )
                    {

                        TotalQuality5+=_InforOderStoreMachine5.InforOrder [i]._PlannedQuantity;
                    }
                CurrentProgress5=PercentValue5;
            }
            #endregion
            #region machine 6
            if ( EmployeeMachine6==null&&_InforOderStoreMachine6.InforOrder!=null&&_InforOderStoreMachine6.InforOrder.Count>0 )
            {
                SupervisorMachine6=_InforOderStoreMachine6.InforOrder;
                EmployeeMachine6=_InforOderStoreMachine6.InforOrder [0]._Employee;
                int i, j;
                j=_InforOderStoreMachine6.InforOrder.Count;
                if ( j>0 )
                    for ( i=0;i<j;i++ )
                    {

                        TotalQuality6+=_InforOderStoreMachine6.InforOrder [i]._PlannedQuantity;
                    }
                CurrentProgress6=PercentValue6;
            }
            #endregion
        }
        //Set and start the timer
        private void SetTimer ( )
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer( );
            dispatcherTimer.Tick+=new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval=new TimeSpan(0,0,2);
            dispatcherTimer.Start( );
        }
        private void ChangedInforMachine1 ( )
        {
            MachineChangeInforOrder=1;
            IsChangeInforDisplay=true;
            ChangeInforMachineHeader="Thay đổi đơn hàng máy 1";
            ChangeInforMachine1=new ObservableCollection<ChangeInforOder>( );
            if ( SupervisorMachine1!=null )
            {
                j=_InforOderStoreMachine1.InforOrder.Count( );
                for ( i=0;i<j;i++ )
                {
                    ChangeInforMachine1.Add(new ChangeInforOder(_InforOderStoreMachine1.InforOrder [i]._Employee,_InforOderStoreMachine1.InforOrder [i]._ProductID,
                        _InforOderStoreMachine1.InforOrder [i]._ProductName,_InforOderStoreMachine1.InforOrder [i]._Boms,_InforOderStoreMachine1.InforOrder [i]._ProductMass,_InforOderStoreMachine1.InforOrder [i]._PlannedQuantity,SupervisorMachine1 [i]._ActualQuantity,
                        SupervisorMachine1 [i]._ErrorProduct,_InforOderStoreMachine1.InforOrder [i]._Note,_InforOderStoreMachine1.InforOrder [i]._PercentValue,_InforOderStoreMachine1.InforOrder [i]._TotalQuality
                        ,_InforOderStoreMachine1.InforOrder [i]._ProductTestResult,_InforOderStoreMachine1.InforOrder [i]._WorkingTime,_InforOderStoreMachine1.InforOrder [i]._TimeStamp,SupervisorMachine1 [i]._ExecutionTime));
                }
            }
            ChangeInforMachine=ChangeInforMachine1;
        }
        private void ChangedInforMachine2 ( )
        {
            MachineChangeInforOrder=2;
            IsChangeInforDisplay=true;
            ChangeInforMachineHeader="Thay đổi đơn hàng máy 2";
            ChangeInforMachine2=new ObservableCollection<ChangeInforOder>( );
            if ( SupervisorMachine2!=null )
            {
                j=SupervisorMachine2.Count( );
                for ( i=0;i<j;i++ )
                {
                    ChangeInforMachine2.Add(new ChangeInforOder(SupervisorMachine2 [i]._Employee,SupervisorMachine2 [i]._ProductID,
                        SupervisorMachine2 [i]._ProductName,SupervisorMachine2 [i]._Boms,SupervisorMachine2 [i]._ProductMass,
                        SupervisorMachine2 [i]._PlannedQuantity,SupervisorMachine2 [i]._ActualQuantity,SupervisorMachine2 [i]._ErrorProduct,
                        SupervisorMachine2 [i]._Note,SupervisorMachine2 [i]._PercentValue,SupervisorMachine2 [i]._TotalQuality,SupervisorMachine2 [i]._ProductTestResult,SupervisorMachine2 [i]._WorkingTime,SupervisorMachine2 [i]._TimeStamp,SupervisorMachine2 [i]._ExecutionTime));
                }
            }
            ChangeInforMachine=ChangeInforMachine2;
        }
        private void ChangedInforMachine3 ( )
        {
            MachineChangeInforOrder=3;
            IsChangeInforDisplay=true;
            ChangeInforMachineHeader="Thay đổi đơn hàng máy 3";
            ChangeInforMachine3=new ObservableCollection<ChangeInforOder>( );
            if ( SupervisorMachine3!=null )
            {
                j=SupervisorMachine3.Count( );
                for ( i=0;i<j;i++ )
                {
                    ChangeInforMachine3.Add(new ChangeInforOder(SupervisorMachine3 [i]._Employee,SupervisorMachine3 [i]._ProductID,
                        SupervisorMachine3 [i]._ProductName,SupervisorMachine3 [i]._Boms,SupervisorMachine3 [i]._ProductMass,
                        SupervisorMachine3 [i]._PlannedQuantity,SupervisorMachine3 [i]._ActualQuantity,SupervisorMachine3 [i]._ErrorProduct,
                        SupervisorMachine3 [i]._Note,SupervisorMachine3 [i]._PercentValue,SupervisorMachine3 [i]._TotalQuality,SupervisorMachine3 [i]._ProductTestResult,SupervisorMachine3 [i]._WorkingTime,SupervisorMachine3 [i]._TimeStamp,SupervisorMachine3 [i]._ExecutionTime));
                }
            }
            ChangeInforMachine=ChangeInforMachine3;
        }
        private void ChangedInforMachine4 ( )
        {
            MachineChangeInforOrder=4;
            IsChangeInforDisplay=true;
            ChangeInforMachineHeader="Thay đổi đơn hàng máy 4";
            ChangeInforMachine4=new ObservableCollection<ChangeInforOder>( );
            if ( SupervisorMachine4!=null )
            {
                j=SupervisorMachine4.Count( );
                for ( i=0;i<j;i++ )
                {
                    ChangeInforMachine4.Add(new ChangeInforOder(SupervisorMachine4 [i]._Employee,SupervisorMachine4 [i]._ProductID,
                        SupervisorMachine4 [i]._ProductName,SupervisorMachine4 [i]._Boms,SupervisorMachine4 [i]._ProductMass,
                        SupervisorMachine4 [i]._PlannedQuantity,SupervisorMachine4 [i]._ActualQuantity,SupervisorMachine4 [i]._ErrorProduct,
                        SupervisorMachine4 [i]._Note,SupervisorMachine4 [i]._PercentValue,SupervisorMachine4 [i]._TotalQuality,SupervisorMachine4 [i]._ProductTestResult,SupervisorMachine4 [i]._WorkingTime,SupervisorMachine4 [i]._TimeStamp,SupervisorMachine4 [i]._ExecutionTime));
                }
            }
            ChangeInforMachine=ChangeInforMachine4;
        }
        private void ChangedInforMachine5 ( )
        {
            MachineChangeInforOrder=5;
            IsChangeInforDisplay=true;
            ChangeInforMachineHeader="Thay đổi đơn hàng máy 5";
            ChangeInforMachine5=new ObservableCollection<ChangeInforOder>( );
            if ( SupervisorMachine5!=null )
            {
                j=SupervisorMachine5.Count( );
                for ( i=0;i<j;i++ )
                {
                    ChangeInforMachine5.Add(new ChangeInforOder(SupervisorMachine5 [i]._Employee,SupervisorMachine5 [i]._ProductID,
                        SupervisorMachine5 [i]._ProductName,SupervisorMachine5 [i]._Boms,SupervisorMachine5 [i]._ProductMass,
                        SupervisorMachine5 [i]._PlannedQuantity,SupervisorMachine5 [i]._ActualQuantity,SupervisorMachine5 [i]._ErrorProduct,
                        SupervisorMachine5 [i]._Note,SupervisorMachine5 [i]._PercentValue,SupervisorMachine5 [i]._TotalQuality,SupervisorMachine5 [i]._ProductTestResult,SupervisorMachine5 [i]._WorkingTime,SupervisorMachine5 [i]._TimeStamp,SupervisorMachine5 [i]._ExecutionTime));
                }
            }
            ChangeInforMachine=ChangeInforMachine5;
        }
        private void ChangedInforMachine6 ( )
        {
            MachineChangeInforOrder=6;
            IsChangeInforDisplay=true;
            ChangeInforMachineHeader="Thay đổi đơn hàng máy 6";
            ChangeInforMachine6=new ObservableCollection<ChangeInforOder>( );
            if ( SupervisorMachine6!=null )
            {
                j=SupervisorMachine6.Count( );
                for ( i=0;i<j;i++ )
                {
                    ChangeInforMachine6.Add(new ChangeInforOder(SupervisorMachine6 [i]._Employee,SupervisorMachine6 [i]._ProductID,
                        SupervisorMachine6 [i]._ProductName,SupervisorMachine6 [i]._Boms,SupervisorMachine6 [i]._ProductMass,
                        SupervisorMachine6 [i]._PlannedQuantity,SupervisorMachine6 [i]._ActualQuantity,SupervisorMachine6 [i]._ErrorProduct,
                        SupervisorMachine6 [i]._Note,SupervisorMachine6 [i]._PercentValue,SupervisorMachine6 [i]._TotalQuality,SupervisorMachine6 [i]._ProductTestResult,SupervisorMachine6 [i]._WorkingTime,SupervisorMachine6 [i]._TimeStamp
                        ,SupervisorMachine6 [i]._ExecutionTime));
                }
            }
            ChangeInforMachine=ChangeInforMachine6;
        }
        private void Confirm ( )
        {
            if ( ProductID!=""&&Employee!=""&&ProductName!=""&&ProductMass!=0&&PlannedQuantity!=0 )
            {
                MessageBox.ContentText="Bạn có xác nhận đơn hàng?";
                IsDialogOpen=true;
                MessageBoxCount=1;
            }
            else
            {
                WarningBox.ContentText="Bạn nhập đơn hàng chưa đúng!";
                IsWarningDialogOpen=true;
            }
        }
        private void DialogChangeOdersConfirm ( )
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ActualQuantity=0;
                ErrorProduct=0;
                ChangeInforMachine.Add(new ChangeInforOder(Employee,ProductID,ProductName,Boms,ProductMass,PlannedQuantity,ActualQuantity,ErrorProduct,Note,PercentValue,TotalQuality,ProductTestResult,WorkingTime,TimeStamp,ExecutionTime));
                //Employee = "";
                ProductID="";
                ProductName="";
                ProductMass=0;
                PlannedQuantity=0;
                Note="";
            });
        }
        private void Delete ( )
        {
            /// Since your ObservableCollection is created on UI thread, you can only modify it from UI thread and not from other threads.
            /// If you ever need to update objects created on UI thread from different thread 
            /// simply put the delegate on UI Dispatcher and that will do work for you delegating it to UI thread. This will work
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ChangeInforMachine.Remove(SelectRow);
            });
        }
        private void Edit ( )
        {
            MessageBox.ContentText="Bạn muốn thay đổi đơn hàng?";
            IsDialogOpen=true;
            MessageBoxCount=3;
        }
        private void DialogConfirmEdit ( )
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                SelectRow._employee=Employee;
                SelectRow._productID=ProductID;
                SelectRow._productName=ProductName;
                SelectRow._productMass=ProductMass;
                SelectRow._plannedQuantity=PlannedQuantity;
                SelectRow._actualQuantity=ActualQuantity;
                SelectRow._errorProduct=ErrorProduct;
                SelectRow._note=Note;
                ChangeInforOder editInforBoms = new ChangeInforOder(Employee,ProductID,ProductName,Boms,ProductMass,PlannedQuantity,ActualQuantity,ErrorProduct,Note,PercentValue,TotalQuality,ProductTestResult,WorkingTime,TimeStamp,ExecutionTime);

                int i = ChangeInforMachine.IndexOf(SelectRow);
                ChangeInforMachine.Insert(i+1,editInforBoms);
                ChangeInforMachine.RemoveAt(i);
                i=0;
                ProductID="";
                ProductName="";
                ProductMass=0;
                PlannedQuantity=0;
                Note="";
            });
        }
        private void MoveUp ( )
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                if ( SelectRow==null )
                    return;

                int currentIndex = ChangeInforMachine.IndexOf(SelectRow);
                if ( currentIndex>0 )
                    ChangeInforMachine.Move(currentIndex,currentIndex-1);
            });
        }
        private void MoveDown ( )
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                if ( SelectRow==null )
                    return;

                int currentIndex = ChangeInforMachine.IndexOf(SelectRow);
                if ( currentIndex!=-1&&currentIndex<ChangeInforMachine.Count-1 )
                    ChangeInforMachine.Move(currentIndex,currentIndex+1);
            });
        }
        private void Start ( )
        {
            MessageBox.ContentText="Bạn muốn thay đổi đơn hàng?";
            IsDialogOpen=true;
            MessageBoxCount=4;
        }
        private void DialogConfirmStart ( )
        {
            switch ( MachineChangeInforOrder )
            {
                #region machine 1
                case 1:
                    //  save order information to ChangeInforOrderMachine1
                    ChangedInforOrderMachine1.Clear( );
                    j=ChangeInforMachine.Count( );
                    for ( i=0;i<j;i++ )
                    {
                        ChangedInforOrderMachine1.Add(new InforOders("DG1",ChangeInforMachine [i]._employee,ChangeInforMachine [i]._productID,ChangeInforMachine [i]._productName,ChangeInforMachine [i]._boms,
                        ChangeInforMachine [i]._productMass,ChangeInforMachine [i]._plannedQuantity,ChangeInforMachine [i]._actualQuantity,ChangeInforMachine [i]._errorProduct,ChangeInforMachine [i]._note,ChangeInforMachine [i]._percentValue,ChangeInforMachine [i]._totalQuatity,
                        ChangeInforMachine [i]._productTestResult,ChangeInforMachine [i]._workingTime,ChangeInforMachine [i]._timeStamp,ChangeInforMachine [i]._executionTime));
                    }
                    SupervisorMachine1=ChangedInforOrderMachine1;
                    IsChangeInforDisplay=false;
                    SendDataToRasp( );
                    MachineChangeInforOrder=0;
                    break;
                #endregion
                #region machine 2
                case 2:
                    //  save order information to ChangeInforOrderMachine1
                    ChangedInforOrderMachine2.Clear( );
                    j=ChangeInforMachine.Count( );
                    for ( i=0;i<j;i++ )
                    {
                        ChangedInforOrderMachine2.Add(new InforOders("DG2",ChangeInforMachine [i]._employee,ChangeInforMachine [i]._productID,ChangeInforMachine [i]._productName,ChangeInforMachine [i]._boms,
                        ChangeInforMachine [i]._productMass,ChangeInforMachine [i]._plannedQuantity,ChangeInforMachine [i]._actualQuantity,ChangeInforMachine [i]._errorProduct,ChangeInforMachine [i]._note,ChangeInforMachine [i]._percentValue,ChangeInforMachine [i]._totalQuatity,
                        ChangeInforMachine [i]._productTestResult,ChangeInforMachine [i]._workingTime,ChangeInforMachine [i]._timeStamp,ChangeInforMachine [i]._executionTime));
                    }
                    SupervisorMachine2=ChangedInforOrderMachine2;
                    IsChangeInforDisplay=false;
                    MachineChangeInforOrder=0;
                    break;
                #endregion
                #region machine 3
                case 3:
                    //  save order information to ChangeInforOrderMachine1
                    ChangedInforOrderMachine3.Clear( );
                    j=ChangeInforMachine.Count( );
                    for ( i=0;i<j;i++ )
                    {
                        ChangedInforOrderMachine3.Add(new InforOders("DG3",ChangeInforMachine [i]._employee,ChangeInforMachine [i]._productID,ChangeInforMachine [i]._productName,ChangeInforMachine [i]._boms,
                        ChangeInforMachine [i]._productMass,ChangeInforMachine [i]._plannedQuantity,ChangeInforMachine [i]._actualQuantity,ChangeInforMachine [i]._errorProduct,ChangeInforMachine [i]._note,ChangeInforMachine [i]._percentValue,
                        ChangeInforMachine [i]._totalQuatity,ChangeInforMachine [i]._productTestResult,ChangeInforMachine [i]._workingTime,ChangeInforMachine [i]._timeStamp,ChangeInforMachine [i]._executionTime));
                    }
                    SupervisorMachine3=ChangedInforOrderMachine3;
                    IsChangeInforDisplay=false;
                    MachineChangeInforOrder=0;
                    break;
                #endregion
                #region machine 4
                case 4:
                    //  save order information to ChangeInforOrderMachine1
                    ChangedInforOrderMachine4.Clear( );
                    j=ChangeInforMachine.Count( );
                    for ( i=0;i<j;i++ )
                    {
                        ChangedInforOrderMachine4.Add(new InforOders("DG4",ChangeInforMachine [i]._employee,ChangeInforMachine [i]._productID,ChangeInforMachine [i]._productName,ChangeInforMachine [i]._boms,
                        ChangeInforMachine [i]._productMass,ChangeInforMachine [i]._plannedQuantity,ChangeInforMachine [i]._actualQuantity,ChangeInforMachine [i]._errorProduct,ChangeInforMachine [i]._note,
                        ChangeInforMachine [i]._percentValue,ChangeInforMachine [i]._totalQuatity,ChangeInforMachine [i]._productTestResult,ChangeInforMachine [i]._workingTime,ChangeInforMachine [i]._timeStamp,ChangeInforMachine [i]._executionTime));
                    }
                    SupervisorMachine4=ChangedInforOrderMachine4;
                    IsChangeInforDisplay=false;
                    MachineChangeInforOrder=0;
                    break;
                #endregion
                #region machine 5
                case 5:
                    //  save order information to ChangeInforOrderMachine1
                    ChangedInforOrderMachine5.Clear( );
                    j=ChangeInforMachine.Count( );
                    for ( i=0;i<j;i++ )
                    {
                        ChangedInforOrderMachine5.Add(new InforOders("DG5",ChangeInforMachine [i]._employee,ChangeInforMachine [i]._productID,ChangeInforMachine [i]._productName,ChangeInforMachine [i]._boms,
                        ChangeInforMachine [i]._productMass,ChangeInforMachine [i]._plannedQuantity,ChangeInforMachine [i]._actualQuantity,ChangeInforMachine [i]._errorProduct,ChangeInforMachine [i]._note,ChangeInforMachine [i]._percentValue,
                        ChangeInforMachine [i]._totalQuatity,ChangeInforMachine [i]._productTestResult,ChangeInforMachine [i]._workingTime,ChangeInforMachine [i]._timeStamp,ChangeInforMachine [i]._executionTime));
                    }
                    SupervisorMachine5=ChangedInforOrderMachine5;
                    IsChangeInforDisplay=false;
                    MachineChangeInforOrder=0;
                    break;
                #endregion
                #region machine 6
                case 6:
                    ChangedInforOrderMachine6.Clear( );
                    j=ChangeInforMachine.Count( );
                    for ( i=0;i<j;i++ )
                    {
                        ChangedInforOrderMachine6.Add(new InforOders("DG6",ChangeInforMachine [i]._employee,ChangeInforMachine [i]._productID,ChangeInforMachine [i]._productName,ChangeInforMachine [i]._boms,
                        ChangeInforMachine [i]._productMass,ChangeInforMachine [i]._plannedQuantity,ChangeInforMachine [i]._actualQuantity,ChangeInforMachine [i]._errorProduct,ChangeInforMachine [i]._note,ChangeInforMachine [i]._percentValue,ChangeInforMachine [i]._totalQuatity,
                        ChangeInforMachine [i]._productTestResult,ChangeInforMachine [i]._workingTime,ChangeInforMachine [i]._timeStamp,ChangeInforMachine [i]._executionTime));
                    }
                    SupervisorMachine6=ChangedInforOrderMachine6;
                    IsChangeInforDisplay=false;
                    MachineChangeInforOrder=0;
                    break;
                    #endregion
            }
        }
        private void ChangeMass ( )
        {
            if ( Employee!=""&&ProductID!=""&&ProductName!="" )
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
            double averageMassChange = 0;
            averageMassChange=(massSampling1+massSampling2+massSampling3+massSampling4+massSampling5)/5;
            string roundAverageMass;
            roundAverageMass=averageMassChange.ToString("0.0000");
            averageMassChange=Convert.ToDouble(roundAverageMass);
            ProductMass=averageMassChange;
            IsChangeMassViewDisplayed=false;
            changeMassProductID="";
            MassSampling1=0;
            MassSampling2=0;
            MassSampling3=0;
            MassSampling4=0;
            MassSampling5=0;
        }
        private void Decline ( )
        {
            IsChangeInforDisplay=false;
        }
        public async void GetAllItems ( )
        {
            AllProductId=await _apiServices.GetAllItems("");
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
                        //  await Task.Delay(1);
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
        private void Ingredient ( )
        {
            //switch (MachineChangeInforOrder)
            //{
            //case 1:
            //    {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Ingredients.Clear( );
                IsIngredientViewDisplayed=true;
                foreach ( var Product in AllProductId )
                {
                    if ( ProductID==Product.Id.ToString( ) )
                    {
                        //GetItemById();
                        int j = 0;
                        j=ItemById.boms.Count( );
                        for ( int i = 0;i<j;i++ )
                        {
                            _Cardinal=i+1;
                            _IngredientID=ItemById.boms [i].id;
                            _IngredientName=ItemById.boms [i].name;
                            Ingredients.Add(new Ingredient(_Cardinal,_IngredientID,_IngredientName));
                        }
                    }
                }
            });
            #region comment
            //}
            //break;
            //case 2:
            //    {
            //        Application.Current.Dispatcher.Invoke((Action)delegate
            //        {
            //            Ingredients.Clear();
            //            IsIngredientViewDisplayed = true;
            //            foreach (var Product in AllProductId)
            //            {
            //                if (ProductIDMachine2 == Product.Id.ToString())
            //                {
            //                    //GetItemById();
            //                    int j = 0;
            //                    j = ItemById.boms.Count();
            //                    for (int i = 0; i < j; i++)
            //                    {
            //                        _Cardinal = i + 1;
            //                        _IngredientID = ItemById.boms[i].id;
            //                        _IngredientName = ItemById.boms[i].name;
            //                        Ingredients.Add(new Ingredient(_Cardinal, _IngredientID, _IngredientName));
            //                    }
            //                }
            //            }
            //        });
            //    }
            //    break;
            //};
            #endregion
        }
        private void CloseIngredient ( )
        {
            IsIngredientViewDisplayed=false;
        }

        #region getMessage
        private void GetCycleMessage (ValueMessage Message)
        {
            string MachineId;
            DateTime timeStamp;
            MachineId=Message.MachineId;
            int completedProduct;
            int errorProduct;
            int ItemId;
            double workingTime;
            double executionTime;
            double temp;
            int sumActualPro;
            switch ( MachineId )
            {
                case "DG1":
                    timeStamp=Message.Timestamp;
                    completedProduct=Message.CompletedProduct;
                    errorProduct=Message.ErrorProduct;
                    ItemId=Message.ItemId;
                    workingTime=Message.WorkingTime;
                    executionTime=Message.ExecutionTime;
                    sumActualPro=Message.SumActualPro;
                    UpdateData("DG1",ItemId,completedProduct,errorProduct,workingTime,timeStamp,executionTime,sumActualPro);
                    break;
                case "DG2":
                    completedProduct=Message.CompletedProduct;
                    errorProduct=Message.ErrorProduct;
                    ItemId=Message.ItemId;
                    timeStamp=Message.Timestamp;
                    workingTime=Message.WorkingTime;
                    executionTime=Message.ExecutionTime;
                    sumActualPro=Message.SumActualPro;
                    UpdateData("DG2",ItemId,completedProduct,errorProduct,workingTime,timeStamp,executionTime,sumActualPro);
                    break;
                case "DG3":
                    completedProduct=Message.CompletedProduct;
                    errorProduct=Message.ErrorProduct;
                    ItemId=Message.ItemId;
                    timeStamp=Message.Timestamp;
                    workingTime=Message.WorkingTime;
                    executionTime=Message.ExecutionTime;
                    sumActualPro=Message.SumActualPro;
                    UpdateData("DG3",ItemId,completedProduct,errorProduct,workingTime,timeStamp,executionTime,sumActualPro);
                    break;
                case "DG4":
                    completedProduct=Message.CompletedProduct;
                    errorProduct=Message.ErrorProduct;
                    ItemId=Message.ItemId;
                    timeStamp=Message.Timestamp;
                    workingTime=Message.WorkingTime;
                    executionTime=Message.ExecutionTime;
                    sumActualPro=Message.SumActualPro;
                    UpdateData("DG4",ItemId,completedProduct,errorProduct,workingTime,timeStamp,executionTime,sumActualPro);
                    break;
                case "DG5":
                    completedProduct=Message.CompletedProduct;
                    errorProduct=Message.ErrorProduct;
                    ItemId=Message.ItemId;
                    timeStamp=Message.Timestamp;
                    workingTime=Message.WorkingTime;
                    executionTime=Message.ExecutionTime;
                    sumActualPro=Message.SumActualPro;
                    UpdateData("DG5",ItemId,completedProduct,errorProduct,workingTime,timeStamp,executionTime,sumActualPro);
                    break;
                case "DG6":
                    completedProduct=Message.CompletedProduct;
                    errorProduct=Message.ErrorProduct;
                    ItemId=Message.ItemId;
                    timeStamp=Message.Timestamp;
                    workingTime=Message.WorkingTime;
                    executionTime=Message.ExecutionTime;
                    sumActualPro=Message.SumActualPro;
                    UpdateData("DG6",ItemId,completedProduct,errorProduct,workingTime,timeStamp,executionTime,sumActualPro);
                    break;
            }
        }
        private void GetMachineMessage (MachineMessage Message)
        {
            EventMachine eventMachine = new EventMachine( );

            switch ( Message.MachineId )
            {
                case "DG1":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            StatusMachine1Text="Đã kết nối";
                            IsMachine1Connected=true;
                            eventMachine.MachineID="Máy 1";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.TimeStamp=DateTime.Now;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            StatusMachine1Text="Mất kết nối";
                            eventMachine.NameEvent="Mất kết nối";
                            eventMachine.MachineID="Máy 1";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine1Connected=false;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            StatusMachine1Text="Tạm dừng";
                            eventMachine.NameEvent="Tạm dừng";
                            eventMachine.MachineID="Máy 1";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine1Connected=false;
                            ListEvent.Add(eventMachine);
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            StatusMachine1Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 1";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine1Connected=true;
                            ListEvent.Add(eventMachine);
                            break;
                    }
                    break;
                case "DG2":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            StatusMachine2Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 2";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine2Connected=true;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            StatusMachine2Text="Mất kết nối";
                            eventMachine.NameEvent="Mất kết nối";
                            eventMachine.MachineID="Máy 2";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine2Connected=false;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            StatusMachine2Text="Tạm dừng";
                            eventMachine.NameEvent="Tạm dừng";
                            eventMachine.MachineID="Máy 2";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine2Connected=false;
                            ListEvent.Add(eventMachine);
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            StatusMachine2Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 2";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine2Connected=true;
                            ListEvent.Add(eventMachine);
                            break;
                    }
                    break;
                case "DG3":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            StatusMachine3Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 3";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine3Connected=true;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            StatusMachine3Text="Mất kết nối";
                            eventMachine.NameEvent="Mất kết nối";
                            eventMachine.MachineID="Máy 3";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine3Connected=false;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            StatusMachine3Text="Tạm dừng";
                            eventMachine.NameEvent="Tạm dừng";
                            eventMachine.MachineID="Máy 3";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine3Connected=false;
                            ListEvent.Add(eventMachine);
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            StatusMachine3Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 3";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine3Connected=true;
                            ListEvent.Add(eventMachine);
                            break;
                    }
                    break;
                case "DG4":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            StatusMachine4Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 4";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine4Connected=true;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            StatusMachine4Text="Mất kết nối";
                            eventMachine.NameEvent="Mất kết nối";
                            eventMachine.MachineID="Máy 4";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine4Connected=false;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            StatusMachine4Text="Tạm dừng";
                            eventMachine.NameEvent="Tạm dừng";
                            eventMachine.MachineID="Máy 4";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine4Connected=false;
                            ListEvent.Add(eventMachine);
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            StatusMachine4Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 4";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine4Connected=true;
                            ListEvent.Add(eventMachine);
                            break;
                    }
                    break;
                case "DG5":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            StatusMachine5Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 5";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine5Connected=true;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            StatusMachine5Text="Mất kết nối";
                            eventMachine.NameEvent="Mất kết nối";
                            eventMachine.MachineID="Máy 5";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine5Connected=false;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            StatusMachine5Text="Tạm dừng";
                            eventMachine.NameEvent="Tạm dừng";
                            eventMachine.MachineID="Máy 5";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine5Connected=false;
                            ListEvent.Add(eventMachine);
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            StatusMachine5Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 5";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine5Connected=true;
                            ListEvent.Add(eventMachine);
                            break;
                    }
                    break;
                case "DG6":
                    switch ( Message.MachineStatus )
                    {
                        case EMachineStatus.Connected:
                            // Connected
                            StatusMachine6Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 6";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine6Connected=true;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.Disconnected:
                            // Disconnected
                            StatusMachine6Text="Mất kết nối";
                            eventMachine.NameEvent="Mất kết nối";
                            eventMachine.MachineID="Máy 6";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine6Connected=false;
                            ListEvent.Add(eventMachine);
                            break;

                        case EMachineStatus.IdleOn:
                            // Connected
                            StatusMachine6Text="Tạm dừng";
                            eventMachine.NameEvent="Tạm dừng";
                            eventMachine.MachineID="Máy 6";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine6Connected=false;
                            ListEvent.Add(eventMachine);
                            break;
                        case EMachineStatus.IdleOff:
                            // Connected
                            StatusMachine6Text="Đã kết nối";
                            eventMachine.NameEvent="Đã kết nối";
                            eventMachine.MachineID="Máy 6";
                            eventMachine.TimeStamp=DateTime.Now;
                            IsMachine6Connected=true;
                            ListEvent.Add(eventMachine);
                            break;
                    }
                    break;
            }
        }
        #endregion
        double SumActualPro1 = 0;
        double SumActualPro2 = 0;
        double SumActualPro3 = 0;
        double SumActualPro4 = 0;
        double SumActualPro5 = 0;
        double SumActualPro6 = 0;
        private void UpdateData (string machineId,int itemId,int completedProduct,int errorProduct,double workingTime,DateTime timeStamp,double executionTime,int sumActualPro)
        {


            switch ( machineId )
            {
                case "DG1":
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        SumActualPro1=sumActualPro;
                        Employee=SupervisorMachine1 [itemId]._Employee;
                        ProductID=SupervisorMachine1 [itemId]._ProductID;
                        ProductName=SupervisorMachine1 [itemId]._ProductName;
                        Boms=SupervisorMachine1 [itemId]._Boms;
                        ProductMass=SupervisorMachine1 [itemId]._ProductMass;
                        PlannedQuantity=SupervisorMachine1 [itemId]._PlannedQuantity;
                        ActualQuantity=completedProduct;
                        ErrorProduct=errorProduct;
                        WorkingTime=workingTime;
                        TimeStamp=timeStamp;
                        ExecutionTime=executionTime;
                        ProductTestResult="";
                        Note=SupervisorMachine1 [itemId]._Note;
                        PercentValue1=(SumActualPro1/TotalQuality1)*100;
                        SupervisorMachine1.RemoveAt(itemId);
                        SupervisorMachine1.Insert(itemId,new InforOders("DG1",Employee,
                                                    ProductID,ProductName,Boms,
                                                    ProductMass,PlannedQuantity,ActualQuantity,ErrorProduct,Note,PercentValue1,TotalQuality1,ProductTestResult,WorkingTime,TimeStamp,ExecutionTime));
                        _InforOderStoreMachine1.InforOrder=SupervisorMachine1;
                        CurrentProgress1=PercentValue1;
                    });

                    break;
                case "DG2":
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee=SupervisorMachine2 [itemId]._Employee;
                        ProductID=SupervisorMachine2 [itemId]._ProductID;
                        ProductName=SupervisorMachine2 [itemId]._ProductName;
                        Boms=SupervisorMachine2 [itemId]._Boms;
                        ProductMass=SupervisorMachine2 [itemId]._ProductMass;
                        PlannedQuantity=SupervisorMachine2 [itemId]._PlannedQuantity;
                        ActualQuantity=completedProduct;
                        ErrorProduct=errorProduct;
                        WorkingTime=workingTime;
                        TimeStamp=timeStamp;
                        ExecutionTime=executionTime;
                        ProductTestResult="";
                        SumActualPro2=sumActualPro;
                        PercentValue2=(SumActualPro2/TotalQuality2)*100;
                        Note=SupervisorMachine2 [itemId]._Note;
                        SupervisorMachine2.RemoveAt(itemId);
                        SupervisorMachine2.Insert(itemId,new InforOders("DG2",Employee,
                                                    ProductID,ProductName,Boms,
                                                    ProductMass,PlannedQuantity,ActualQuantity,ErrorProduct,Note,PercentValue2,TotalQuality2,ProductTestResult,WorkingTime,TimeStamp,ExecutionTime));
                        _InforOderStoreMachine2.InforOrder=SupervisorMachine2;
                        CurrentProgress2=PercentValue2;
                    });
                    break;

                case "DG3":
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee=SupervisorMachine3 [itemId]._Employee;
                        ProductID=SupervisorMachine3 [itemId]._ProductID;
                        ProductName=SupervisorMachine3 [itemId]._ProductName;
                        Boms=SupervisorMachine3 [itemId]._Boms;
                        ProductMass=SupervisorMachine3 [itemId]._ProductMass;
                        PlannedQuantity=SupervisorMachine3 [itemId]._PlannedQuantity;
                        ActualQuantity=completedProduct;
                        ErrorProduct=errorProduct;
                        WorkingTime=workingTime;
                        TimeStamp=timeStamp;
                        ExecutionTime=executionTime;
                        SumActualPro3=sumActualPro;
                        PercentValue3=(SumActualPro3/TotalQuality3)*100;
                        Note=SupervisorMachine3 [itemId]._Note;
                        SupervisorMachine3.RemoveAt(itemId);
                        SupervisorMachine3.Insert(itemId,new InforOders("DG3",Employee,
                                                    ProductID,ProductName,Boms,
                                                    ProductMass,PlannedQuantity,ActualQuantity,ErrorProduct,Note,PercentValue3,TotalQuality3,ProductTestResult,WorkingTime,TimeStamp,ExecutionTime));
                        _InforOderStoreMachine3.InforOrder=SupervisorMachine3;
                        CurrentProgress3=PercentValue3;
                    });

                    break;
                case "DG4":
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee=SupervisorMachine4 [itemId]._Employee;
                        ProductID=SupervisorMachine4 [itemId]._ProductID;
                        ProductName=SupervisorMachine4 [itemId]._ProductName;
                        Boms=SupervisorMachine4 [itemId]._Boms;
                        ProductMass=SupervisorMachine4 [itemId]._ProductMass;
                        PlannedQuantity=SupervisorMachine4 [itemId]._PlannedQuantity;
                        ActualQuantity=completedProduct;
                        ErrorProduct=errorProduct;
                        WorkingTime=workingTime;
                        TimeStamp=timeStamp;
                        ExecutionTime=executionTime;
                        SumActualPro4=sumActualPro;
                        PercentValue4=(SumActualPro4/TotalQuality4)*100;
                        Note=SupervisorMachine4 [itemId]._Note;
                        SupervisorMachine4.RemoveAt(itemId);
                        SupervisorMachine4.Insert(itemId,new InforOders("DG4",Employee,
                                                    ProductID,ProductName,Boms,
                                                    ProductMass,PlannedQuantity,ActualQuantity,ErrorProduct,Note,PercentValue4,TotalQuality4,ProductTestResult,WorkingTime,TimeStamp,ExecutionTime));
                        _InforOderStoreMachine4.InforOrder=SupervisorMachine4;
                        CurrentProgress4=PercentValue4;
                    });
                    break;
                case "DG5":
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {

                        Employee=SupervisorMachine5 [itemId]._Employee;
                        ProductID=SupervisorMachine5 [itemId]._ProductID;
                        ProductName=SupervisorMachine5 [itemId]._ProductName;
                        Boms=SupervisorMachine5 [itemId]._Boms;
                        ProductMass=SupervisorMachine5 [itemId]._ProductMass;
                        PlannedQuantity=SupervisorMachine5 [itemId]._PlannedQuantity;
                        ActualQuantity=completedProduct;
                        ErrorProduct=errorProduct;
                        WorkingTime=workingTime;
                        TimeStamp=timeStamp;
                        ExecutionTime=executionTime;
                        SumActualPro5=sumActualPro;
                        PercentValue5=(SumActualPro5/TotalQuality5)*100;
                        Note=SupervisorMachine5 [itemId]._Note;
                        SupervisorMachine5.RemoveAt(itemId);
                        SupervisorMachine5.Insert(itemId,new InforOders("DG5",Employee,
                                                    ProductID,ProductName,Boms,
                                                    ProductMass,PlannedQuantity,ActualQuantity,ErrorProduct,Note,PercentValue5,TotalQuality5,ProductTestResult,WorkingTime,TimeStamp,ExecutionTime));
                        _InforOderStoreMachine5.InforOrder=SupervisorMachine5;
                        CurrentProgress5=PercentValue5;
                    });
                    break;
                case "DG6":
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee=SupervisorMachine6 [itemId]._Employee;
                        ProductID=SupervisorMachine6 [itemId]._ProductID;
                        ProductName=SupervisorMachine6 [itemId]._ProductName;
                        Boms=SupervisorMachine6 [itemId]._Boms;
                        ProductMass=SupervisorMachine6 [itemId]._ProductMass;
                        PlannedQuantity=SupervisorMachine6 [itemId]._PlannedQuantity;
                        ActualQuantity=completedProduct;
                        ErrorProduct=errorProduct;
                        WorkingTime=workingTime;
                        TimeStamp=timeStamp;
                        ExecutionTime=executionTime;
                        SumActualPro6=sumActualPro;
                        PercentValue6=(SumActualPro6/TotalQuality6)*100;
                        Note=SupervisorMachine6 [itemId]._Note;
                        SupervisorMachine6.RemoveAt(itemId);
                        SupervisorMachine6.Insert(itemId,new InforOders("DG6",Employee,
                                                    ProductID,ProductName,Boms,
                                                    ProductMass,PlannedQuantity,ActualQuantity,ErrorProduct,Note,PercentValue6,TotalQuality6,ProductTestResult,WorkingTime,TimeStamp,ExecutionTime));
                        _InforOderStoreMachine6.InforOrder=SupervisorMachine6;
                        CurrentProgress6=PercentValue6;
                    });
                    break;
            }
        }
        private async void SendDataToRasp ( )
        {
            var endpoint = await _busControl.GetSendEndpoint(new Uri("http://127.0.0.1:8181/send-config"));
            int j = 0;
            switch ( MachineChangeInforOrder )
            {
                case 1:
                    j=SupervisorMachine1.Count( );
                    Items=new List<ItemRasConfigurationMessage>( );
                    for ( int i = 0;i<j;i++ )
                    {
                        int SetpointTotal = SupervisorMachine1 [i]._PlannedQuantity;
                        string ProductId = SupervisorMachine1 [i]._ProductID;
                        string ProductName = SupervisorMachine1 [i]._ProductName;
                        List<string> boms = new List<string>( );
                        boms.Add("no");
                        double ProductMass = SupervisorMachine1 [i]._ProductMass;
                        string Standard = "50 gói / 1 thùng";
                        int CompletedProduct = SupervisorMachine1 [i]._ActualQuantity;
                        int ErrorProduct = SupervisorMachine1 [i]._ErrorProduct;
                        Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
                    }
                    RasConfigurationMessage mess = new RasConfigurationMessage { MachineId="DG1",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
                    await endpoint.Send<RasConfigurationMessage>(mess);
                    break;
                case 2:
                    j=SupervisorMachine2.Count( );
                    Items=new List<ItemRasConfigurationMessage>( );
                    for ( int i = 0;i<j;i++ )
                    {
                        int SetpointTotal = SupervisorMachine2 [i]._PlannedQuantity;
                        string ProductId = SupervisorMachine2 [i]._ProductID;
                        string ProductName = SupervisorMachine2 [i]._ProductName;
                        List<string> boms = new List<string>( );
                        boms.Add("no");
                        double ProductMass = SupervisorMachine2 [i]._ProductMass;
                        string Standard = "50 gói / 1 thùng";
                        int CompletedProduct = SupervisorMachine2 [i]._ActualQuantity;
                        int ErrorProduct = SupervisorMachine2 [i]._ErrorProduct;
                        Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
                    }
                    mess=new RasConfigurationMessage { MachineId="DG2",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
                    await endpoint.Send<RasConfigurationMessage>(mess);
                    break;

                case 3:
                    j=SupervisorMachine3.Count( );
                    Items=new List<ItemRasConfigurationMessage>( );
                    for ( int i = 0;i<j;i++ )
                    {
                        int SetpointTotal = SupervisorMachine3 [i]._PlannedQuantity;
                        string ProductId = SupervisorMachine3 [i]._ProductID;
                        string ProductName = SupervisorMachine3 [i]._ProductName;
                        List<string> boms = new List<string>( );
                        boms.Add("no");
                        double ProductMass = SupervisorMachine3 [i]._ProductMass;
                        string Standard = "50 gói / 1 thùng";
                        int CompletedProduct = SupervisorMachine3 [i]._ActualQuantity;
                        int ErrorProduct = SupervisorMachine3 [i]._ErrorProduct;
                        Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
                    }
                    mess=new RasConfigurationMessage { MachineId="DG3",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
                    await endpoint.Send<RasConfigurationMessage>(mess);
                    break;
                case 4:
                    j=SupervisorMachine4.Count( );
                    Items=new List<ItemRasConfigurationMessage>( );
                    for ( int i = 0;i<j;i++ )
                    {
                        int SetpointTotal = SupervisorMachine4 [i]._PlannedQuantity;
                        string ProductId = SupervisorMachine4 [i]._ProductID;
                        string ProductName = SupervisorMachine4 [i]._ProductName;
                        List<string> boms = new List<string>( );
                        boms.Add("no");
                        double ProductMass = SupervisorMachine4 [i]._ProductMass;
                        string Standard = "50 gói / 1 thùng";
                        int CompletedProduct = SupervisorMachine4 [i]._ActualQuantity;
                        int ErrorProduct = SupervisorMachine4 [i]._ErrorProduct;
                        Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
                    }
                    mess=new RasConfigurationMessage { MachineId="DG4",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
                    await endpoint.Send<RasConfigurationMessage>(mess);
                    break;
                case 5:
                    j=SupervisorMachine5.Count( );
                    Items=new List<ItemRasConfigurationMessage>( );
                    for ( int i = 0;i<j;i++ )
                    {
                        int SetpointTotal = SupervisorMachine5 [i]._PlannedQuantity;
                        string ProductId = SupervisorMachine5 [i]._ProductID;
                        string ProductName = SupervisorMachine5 [i]._ProductName;
                        List<string> boms = new List<string>( );
                        boms.Add("no");
                        double ProductMass = SupervisorMachine5 [i]._ProductMass;
                        string Standard = "50 gói / 1 thùng";
                        int CompletedProduct = SupervisorMachine5 [i]._ActualQuantity;
                        int ErrorProduct = SupervisorMachine5 [i]._ErrorProduct;
                        Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
                    }
                    mess=new RasConfigurationMessage { MachineId="DG5",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
                    await endpoint.Send<RasConfigurationMessage>(mess);
                    break;
                case 6:
                    j=SupervisorMachine6.Count( );
                    Items=new List<ItemRasConfigurationMessage>( );
                    for ( int i = 0;i<j;i++ )
                    {
                        int SetpointTotal = SupervisorMachine6 [i]._PlannedQuantity;
                        string ProductId = SupervisorMachine6 [i]._ProductID;
                        string ProductName = SupervisorMachine6 [i]._ProductName;
                        List<string> boms = new List<string>( );
                        boms.Add("no");
                        double ProductMass = SupervisorMachine6 [i]._ProductMass;
                        string Standard = "50 gói / 1 thùng";
                        int CompletedProduct = SupervisorMachine6 [i]._ActualQuantity;
                        int ErrorProduct = SupervisorMachine6 [i]._ErrorProduct;
                        Items.Add(new ItemRasConfigurationMessage { SetpointTotal=SetpointTotal,ProductId=ProductId,ProductName=ProductName,Boms=boms,ProductMass=ProductMass,Standard=Standard,CompletedProduct=CompletedProduct,ErrorProduct=ErrorProduct });
                    }
                    mess=new RasConfigurationMessage { MachineId="DG6",Timestamp=DateTime.UtcNow,Quantity=j,Items=Items };
                    await endpoint.Send<RasConfigurationMessage>(mess);
                    break;
            }
        }

    }
}
