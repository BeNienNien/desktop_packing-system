using Microsoft.Win32;
using OfficeOpenXml;
using PackingMachine.core.Domain.Communication;
using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.ImportExcel;
using PackingMachine.core.Doman.Model;
using PackingMachine.core.Service.Interface;
using PackingMachine.Core.Domain.Communication;
using PackingMachine.ModuleSeperateOrder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Employee = PackingMachine.core.Domain.Model.Api.Employee;

namespace PackingMachine.core.Service
{
    public class ExportExcelService: IExcelExportService
    {
        private ExcelPackage package;
        private ExcelWorksheet worksheet;

        private bool isExportDialogOpenned = false;

        public ExportExcelService ( )
        {

        }
        private async Task<ServiceResponse> ExportExcelFile ( )
        {
            string filePath = "Báo cáo";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog( );
            dialog.Filter="Excel | *.xlsx";
            dialog.FileName=DateTime.Now.ToString("yyyy_MM_dd")+"_Báo_cáo_đóng_gói.xlsx";
            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            if ( dialog.ShowDialog( )==true )
            {
                filePath=dialog.FileName;
                var file = new FileInfo(filePath);
                try
                {
                    using ( package )
                    {
                        //Lưu file lại
                        await package.SaveAsAsync(file);
                    }
                    return ServiceResponse.Successful( );
                }
                catch ( Exception ex )
                {
                    Error error = new Error
                    {
                        NameEvent="ExportExcel",
                        Message=ex.ToString( )
                    };
                    return ServiceResponse.Failed(error);
                }
            }
            else
            {
                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if ( string.IsNullOrEmpty(filePath) )
                {
                    MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                    Error error = new Error
                    {
                        NameEvent="ExportExcel",
                        Message="Đường dẫn không hợp lệ"
                    };
                    return ServiceResponse.Failed(error);
                }
                else
                {
                    Error error = new Error
                    {
                        NameEvent="ExportExcel",
                        Message="Lỗi Lưu file"
                    };
                    return ServiceResponse.Failed(error);
                }
            }
        }




