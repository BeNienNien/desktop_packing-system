using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model
{
    public class Report
    {
        public int _Cardinal { get; set; }
        public string _Day { get; set; }
        public string _ProductID { get; set; }
        public string _ProductName { get; set; }
        public string _Unit { get; set; }
        public int _ActualQuantity { get; set; }
        public int _PlannedQuantity { get; set; }
        public string _ProductTestResult { get; set; }
        public string _Employee { get; set; }
        public double _WorkingTime { get; set; }
        public string _Note { get; set; }

        public Report(int Cardinal, string Day, string ProductID, string ProductName, string Unit, int ActualQuantity, int PlannedQuantity, string ProductTestResult, string Employee, double WorkingTime, string Note)
        {
            _Cardinal = Cardinal;
            _Day = Day;
            _ProductID = ProductID;
            _ProductName = ProductName;
            _Unit = Unit;
            _ActualQuantity = ActualQuantity;
            _PlannedQuantity = PlannedQuantity;
            _ProductTestResult = ProductTestResult;
            _Employee = Employee;
            _WorkingTime = WorkingTime;
             _Note = Note;
        }
    }
}
