using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.Api;
using PackingMachine.core.Domain.Model.Api.Shift;
using PackingMachine.core.Service;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.Store;
using PackingMachine.core.ViewModel.Components.MessageBox;
using PackingMachine.core.ViewModel.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace PackingMachine.core.ViewModel.ReportViewModel
{

    public class PackingReportViewModel: BaseViewModel

    {
        private readonly NavigationStore _navigationStore;
        public ViewModel.ViewModelBase.BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        //command

        #region KHAIBAO

        private int _Cardinal;
        public int Cardinal
        {
            get { return _Cardinal; }
            set { _Cardinal=value; OnPropertyChanged( ); }

        }
        private string _Day;
        public string Day
        {
            get { return _Day; }
            set { _Day=value; OnPropertyChanged( ); }

        }
        private string _ProductID;
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID=value; OnPropertyChanged( ); }

        }
        private string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName=value; OnPropertyChanged( ); }

        }
        private string _Unit;
        public string Unit
        {
            get { return _Unit; }
            set { _Unit=value; OnPropertyChanged( ); }

        }
        private int _ActualQuantity;
        public int ActualQuantity
        {
            get { return _ActualQuantity; }
            set { _ActualQuantity=value; OnPropertyChanged( ); }

        }
        private int _PlannedQuantity;
        public int PlannedQuantity
        {
            get { return _PlannedQuantity; }
            set { _PlannedQuantity=value; OnPropertyChanged( ); }

        }
        private string _ProductTestResult;
        public string ProductTestResult
        {
            get { return _ProductTestResult; }
            set { _ProductTestResult=value; OnPropertyChanged(nameof(ProductTestResult)); }

        }
        private string _Employee;
        public string Employee
        {
            get { return _Employee; }
            set { _Employee=value; OnPropertyChanged( ); }

        }
        private double _WorkingTime;
        public double WorkingTime
        {
            get { return _WorkingTime; }
            set { _WorkingTime=value; OnPropertyChanged(nameof(WorkingTime)); }

        }
        private string _Note;
        public string Note
        {
            get { return _Note; }
            set { _Note=value; OnPropertyChanged( ); }

        }
        private DateTime _date;
        public DateTime date
        {
            get => _date;
            set { _date=value; OnPropertyChanged( ); }
        }

        private int i, j;
        private int p, h, k, l, m, n;

        private string _CurrentDay;
        private string _WorkingDay;
        public string WorkingDay
        {
            get { return _WorkingDay; }
            set { _WorkingDay=value; OnPropertyChanged( ); }
        }
        #endregion


        #region COMMAND
        private String _ReportName;
        public String ReportName
        {
            get { return _ReportName; }
            set { _ReportName=value; OnPropertyChanged( ); }
        }
        private ObservableCollection<Report> _PackingReport;

        public ObservableCollection<Report> PackingReport { get => _PackingReport; set { _PackingReport=value; OnPropertyChanged( ); } }
        private PageStoreMachine1 _InforOderStoreMachine1;
        // Export Excel

        private bool saveFlag { get; set; }

        // Command
        public ICommand AccessCommand { set; get; }
        public ICommand ExportExcelCommand { set; get; }
        public ICommand PushCommand { set; get; }
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
        // machine 2
        private PageStoreMachine2 _InforOderStoreMachine2;
        // machine 3
        private PageStoreMachine3 _InforOderStoreMachine3;
        // machine 4
        private PageStoreMachine4 _InforOderStoreMachine4;
        // machine 5
        private PageStoreMachine5 _InforOderStoreMachine5;
        // machine 6
        private PageStoreMachine6 _InforOderStoreMachine6;
        #region PUSH REPORT
        private ObservableCollection<ShiftReport> _shiftReport;
        public ObservableCollection<ShiftReport> ShiftReport { get => _shiftReport; set { _shiftReport=value; OnPropertyChanged( ); } }
        private List<ItemShift> _itemShift;
        public List<ItemShift> ItemShift { get => _itemShift; set { _itemShift=value; OnPropertyChanged( ); } }
        #endregion
        private Employee _employeePush;
        public Employee EmployeePush { get => _employeePush; set { _employeePush=value; OnPropertyChanged( ); } }
        private IApiServices _apiServices;
        private ObservableCollection<Employee> _allEmployee;
        public ObservableCollection<Employee> AllEmployee { get => _allEmployee; set { _allEmployee=value; OnPropertyChanged( ); } }


        #endregion

        public PackingReportViewModel (IApiServices apiServices,PageStoreMachine1 InforOderStoreMachine1,PageStoreMachine2 InforOderStoreMachine2,PageStoreMachine3 InforOderStoreMachine3,PageStoreMachine4 InforOderStoreMachine4,PageStoreMachine5 InforOderStoreMachine5,PageStoreMachine6 InforOderStoreMachine6)
        {
            _InforOderStoreMachine1=InforOderStoreMachine1;
            PackingReport=new ObservableCollection<Report>( );

            // Access command
            AccessCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Access( )));
            BindingOperations.EnableCollectionSynchronization(PackingReport,Access);
            // Export Excel
            ExportExcelCommand=new RelayCommand(async ( ) => await Task.Run(( ) => ExportExcel( )));
            BindingOperations.EnableCollectionSynchronization(PackingReport,ExportExcel);
            PushCommand=new RelayCommand(async ( ) => await Task.Run(( ) => Push( )));
            BindingOperations.EnableCollectionSynchronization(PackingReport,Push);

            WorkingDay=DateTime.Now.ToString("dd/MM/yyyy");
            ReportName=WorkingDay.ToString( )+" Báo cáo đóng gói";


            var firstDayOfMonth = new DateTime(date.Year,date.Month,1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            // message box 
            // Xác nhận đơn hàng
            MessageBox=new MessageBoxViewModel( );
            MessageBox.Confirm+=DialogConfirm;
            MessageBox.Cancel+=DialogClose;

            // WarningBox
            WarningBox=new WarningBoxViewModel( );
            WarningBox.WarningConfirm+=WarningConfirm;
            // machine 2
            _InforOderStoreMachine2=InforOderStoreMachine2;
            // machine 3
            _InforOderStoreMachine3=InforOderStoreMachine3;
            // machine 4
            _InforOderStoreMachine4=InforOderStoreMachine4;
            // machine 5
            _InforOderStoreMachine5=InforOderStoreMachine5;
            // machine 6
            _InforOderStoreMachine6=InforOderStoreMachine6;
            ShiftReport=new ObservableCollection<ShiftReport>( );
            ItemShift=new List<ItemShift>( );
            _apiServices=apiServices;
            GetEmployee( );
        }
        private void DialogConfirm ( )
        {
            switch ( MessageBoxCount )
            {
                case 1:
                    IsDialogOpen=false;
                    MessageBoxCount=0;
                    DialogExportExcel( );

                    break;
                case 2:
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
        private void Access ( )
        {

            PackingReport.Clear( );

            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int e = 0;
            int f = 0;
            // phklmn
            if ( _InforOderStoreMachine1.InforOrder!=null )
            {
                a=_InforOderStoreMachine1.InforOrder.Count( );
                for ( i=0;i<a;i++ )
                {
                    Cardinal=i+1;
                    Day=WorkingDay;
                    ProductID=_InforOderStoreMachine1.InforOrder [i]._ProductID;
                    ProductName=_InforOderStoreMachine1.InforOrder [i]._ProductName;
                    Unit="DG1";
                    ActualQuantity=_InforOderStoreMachine1.InforOrder [i]._ActualQuantity;
                    PlannedQuantity=_InforOderStoreMachine1.InforOrder [i]._PlannedQuantity;
                    if ( ActualQuantity==PlannedQuantity )
                    {
                        ProductTestResult="Đạt";
                    }
                    else ProductTestResult="Chưa hoàn thành";
                    WorkingTime=_InforOderStoreMachine1.InforOrder [i]._WorkingTime;
                    Employee=_InforOderStoreMachine1.InforOrder [i]._Employee;
                    if ( WorkingTime>5 )
                    {
                        Note="Chậm tiến độ";
                    }
                    else Note="Không";
                    PackingReport.Add(new Report(Cardinal,Day,ProductID,ProductName,Unit,ActualQuantity,PlannedQuantity,ProductTestResult,Employee,WorkingTime,Note));
                }
            }
            if ( _InforOderStoreMachine2.InforOrder!=null )
            {
                a=_InforOderStoreMachine2.InforOrder.Count( );
                for ( i=0;i<a;i++ )
                {
                    Cardinal=a+i+1;
                    Day=WorkingDay;
                    ProductID=_InforOderStoreMachine2.InforOrder [i]._ProductID;
                    ProductName=_InforOderStoreMachine2.InforOrder [i]._ProductName;
                    Unit="DG2";
                    ActualQuantity=_InforOderStoreMachine2.InforOrder [i]._ActualQuantity;
                    PlannedQuantity=_InforOderStoreMachine2.InforOrder [i]._PlannedQuantity;
                    if ( ActualQuantity==PlannedQuantity )
                    {
                        ProductTestResult="Đạt";
                    }
                    else ProductTestResult="Chưa hoàn thành";
                    WorkingTime=_InforOderStoreMachine2.InforOrder [i]._WorkingTime;
                    Employee=_InforOderStoreMachine2.InforOrder [i]._Employee;
                    if ( WorkingTime>5 )
                    {
                        Note="Chậm tiến độ";
                    }
                    else Note="Không";
                    PackingReport.Add(new Report(Cardinal,Day,ProductID,ProductName,Unit,ActualQuantity,PlannedQuantity,ProductTestResult,Employee,WorkingTime,Note));
                }
            }
            if ( _InforOderStoreMachine3.InforOrder!=null )
            {
                c=_InforOderStoreMachine3.InforOrder.Count( );
                for ( i=0;i<c;i++ )
                {
                    Cardinal=a+b+i+1;
                    Day=WorkingDay;
                    ProductID=_InforOderStoreMachine3.InforOrder [i]._ProductID;
                    ProductName=_InforOderStoreMachine3.InforOrder [i]._ProductName;
                    Unit="DG3";
                    ActualQuantity=_InforOderStoreMachine3.InforOrder [i]._ActualQuantity;
                    PlannedQuantity=_InforOderStoreMachine3.InforOrder [i]._PlannedQuantity;
                    if ( ActualQuantity==PlannedQuantity )
                    {
                        ProductTestResult="Đạt";
                    }
                    else ProductTestResult="Chưa hoàn thành";
                    WorkingTime=_InforOderStoreMachine3.InforOrder [i]._WorkingTime;
                    Employee=_InforOderStoreMachine3.InforOrder [i]._Employee;
                    if ( WorkingTime>5 )
                    {
                        Note="Chậm tiến độ";
                    }
                    else Note="Không";
                    PackingReport.Add(new Report(Cardinal,Day,ProductID,ProductName,Unit,ActualQuantity,PlannedQuantity,ProductTestResult,Employee,WorkingTime,Note));
                }
            };
            if ( _InforOderStoreMachine4.InforOrder!=null )
            {
                d=_InforOderStoreMachine4.InforOrder.Count( );
                for ( i=0;i<d;i++ )
                {
                    Cardinal=a+b+c+i+1;
                    Day=WorkingDay;
                    ProductID=_InforOderStoreMachine4.InforOrder [i]._ProductID;
                    ProductName=_InforOderStoreMachine4.InforOrder [i]._ProductName;
                    Unit="DG4";
                    ActualQuantity=_InforOderStoreMachine4.InforOrder [i]._ActualQuantity;
                    PlannedQuantity=_InforOderStoreMachine4.InforOrder [i]._PlannedQuantity;
                    if ( ActualQuantity==PlannedQuantity )
                    {
                        ProductTestResult="Đạt";
                    }
                    else ProductTestResult="Chưa hoàn thành";
                    WorkingTime=_InforOderStoreMachine4.InforOrder [i]._WorkingTime;
                    Employee=_InforOderStoreMachine4.InforOrder [i]._Employee;
                    if ( WorkingTime>5 )
                    {
                        Note="Chậm tiến độ";
                    }
                    else Note="Không";
                    PackingReport.Add(new Report(Cardinal,Day,ProductID,ProductName,Unit,ActualQuantity,PlannedQuantity,ProductTestResult,Employee,WorkingTime,Note));
                }
            };
            if ( _InforOderStoreMachine5.InforOrder!=null )
            {
                e=_InforOderStoreMachine5.InforOrder.Count( );
                for ( i=0;i<e;i++ )
                {
                    Cardinal=a+b+c+d+i+1;
                    Day=WorkingDay;
                    ProductID=_InforOderStoreMachine5.InforOrder [i]._ProductID;
                    ProductName=_InforOderStoreMachine5.InforOrder [i]._ProductName;
                    Unit="DG5";
                    ActualQuantity=_InforOderStoreMachine5.InforOrder [i]._ActualQuantity;
                    PlannedQuantity=_InforOderStoreMachine5.InforOrder [i]._PlannedQuantity;
                    if ( ActualQuantity==PlannedQuantity )
                    {
                        ProductTestResult="Đạt";
                    }
                    else ProductTestResult="Chưa hoàn thành";
                    WorkingTime=_InforOderStoreMachine5.InforOrder [i]._WorkingTime;
                    Employee=_InforOderStoreMachine5.InforOrder [i]._Employee;
                    if ( WorkingTime>5 )
                    {
                        Note="Chậm tiến độ";
                    }
                    else Note="Không";
                    PackingReport.Add(new Report(Cardinal,Day,ProductID,ProductName,Unit,ActualQuantity,PlannedQuantity,ProductTestResult,Employee,WorkingTime,Note));
                }
            };
            if ( _InforOderStoreMachine6.InforOrder!=null )
            {
                f=_InforOderStoreMachine6.InforOrder.Count( );
                for ( i=0;i<f;i++ )
                {
                    Cardinal=a+b+c+d+e+i+1;
                    Day=WorkingDay;
                    ProductID=_InforOderStoreMachine6.InforOrder [i]._ProductID;
                    ProductName=_InforOderStoreMachine6.InforOrder [i]._ProductName;
                    Unit="DG6";
                    ActualQuantity=_InforOderStoreMachine6.InforOrder [i]._ActualQuantity;
                    PlannedQuantity=_InforOderStoreMachine6.InforOrder [i]._PlannedQuantity;
                    if ( ActualQuantity==PlannedQuantity )
                    {
                        ProductTestResult="Đạt";
                    }
                    else ProductTestResult="Chưa hoàn thành";
                    WorkingTime=_InforOderStoreMachine6.InforOrder [i]._WorkingTime;
                    Employee=_InforOderStoreMachine6.InforOrder [i]._Employee;
                    if ( WorkingTime>5 )
                    {
                        Note="Chậm tiến độ";
                    }
                    else Note="Không";
                    PackingReport.Add(new Report(Cardinal,Day,ProductID,ProductName,Unit,ActualQuantity,PlannedQuantity,ProductTestResult,Employee,WorkingTime,Note));
                }

            };
        }
        private void ExportExcel ( )
        {
            MessageBox.ContentText="Bạn muốn xuất Excel?";
            IsDialogOpen=true;
            MessageBoxCount=1;
        }
        private async void Push ( )
        {
            date=DateTime.Now;

            int a = _InforOderStoreMachine1.InforOrder.Count;
            int b = _InforOderStoreMachine2.InforOrder.Count;
            int c = _InforOderStoreMachine3.InforOrder.Count;
            int d = _InforOderStoreMachine4.InforOrder.Count;
            int e = _InforOderStoreMachine5.InforOrder.Count;
            int f = _InforOderStoreMachine6.InforOrder.Count;
            if ( a>0 )
            {
                for ( int i = 0;i<a;i++ )
                {
                    ItemShift.Add(new ItemShift(_InforOderStoreMachine1.InforOrder [i]._ProductID,_InforOderStoreMachine1.InforOrder [i]._PlannedQuantity,_InforOderStoreMachine1.InforOrder [i]._ActualQuantity,_InforOderStoreMachine1.InforOrder [i]._Note,0));
                }
                foreach ( Employee g in AllEmployee )
                {
                    if ( _InforOderStoreMachine1.InforOrder [0]._Employee==g.FirstName )
                    {
                        EmployeePush=g;
                    }
                }

                ShiftReport.Add(new ShiftReport(_date,EmployeePush,_InforOderStoreMachine1.InforOrder [0]._PackingUnit,ItemShift,_InforOderStoreMachine1.InforOrder [0]._WorkingTime));
            }
            if ( b>0 )
            {
                for ( int i = 0;i<b;i++ )
                {
                    ItemShift.Add(new ItemShift(_InforOderStoreMachine2.InforOrder [i]._ProductID,_InforOderStoreMachine2.InforOrder [i]._PlannedQuantity,_InforOderStoreMachine2.InforOrder [i]._ActualQuantity,_InforOderStoreMachine2.InforOrder [i]._Note,0));
                }
                foreach ( Employee g in AllEmployee )
                {
                    if ( _InforOderStoreMachine2.InforOrder [0]._Employee==g.FirstName )
                    {
                        EmployeePush=g;
                    }
                }

                ShiftReport.Add(new ShiftReport(_date,EmployeePush,_InforOderStoreMachine2.InforOrder [0]._PackingUnit,ItemShift,_InforOderStoreMachine2.InforOrder [0]._WorkingTime));
            }
            if ( c>0 )
            {
                for ( int i = 0;i<c;i++ )
                {
                    ItemShift.Add(new ItemShift(_InforOderStoreMachine3.InforOrder [i]._ProductID,_InforOderStoreMachine3.InforOrder [i]._PlannedQuantity,_InforOderStoreMachine3.InforOrder [i]._ActualQuantity,_InforOderStoreMachine3.InforOrder [i]._Note,0));
                }
                foreach ( Employee g in AllEmployee )
                {
                    if ( _InforOderStoreMachine3.InforOrder [0]._Employee==g.FirstName )
                    {
                        EmployeePush=g;
                    }
                }

                ShiftReport.Add(new ShiftReport(_date,EmployeePush,_InforOderStoreMachine3.InforOrder [0]._PackingUnit,ItemShift,_InforOderStoreMachine3.InforOrder [0]._WorkingTime));
            }
            if ( d>0 )
            {
                for ( int i = 0;i<d;i++ )
                {
                    ItemShift.Add(new ItemShift(_InforOderStoreMachine4.InforOrder [i]._ProductID,_InforOderStoreMachine4.InforOrder [i]._PlannedQuantity,_InforOderStoreMachine4.InforOrder [i]._ActualQuantity,_InforOderStoreMachine4.InforOrder [i]._Note,0));
                }
                foreach ( Employee g in AllEmployee )
                {
                    if ( _InforOderStoreMachine4.InforOrder [0]._Employee==g.FirstName )
                    {
                        EmployeePush=g;
                    }
                }

                ShiftReport.Add(new ShiftReport(_date,EmployeePush,_InforOderStoreMachine4.InforOrder [0]._PackingUnit,ItemShift,_InforOderStoreMachine4.InforOrder [0]._WorkingTime));
            }
            if ( e>0 )
            {
                for ( int i = 0;i<e;i++ )
                {
                    ItemShift.Add(new ItemShift(_InforOderStoreMachine5.InforOrder [i]._ProductID,_InforOderStoreMachine5.InforOrder [i]._PlannedQuantity,_InforOderStoreMachine5.InforOrder [i]._ActualQuantity,_InforOderStoreMachine5.InforOrder [i]._Note,0));
                }
                foreach ( Employee g in AllEmployee )
                {
                    if ( _InforOderStoreMachine5.InforOrder [0]._Employee==g.FirstName )
                    {
                        EmployeePush=g;
                    }
                }

                ShiftReport.Add(new ShiftReport(_date,EmployeePush,_InforOderStoreMachine5.InforOrder [0]._PackingUnit,ItemShift,_InforOderStoreMachine5.InforOrder [0]._WorkingTime));
            }
            if ( f>0 )
            {
                for ( int i = 0;i<f;i++ )
                {
                    ItemShift.Add(new ItemShift(_InforOderStoreMachine6.InforOrder [i]._ProductID,_InforOderStoreMachine6.InforOrder [i]._PlannedQuantity,_InforOderStoreMachine6.InforOrder [i]._ActualQuantity,_InforOderStoreMachine6.InforOrder [i]._Note,0));
                }
                foreach ( Employee g in AllEmployee )
                {
                    if ( _InforOderStoreMachine6.InforOrder [0]._Employee==g.FirstName )
                    {
                        EmployeePush=g;
                    }
                }
                i=0;
                ShiftReport.Add(new ShiftReport(_date,EmployeePush,_InforOderStoreMachine6.InforOrder [0]._PackingUnit,ItemShift,_InforOderStoreMachine6.InforOrder [0]._WorkingTime));
            }

            await _apiServices.PostShiftReport("",ShiftReport);
            ItemShift.Clear( );
            ShiftReport.Clear( );
            IsDialogOpen=true;
            MessageBox.ContentText="Gửi lên máy chủ thành công!";
            MessageBoxCount=2;
        }
        private async Task DialogExportExcel ( )
        {
            await RunCommandAsync(saveFlag,async ( ) =>
            {
                //await Task.Delay(1);
                ExportExcelService exportExcel = new ExportExcelService( );
                var serviceResponse = await exportExcel.ExportTest(@"ReportTemplate.xlsx",PackingReport);
                if ( serviceResponse.Success )
                {
                    MessageBox.ContentText="Xuất Excel thành công!";
                    IsDialogOpen=true;
                    MessageBoxCount=2;
                }
            });
        }
        public async void GetEmployee ( )
        {
            await Task.Delay(1);
            AllEmployee=await _apiServices.GetEmployee("");
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
