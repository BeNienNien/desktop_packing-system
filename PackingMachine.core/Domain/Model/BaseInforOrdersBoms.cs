using PackingMachine.core.Domain.Model.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Doman.Model
{
    public class BaseInforOrdersBoms 
    {
        public string _PackingUnit { get; set; }
        public string _Employee { get; set; }
        public string _ProductID { get; set; }
        public string _ProductName { get; set; }
        public List<string> _Boms { get; set; }
        public  double _ProductMass { get; set; }
        public int _PlannedQuantity { get; set; }
        public string _Note { get; set; }

        public BaseInforOrdersBoms(string PackingUnit, string employee, string productID, string productname, List<string> boms, double productmass, int plannedQuantity, string note)
        {
            _PackingUnit = PackingUnit;
            _Employee = employee;
            _ProductID = productID;
            _ProductName = productname;
            _Boms = boms;
            _ProductMass = productmass;
            _PlannedQuantity = plannedQuantity;
            _Note = note;
        }
    }
}
