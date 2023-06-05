using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model
{
    public class SupervisorOderMachine2
    {

        public string _ProductIDMachine2 { get; set; }

        public int _PlannedQuantityMachine2 { get; set; }
        public  int _ActualQuantityMachine2 { get; set; }
        public int _ErrorProductMachine2 { get; set; }
        public string _NoteMachine2 { get; set; }

        public SupervisorOderMachine2(string ProductID, int PlannedQuantity, int ActualQuantity, int FixNumber, string Note)
        {
            _ProductIDMachine2 = ProductID;
            _PlannedQuantityMachine2 = PlannedQuantity;
            _ActualQuantityMachine2 = ActualQuantity;
            _ErrorProductMachine2 = FixNumber;
            _NoteMachine2 = Note;
        }
    }
}
