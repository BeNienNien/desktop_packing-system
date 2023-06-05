
using Newtonsoft.Json;
using PackingMachine.core.Domain.Model;
using PackingMachine.core.Domain.Model.Api;
using PackingMachine.core.Domain.Model.Api.Shift;
using PackingMachine.core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.Core.Services
{
    public class ApiServices: IApiServices
    {
        private HttpClient httpClient;
        public ObservableCollection<AllItems> allItems = new ObservableCollection<AllItems>( );
        public ObservableCollection<Employee> employees = new ObservableCollection<Employee>( );
        public ObservableCollection<PackingUnit> packingUnit = new ObservableCollection<PackingUnit>( );
        public ObservableCollection<ShiftReport> ListShift = new ObservableCollection<ShiftReport>( );


        public Item ItemById = new Item( );
        public ItemHistory itemHistory = new ItemHistory( );
        private HttpRequestMessage httpRequest;
        // https://mywebapiapp20220920140917.azurewebsites.net/ link api Toàn
        // cloud
        //private string Address = "https://localhost:7171/";
        //CHA
        //private string Address = "http://10.84.70.81:8083/";
        private string Address = "https://packingsystemmicroservice.azurewebsites.net/";
        public Employee employee;
        public string packingUnitId;
        public double workingTime;
        public DateTime date;



        public List<ItemShift> items { get; set; }
        public ApiServices ( )
        {
            httpClient=new HttpClient( );
        }

        public async Task<ObservableCollection<AllItems>> GetAllItems (string auth)
        {
            try
            {
                // PDA
                // Cloud
                var httpRequest = new HttpRequestMessage( );
                // server CHA
                //string Url = "";
                // server PDA
                //string Url = "https://mywebapiapp20220920140917.azurewebsites.net/api/items/minimal?topLevel=true";
                //Cloud
                string Url = Address+"api/items/minimal";
                httpRequest.Method=System.Net.Http.HttpMethod.Get;
                httpRequest.RequestUri=new Uri(Url);
                var httpResponse = await httpClient.SendAsync(httpRequest);
                httpResponse.EnsureSuccessStatusCode( );
                var ob = await httpResponse.Content.ReadAsStringAsync( );
                allItems=JsonConvert.DeserializeObject<ObservableCollection<AllItems>>(ob);
            }
            catch
            {
                // MessageBox.Show("Looix");
            }
            return allItems;
        }
        public async Task<ObservableCollection<Employee>> GetEmployee (string auth)
        {
            try
            {
                // PDA
                // Cloud
                var httpRequest = new HttpRequestMessage( );
                // server CHA
                //string Url = "http://10.84.70.80:8083/api/items/minimal?topLevel=true";
                // server PDA
                //string Url = "http://192.168.1.80:8083/api/items/minimal?topLevel=true";
                //Cloud
                string Url = Address+"api/Employees/";
                //server
                //string Url = Address + "api/employees/";
                httpRequest.Method=System.Net.Http.HttpMethod.Get;
                httpRequest.RequestUri=new Uri(Url);
                var httpResponse = await httpClient.SendAsync(httpRequest);
                httpResponse.EnsureSuccessStatusCode( );
                var ob = await httpResponse.Content.ReadAsStringAsync( );
                employees=JsonConvert.DeserializeObject<ObservableCollection<Employee>>(ob);
            }
            catch
            {
                // MessageBox.Show("Looix");
            }
            return employees;
        }
        public async Task<ObservableCollection<PackingUnit>> GetAllPackingUnits (string auth)
        {
            try
            {
                // PDA
                // Cloud
                var httpRequest = new HttpRequestMessage( );
                // server CHA
                //string Url = "http://10.84.70.80:8083/api/items/minimal?topLevel=true";
                // server PDA
                //string Url = "http://192.168.1.80:8083/api/items/minimal?topLevel=true";
                //Cloud
                string Url = Address+"api/packingunits/";
                //server
                //string Url = Address + "api/employees/";
                httpRequest.Method=System.Net.Http.HttpMethod.Get;
                httpRequest.RequestUri=new Uri(Url);
                var httpResponse = await httpClient.SendAsync(httpRequest);
                httpResponse.EnsureSuccessStatusCode( );
                var ob = await httpResponse.Content.ReadAsStringAsync( );
                packingUnit=JsonConvert.DeserializeObject<ObservableCollection<PackingUnit>>(ob);
            }
            catch
            {
                // MessageBox.Show("Lỗi");
            }
            return packingUnit;
        }
        public async Task<Item> GetItemById (string auth,string productId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage( );
                // server CHA
                //string Url = "http://10.84.70.80:8083/api/items/" + productId;
                // Server PDA
                //string Url = "http://192.168.1.80:8083/api/items/" + productId;
                // Cloud
                string Url = Address+"api/items/"+productId;
                httpRequest.Method=System.Net.Http.HttpMethod.Get;
                httpRequest.RequestUri=new Uri(Url);
                var httpResponse = await httpClient.SendAsync(httpRequest);
                httpResponse.EnsureSuccessStatusCode( );
                var ob = await httpResponse.Content.ReadAsStringAsync( );
                Item itemById = JsonConvert.DeserializeObject<Item>(ob);
                ItemById=itemById;
            }
            catch
            {
                //  MessageBox.Show("Take item error");
            }
            return ItemById;
        }
        public async Task PostShiftReport (string auth,ObservableCollection<ShiftReport> ListShiftReport)
        {
            try
            {
                string data = JsonConvert.SerializeObject(ListShiftReport);
                var content = new StringContent(data,Encoding.UTF8,"application/json");
                using ( httpClient=new HttpClient( ) )
                {
                    using ( httpRequest=new HttpRequestMessage( ) )
                    {
                        string Url = Address+"api/Shifts";
                        httpRequest.Method=System.Net.Http.HttpMethod.Post;
                        httpRequest.RequestUri=new Uri(Url);
                        httpRequest.Content=content;
                        HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
                        if ( httpResponse.StatusCode==System.Net.HttpStatusCode.OK )

                        {
                            // CustomMessageBox.Show("Gửi dữ liệu thành công", "Thông báo", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Asterisk);
                        }
                        else
                        {
                            //     CustomMessageBox.Show("Gửi dữ liệu không thành công.", "Lỗi", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Error);
                        }

                    }

                }

            }
            catch
            {
                //   CustomMessageBox.Show("Lỗi trong quá trình gửi dữ liệu lên server!", "Cảnh bảo", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Warning);
            }
        }
        public async Task<ObservableCollection<ShiftReport>> GetShift (string auth,string startTime,string endTime)
        {
            try
            {
                var httpRequest = new HttpRequestMessage( );
                // Cloud
                string Url = Address+"api/Shifts?"+"StartTime="+startTime+"T00:00:00.000Z&EndTime="+endTime+"T23:59:59.0000Z";
                httpRequest.Method=System.Net.Http.HttpMethod.Get;
                httpRequest.RequestUri=new Uri(Url);
                var httpResponse = await httpClient.SendAsync(httpRequest);
                httpResponse.EnsureSuccessStatusCode( );

                var ob = await httpResponse.Content.ReadAsStringAsync( );
                itemHistory=JsonConvert.DeserializeObject<ItemHistory>(ob);
                ListShift.Clear( );
                foreach ( var preShiftReport in itemHistory.Items )
                {
                    ShiftReport shiftReport = new ShiftReport(date=preShiftReport.date,employee=preShiftReport.employee,packingUnitId=preShiftReport.packingUnitId,items=preShiftReport.items,workingTime=preShiftReport.workingTime);
                    ListShift.Add(shiftReport);
                }
            }
            catch
            {
                //  MessageBox.Show("Take item error");
            }
            return ListShift;
        }
    }
}