        private ServiceResponse EditExcelSetting (ObservableCollection<BaseInforOrders> shift)
        {
            try
            {
                int i = 0;
                while ( true )
                {
                    string a = Convert.ToString(worksheet.Cells ["J"+Convert.ToString(9+i)].Value);
                    if ( a!="bộ/cái" )
                    { break; }
                    else { i++; }
                }
                int j = 0;
                for ( j=0;j<i;j++ )
                {
                    string _PackingUnit = "";
                    string _Employee = worksheet.Cells ["O"+Convert.ToString(9+j)].Value.ToString( );
                    string _ProductID = Convert.ToString(worksheet.Cells ["B"+Convert.ToString(9+j)].Value.ToString( ));
                    string _ProductName = Convert.ToString(worksheet.Cells ["D"+Convert.ToString(9+j)].Value.ToString( ));
                    double _ProductMass = 0;
                    int _PlannedQuantity = Convert.ToInt32(worksheet.Cells ["K"+Convert.ToString(9+j)].Value.ToString( ));
                    string _Note = Convert.ToString(worksheet.Cells ["R"+Convert.ToString(9+j)].Value);

                    shift.Add(new BaseInforOrders(_PackingUnit,_Employee,_ProductID,_ProductName,_ProductMass,_PlannedQuantity,_Note));
                }
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="EditExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }
        }
        //report excel
        private ServiceResponse ReadExcelFile (string path)
        {
            ExcelPackage.LicenseContext=OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                // mở file excel
                package=new ExcelPackage(new FileInfo(path));
                // lấy ra sheet đầu tiên để thao tác
                worksheet=package.Workbook.Worksheets ["Baocao"];
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="ReadExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }
        }
        private ServiceResponse EditExcelTest (ObservableCollection<Report> report)
        {
            try
            {
                int i = 0;
                foreach ( Report item in report )
                {
                    worksheet.Cells ["L6"].Value=item._Day;
                    worksheet.Cells ["A"+Convert.ToString(9+i)].Value=item._Cardinal;
                    worksheet.Cells ["B"+Convert.ToString(9+i)].Value=item._ProductID;
                    worksheet.Cells ["D"+Convert.ToString(9+i)].Value=item._ProductName;
                    worksheet.Cells ["J"+Convert.ToString(9+i)].Value=item._Unit;
                    worksheet.Cells ["K"+Convert.ToString(9+i)].Value=item._ActualQuantity;
                    worksheet.Cells ["L"+Convert.ToString(9+i)].Value=item._ProductTestResult;
                    worksheet.Cells ["O"+Convert.ToString(9+i)].Value=item._Employee;
                    worksheet.Cells ["Q"+Convert.ToString(9+i)].Value=item._WorkingTime;
                    worksheet.Cells ["R"+Convert.ToString(9+i)].Value=item._Note;
                    i++;
                }
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="EditExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }
        }

        public async Task<ServiceResponse> ExportTest (string path,ObservableCollection<Report> report)
        {
            var step1 = ReadExcelFile(path);
            var step2 = EditExcelTest(report);
            var step3 = await ExportExcelFile( );
            if ( step1.Success!=true )
            {
                return step1;
            }
            else
            {
                if ( step2.Success!=true )
                {
                    return step2;
                }
                else
                {
                    if ( step3.Success!=true )
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful( );
                    }
                }
            }
        }
        public async Task<ServiceResponse> Import (string path,ObservableCollection<BaseInforOrders> setting)
        {
            var step1 = ReadExcelFile(path);
            var step2 = EditExcelSetting(setting);
            if ( step1.Success!=true )
            {
                return step1;
            }
            else
            {
                if ( step2.Success!=true )
                {
                    return step2;
                }
                else
                {
                    return ServiceResponse.Successful( );
                }
            }
        }
        #region seperate order
        // báo cáo có thêm tên công nhân
        public async Task<ServiceResponse> Employee (string path,ObservableCollection<Employee> employee)
        {
            var step1 = Read(path);
            var step2 = Report(employee);
            if ( step1.Success!=true )
            {
                return step1;
            }
            else
            {
                if ( step2.Success!=true )
                {
                    return step2;
                }
                else
                {
                    return ServiceResponse.Successful( );
                }
            }
        }

        private ServiceResponse Report (ObservableCollection<Employee> employee)
        {
            try
            {
                int a;
                for ( a=0;a<27;a++ )
                {
                    string id = worksheet.Cells ["B"+Convert.ToString(12+a)].Value.ToString( );
                    string name = worksheet.Cells ["C"+Convert.ToString(12+a)].Value.ToString( );
                    string [] arrayStr = name.Split(new string [] { " " },StringSplitOptions.RemoveEmptyEntries); // split ra 1 mảng 3 phần tử
                    string firstName = arrayStr [0];
                    string lastName = arrayStr [arrayStr.Length-1];
                    employee.Add(new Employee(id,firstName,lastName));
                }
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="EditExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }
        }



        private ServiceResponse ReadReport (string path)
        {
            ExcelPackage.LicenseContext=OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                // mở file excel
                package=new ExcelPackage(new FileInfo(path));
                // lấy ra sheet đầu tiên để thao tác
                worksheet=package.Workbook.Worksheets ["Baocao"];
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="ReadExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }
        }
        public async Task<ServiceResponse> Export2 (string path,ObservableCollection<ReportDetail> list)
        {
            var step1 = ReadReport(path);
            var step2 = EditReport(list);
            var step3 = await ExportExcelFile( );
            if ( step1.Success!=true )
            {
                return step1;
            }
            else
            {
                if ( step2.Success!=true )
                {
                    return step2;
                }
                else
                {
                    if ( step3.Success!=true )
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful( );
                    }
                }
            }
        }
        private ServiceResponse EditReport (ObservableCollection<ReportDetail> list)
        {
            try
            {
                int j = 0; int a = 0;
                foreach ( ReportDetail item in list )
                {
                    worksheet.Cells ["B"+Convert.ToString(33+j)].Value=item.Id;
                    worksheet.Cells ["D"+Convert.ToString(33+j)].Value=item.NameEmployee;
                    worksheet.Cells ["H"+Convert.ToString(33+j)].Value=item.Department;
                    worksheet.Cells ["J"+Convert.ToString(33+j)].Value=item.WorkingTime;
                    worksheet.Cells ["L"+Convert.ToString(33+j)].Value=item.PauseTime;
                    worksheet.Cells ["N"+Convert.ToString(33+j)].Value=item.Policy;
                    worksheet.Cells ["R"+Convert.ToString(33+j)].Value=item.Note;
                    j++;
                }
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="EditExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }

        }


        //thêm đoạn này chấm công
        private ServiceResponse Read (string path)
        {
            ExcelPackage.LicenseContext=OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                // mở file excel
                package=new ExcelPackage(new FileInfo(path));
                // lấy ra sheet baocaochamcong để thao tác
                worksheet=package.Workbook.Worksheets ["BAOCAOCHAMCONG"];
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="ReadExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }
        }
        private ServiceResponse EditReportPro (ObservableCollection<Productivity> list)
        {
            try
            {
                int j = 0; int a = 0;
                foreach ( Productivity item in list )
                {
                    worksheet.Cells ["A"+Convert.ToString(9+j)].Value=(j+1).ToString( );
                    worksheet.Cells ["B"+Convert.ToString(33+j)].Value=DateTime.Now.ToString("yyyy/MM/dd");
                    worksheet.Cells ["C"+Convert.ToString(9+j)].Value=item.EmployeeId;
                    worksheet.Cells ["E"+Convert.ToString(9+j)].Value=item.EmployeeName;
                    worksheet.Cells ["H"+Convert.ToString(9+j)].Value=item.Department;
                    worksheet.Cells ["I"+Convert.ToString(9+j)].Value=item.WorkingTime;
                    worksheet.Cells ["J"+Convert.ToString(9+j)].Value=item.PauseTime;
                    worksheet.Cells ["K"+Convert.ToString(9+j)].Value=item.Remind;
                    worksheet.Cells ["L"+Convert.ToString(9+j)].Value=item.Note;
                    j++;
                }
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="EditExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }

        }

        public async Task<ServiceResponse> ExportProductivity (string path,ObservableCollection<Productivity> list)
        {
            var step1 = Read(path);
            var step2 = EditReportPro(list);
            var step3 = await ExportExcelFile( );
            if ( step1.Success!=true )
            {
                return step1;
            }
            else
            {
                if ( step2.Success!=true )
                {
                    return step2;
                }
                else
                {
                    if ( step3.Success!=true )
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful( );
                    }
                }
            }
        }




        // xuất báo cáo phân chia công việc
        public async Task<ServiceResponse> Export6Sheet (ObservableCollection<InforOders> listMachine1,ObservableCollection<InforOders> listMachine2,
            ObservableCollection<InforOders> listMachine3,ObservableCollection<InforOders> listMachine4,ObservableCollection<InforOders> listMachine5,
            ObservableCollection<InforOders> listMachine6)
        {
            ExcelPackage.LicenseContext=OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                package=new ExcelPackage(new FileInfo("Export6Machine.xlsx"));
                string workingDay = DateTime.Now.ToString("yyyy/MM/dd");
                int a = listMachine1.Count( );
                int b = listMachine2.Count( );
                int c = listMachine3.Count( );
                int d = listMachine4.Count( );
                int e = listMachine5.Count( );
                int f = listMachine6.Count( );

                if ( a!=null )
                {
                    int i = 0;
                    foreach ( var item in listMachine1 )
                    {
                        package.Workbook.Worksheets ["Sheet1"].Cells ["G6"].Value=item._Employee;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J6"].Value=workingDay;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["A"+(9+i).ToString( )].Value=i+1;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["B"+(9+i).ToString( )].Value=item._ProductID;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["D"+(9+i).ToString( )].Value=item._ProductName;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["H"+(9+i).ToString( )].Value="Bộ";
                        package.Workbook.Worksheets ["Sheet1"].Cells ["I"+(9+i).ToString( )].Value=item._PlannedQuantity;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J"+(9+i).ToString( )].Value=item._Note;
                        i++;
                    }
                }
                if ( b!=null )
                {
                    int i = 0;
                    foreach ( var item in listMachine2 )
                    {
                        package.Workbook.Worksheets ["Sheet1"].Cells ["G21"].Value=item._Employee;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J21"].Value=workingDay;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["A"+(24+i).ToString( )].Value=i+1;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["B"+(24+i).ToString( )].Value=item._ProductID;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["D"+(24+i).ToString( )].Value=item._ProductName;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["H"+(24+i).ToString( )].Value="Bộ";
                        package.Workbook.Worksheets ["Sheet1"].Cells ["I"+(24+i).ToString( )].Value=item._PlannedQuantity;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J"+(24+i).ToString( )].Value=item._Note;
                        i++;
                    }
                }
                if ( c!=null )
                {
                    int i = 0;
                    foreach ( var item in listMachine3 )
                    {
                        package.Workbook.Worksheets ["Sheet1"].Cells ["G36"].Value=item._Employee;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J36"].Value=workingDay;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["A"+(39+i).ToString( )].Value=i+1;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["B"+(39+i).ToString( )].Value=item._ProductID;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["D"+(39+i).ToString( )].Value=item._ProductName;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["H"+(39+i).ToString( )].Value="Bộ";
                        package.Workbook.Worksheets ["Sheet1"].Cells ["I"+(39+i).ToString( )].Value=item._PlannedQuantity;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J"+(39+i).ToString( )].Value=item._Note;
                        i++;
                    }
                }
                if ( d!=null )
                {
                    int i = 0;
                    foreach ( var item in listMachine4 )
                    {
                        package.Workbook.Worksheets ["Sheet1"].Cells ["G51"].Value=item._Employee;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J51"].Value=workingDay;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["A"+(54+i).ToString( )].Value=i+1;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["B"+(54+i).ToString( )].Value=item._ProductID;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["D"+(54+i).ToString( )].Value=item._ProductName;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["H"+(54+i).ToString( )].Value="Bộ";
                        package.Workbook.Worksheets ["Sheet1"].Cells ["I"+(54+i).ToString( )].Value=item._PlannedQuantity;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J"+(54+i).ToString( )].Value=item._Note;
                        i++;
                    }
                }
                if ( e!=null )
                {
                    int i = 0;
                    foreach ( var item in listMachine5 )
                    {
                        package.Workbook.Worksheets ["Sheet1"].Cells ["G67"].Value=item._Employee;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J67"].Value=workingDay;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["A"+(70+i).ToString( )].Value=i+1;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["B"+(70+i).ToString( )].Value=item._ProductID;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["D"+(70+i).ToString( )].Value=item._ProductName;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["H"+(70+i).ToString( )].Value="Bộ";
                        package.Workbook.Worksheets ["Sheet1"].Cells ["I"+(70+i).ToString( )].Value=item._PlannedQuantity;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J"+(70+i).ToString( )].Value=item._Note;
                        i++;
                    }
                }
                if ( f!=null )
                {
                    int i = 0;
                    foreach ( var item in listMachine6 )
                    {
                        package.Workbook.Worksheets ["Sheet1"].Cells ["G6"].Value=item._Employee;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["L6"].Value=workingDay;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["A"+(9+i).ToString( )].Value=i+1;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["B"+(9+i).ToString( )].Value=item._ProductID;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["D"+(9+i).ToString( )].Value=item._ProductName;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["J"+(9+i).ToString( )].Value="Bộ";
                        package.Workbook.Worksheets ["Sheet1"].Cells ["K"+(9+i).ToString( )].Value=item._PlannedQuantity;
                        package.Workbook.Worksheets ["Sheet1"].Cells ["M"+(9+i).ToString( )].Value=item._Note;
                        i++;
                    }
                }
                ExportExcelFile( );
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="ReadExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }
        }
        //
        private ServiceResponse ReadExcelFile ( )
        {
            ExcelPackage.LicenseContext=OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                var filePath = string.Empty;

                OpenFileDialog choofdlog = new OpenFileDialog( );
                choofdlog.DefaultExt="xlsx";
                choofdlog.Filter="Excel Files (*.xlsx)|*.xlsx";
                choofdlog.FilterIndex=1;
                choofdlog.Multiselect=false;

                if ( choofdlog.ShowDialog( )==true )
                {
                    filePath=choofdlog.FileName;
                }
                else
                    filePath=string.Empty;
                // mở file excel
                package=new ExcelPackage(new FileInfo(filePath));

                // lấy ra sheet đầu tiên để thao tác
                worksheet=package.Workbook.Worksheets ["Baocao"];
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="ReadExcel Error",
                    Message="Đường dẫn báo cáo không hợp lệ"
                };
                return ServiceResponse.Failed(error);
            }
        }

