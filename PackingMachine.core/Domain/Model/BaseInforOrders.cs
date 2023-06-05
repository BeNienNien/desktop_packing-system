using PackingMachine.core.Domain.Model.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Doman.Model
{
    public class BaseInforOrders 
    {
        public string _PackingUnit { get; set; }
        public string _Employee { get; set; }
        public string _ProductID { get; set; }
        public string _ProductName { get; set; }
        public  double _ProductMass { get; set; }
        public int _PlannedQuantity { get; set; }
        public string _Note { get; set; }

        public BaseInforOrders(string PackingUnit, string employee, string productID, string productname, double productmass, int plannedQuantity, string note)
        {
            _PackingUnit = PackingUnit;
            _Employee = employee;
            _ProductID = productID;
            _ProductName = productname;
            _ProductMass = productmass;
            _PlannedQuantity = plannedQuantity;
            _Note = note;
        }
    }
}
