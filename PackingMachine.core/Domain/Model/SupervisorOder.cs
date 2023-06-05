using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model
{
    public class SupervisorOder
    {

        public string _ProductID { get; set; }

        public int _PlannedQuantity { get; set; }
        public  int _ActualQuantity { get; set; }
        public int _ErrorProduct { get; set; }
        public string _Note { get; set; }

        public SupervisorOder(string ProductID, int PlannedQuantity, int ActualQuantity, int ErrorProduct, string Note)
        {
            _ProductID = ProductID;
            _PlannedQuantity = PlannedQuantity;
            _ActualQuantity = ActualQuantity;
            _ErrorProduct = ErrorProduct;
            _Note = Note;
        }
    }
}
