using PackingMachine.core.Components;
using PackingMachine.core.Domain.Model.ImportExcel;
using PackingMachine.core.Service.Interface;
using PackingMachine.core.ViewModel.ViewModelBase;
using PackingMachine.ModuleSeperateOrder;
using PackingMachine.ModuleSeperateOrder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Employee = PackingMachine.ModuleSeperateOrder.Models.Employee;

namespace PackingMachine.core.ViewModel.SettingViewModel
{
    public class FirstInforSettingViewModel: BaseViewModel
    {
        private readonly IExcelExportService _excelExportService;
        private string performance = "";
        public string Performance { get { return performance; } set { performance=value; OnPropertyChanged( ); } }
        public int selectedindex = 0;
        public int SelectedIndex { get { return selectedindex; } set { selectedindex=value; OnPropertyChanged( ); } }
        private ObservableCollection<string> itemSourceEmployee = new ObservableCollection<string>( )
        {
            "Sang","Gia","Thanh","Trí","Tín","Dũng", "Nhi","Tú","Thái","Sĩ"
        };
        public ObservableCollection<string> ItemSourceEmployee { get { return itemSourceEmployee; } set { itemSourceEmployee=value; OnPropertyChanged( ); } }
        private ObservableCollection<EmployeeForOrderSettingViewModel> employees = new ObservableCollection<EmployeeForOrderSettingViewModel>( );
        public ObservableCollection<EmployeeForOrderSettingViewModel> Employees
        {
            get { return employees; }
            set { employees=value; OnPropertyChanged( ); }

        }
        private ObservableCollection<AssemblyTaskForOrderSettingViewModel> assignmentTask = new ObservableCollection<AssemblyTaskForOrderSettingViewModel>( );
        public ObservableCollection<AssemblyTaskForOrderSettingViewModel> AssignmentTasks { get { return assignmentTask; } set { assignmentTask=value; OnPropertyChanged( ); } }
        public ICommand ImportExcelCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ExportExcelCommand { get; set; }

        public FirstInforSettingViewModel (IExcelExportService excelReportService)
        {
            _excelExportService=excelReportService;
            ImportExcelCommand=new RelayCommand(async ( ) => ImportExcel( ));
            AddCommand=new RelayCommand(async ( ) => AddEmployee( ));
            DeleteCommand=new RelayCommand(async ( ) => DeleteEmployee( ));
            ExportExcelCommand=new RelayCommand(async ( ) => ImplementAlgorithm( ));
        }

        private void ImplementAlgorithm ( )
        {
            List<Employee> assemblyEmployees = new List<Employee>( );
            foreach ( var item in Employees )
            {
                assemblyEmployees.Add(new Employee(item.Id,Convert.ToDecimal(item.Performance)));
            }
            List<AssemblyTask> assemblyTasks = new List<AssemblyTask>( );
            foreach ( var item in AssignmentTasks )
            {
                assemblyTasks.Add(new AssemblyTask(item.Id,item.Name,Convert.ToDecimal(item.Workload),Convert.ToInt32(item.Priority),Convert.ToInt16(item.ProductQuantity)));
            }
            var a = assemblyTasks.Sum(x => x.Workload)%1!=0;
            if ( assemblyTasks.Sum(x => x.Workload)%1==0 )
            {
                if ( assemblyEmployees.Sum(e => e.Performance)==assemblyTasks.Sum(t => t.Workload) )
                {
                    var final = TaskAssignmentService.AssignTasks(assemblyEmployees,assemblyTasks);
                    _excelExportService.ExportReportOrder(final);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(new Action(( ) =>
                    {
                        CustomMessageBox.Show("Số lượng công nhân chưa đúng!","Cảnh báo",MessageBoxButton.OK,MessageBoxImage.Warning);

                        //WarningBox.ContentText="Bạn nhập đơn hàng chưa đúng!";
                        //IsWarningDialogOpen=true;
                    }));
                }
            }

        }
        private void DeleteEmployee ( )
        {
            Employees.RemoveAt(SelectedIndex);
            OnPropertyChanged("Employees");
        }

        private void AddEmployee ( )
        {
            if ( decimal.TryParse(Performance,out _) )
            {
                EmployeeForOrderSettingViewModel employee = new EmployeeForOrderSettingViewModel( );
                employee.Id=ItemSourceEmployee [SelectedIndex];
                employee.Performance=Performance;
                Employees.Add(employee);
                OnPropertyChanged("Employees");
            }
        }
        private async void ImportExcel ( )
        {
            ObservableCollection<AssemblyTaskForOrderSettingViewModel> assigns = new ObservableCollection<AssemblyTaskForOrderSettingViewModel>( );
            var data = await _excelExportService.ImportSeperateOrder( );
            if ( data.Success )
            {
                foreach ( var item in data.Resource )
                {
                    AssemblyTaskForOrderSettingViewModel assembly = new AssemblyTaskForOrderSettingViewModel( )
                    {
                        Id=item.Id,
                        Name=item.Name,
                        Workload=String.Format("{0:0.00}",item.Workload),
                        ProductQuantity=Convert.ToString(item.ProductQuantity),
                        Priority=Convert.ToString(item.Priority),
                    };
                    assigns.Add(assembly);
                }
            }
            AssignmentTasks=assigns;
        }
    }
}