        private ServiceResourceResponse<ObservableCollection<AssemblyTask>> ImportSeperateExcelExcel ( )
        {
            try
            {
                ObservableCollection<AssemblyTask> assemblyTasks = new ObservableCollection<AssemblyTask>( );
                int i = 10;
                bool stopFlag = true;
                while ( stopFlag )
                {
                    var Id = Convert.ToString(worksheet.Cells ["B"+Convert.ToString(i)].Value);
                    if ( Id!="\\" )
                    {
                        var Name = Convert.ToString(worksheet.Cells ["E"+Convert.ToString(i)].Value);
                        var Priority = 0;
                        var ProductQuantity = Convert.ToInt32(worksheet.Cells ["R"+Convert.ToString(i)].Value);
                        var Workload = Convert.ToDecimal(worksheet.Cells ["T"+Convert.ToString(i)].Value);
                        AssemblyTask assemblyTask = new AssemblyTask(Id,Name,Workload,Priority,ProductQuantity);
                        i++;
                        assemblyTasks.Add(assemblyTask);
                    }
                    else
                    { stopFlag=false; }
                }
                return new ServiceResourceResponse<ObservableCollection<AssemblyTask>>(assemblyTasks);
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="EditExcel",
                    Message=ex.ToString( )
                };
                return new ServiceResourceResponse<ObservableCollection<AssemblyTask>>(error);
            }

        }

