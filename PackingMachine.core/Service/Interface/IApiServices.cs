using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.Api;
using PackingMachine.core.Domain.Model.Api.Shift;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PackingMachine.core.Service.Interface
{
    public interface IApiServices
    {
        Task<ObservableCollection<AllItems>> GetAllItems (string auth);
        Task<Item> GetItemById (string auth,string productId);
        Task<ObservableCollection<Employee>> GetEmployee (string auth);
        Task<ObservableCollection<PackingUnit>> GetAllPackingUnits (string auth);
        Task PostShiftReport (string auth,ObservableCollection<ShiftReport> ListShiftReport);
        Task<ObservableCollection<ShiftReport>> GetShift (string auth,string startTime,string endTime);
        //Task<ObservableCollection<Product>> GetProductTotal(string auth);

    }
}
