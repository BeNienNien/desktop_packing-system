using PackingMachine.core.Domain.Communication;
using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.ImportExcel;
using PackingMachine.core.Doman.Model;
using PackingMachine.Core.Domain.Communication;
using PackingMachine.ModuleSeperateOrder.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Employee = PackingMachine.core.Domain.Model.Api.Employee;

namespace PackingMachine.core.Service.Interface
{
    public interface IExcelExportService
    {
        Task<ServiceResponse> ExportTest (string path,ObservableCollection<Report> report);
        Task<ServiceResponse> Import (string path,ObservableCollection<BaseInforOrders> setting);
        Task<ServiceResponse> Employee (string path,ObservableCollection<Employee> employee);
        Task<ServiceResponse> Export2 (string path,ObservableCollection<ReportDetail> list);
        Task<ServiceResponse> Export6Sheet (ObservableCollection<InforOders> listMachine1,ObservableCollection<InforOders> listMachine2,
            ObservableCollection<InforOders> listMachine3,ObservableCollection<InforOders> listMachine4,ObservableCollection<InforOders> listMachine5,
            ObservableCollection<InforOders> listMachine6);
        Task<ServiceResponse> ExportProductivity (string path,ObservableCollection<Productivity> list);
        Task<ServiceResourceResponse<ObservableCollection<AssemblyTask>>> ImportSeperateOrder ( );
        Task<ServiceResponse> ExportReportOrder (List<Assignment> list);
    }
}