        public async Task<ServiceResourceResponse<ObservableCollection<AssemblyTask>>> ImportSeperateOrder ( )
        {
            var step1 = ReadExcelFile( );
            if ( step1.Success )
            {
                var step2 = ImportSeperateExcelExcel( );
                if ( step2.Success )
                {
                    return new ServiceResourceResponse<ObservableCollection<AssemblyTask>>(step2.Resource);
                }

                else
                {
                    return new ServiceResourceResponse<ObservableCollection<AssemblyTask>>(step2.Error);
                }
            }
            else
            {
                Error error = new Error
                {
                    NameEvent="ReadExcel",
                    Message="Error Import Excel"
                };
                return new ServiceResourceResponse<ObservableCollection<AssemblyTask>>(error);
            }
        }
        //
        private ServiceResponse EditReportOrder (List<Assignment> list)
        {
            try
            {
                int j = 0;
                foreach ( var item in list )
                {
                    foreach ( var item1 in item.Tasks )
                    {
                        worksheet.Cells ["A"+Convert.ToString(9+j)].Value=j+1;
                        worksheet.Cells ["B"+Convert.ToString(9+j)].Value=item1.Id;
                        worksheet.Cells ["D"+Convert.ToString(9+j)].Value=item1.Name;
                        worksheet.Cells ["J"+Convert.ToString(9+j)].Value="bộ/cái";
                        worksheet.Cells ["K"+Convert.ToString(9+j)].Value=item1.ProductQuantity;
                        worksheet.Cells ["O"+Convert.ToString(9+j)].Value=item.Employee.Id;
                        j++;
                    }
                }
                return ServiceResponse.Successful( );
            }
            catch ( Exception ex )
            {
                Error error = new Error
                {
                    NameEvent="EditExcel",
                    Message=ex.ToString( )
                };
                return ServiceResponse.Failed(error);
            }

        }
        public async Task<ServiceResponse> ExportReportOrder (List<Assignment> list)
        {
            var step1 = ReadExcelFile("ExportExcel.xlsx");
            var step2 = EditReportOrder(list);
            var step3 = await ExportExcelFile( );
            if ( step1.Success!=true )
            {
                return step1;
            }
            else
            {
                if ( step2.Success!=true )
                {
                    return step2;
                }
                else
                {
                    if ( step3.Success!=true )
                    {
                        return step3;
                    }
                    else
                    {
                        return ServiceResponse.Successful( );
                    }
                }
            }
        }
        #endregion
    }

}
