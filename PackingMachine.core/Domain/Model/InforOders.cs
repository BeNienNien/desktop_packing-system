using System;
using System.Collections.Generic;

namespace PackingMachine.core.Doman.Model
{
    public class InforOders
    {
        public string _PackingUnit { get; set; }
        public string _Employee { get; set; }
        public string _ProductID { get; set; }
        public string _ProductName { get; set; }
        public List<string> _Boms { get; set; }
        public double _ProductMass { get; set; }
        public int _PlannedQuantity { get; set; }
        public int _ActualQuantity { get; set; }
        public int _ErrorProduct { get; set; }
        public string _Note { get; set; }
        public double _PercentValue { get; set; }
        public double _TotalQuality { get; set; }
        public string _ProductTestResult { get; set; }
        public double _WorkingTime { get; set; }
        public double _ExecutionTime { get; set; }
        public DateTime _TimeStamp { get; set; }

        public InforOders (string PackingUnit,string employee,string productID,string productName,List<string> boms,double productMass,int plannedQuantity,int actualQuantity,int errorProduct,string note,double percentValue,double totalQuality,string productTestResult,double workingTime,DateTime timeStamp,double executionTime)
        {

            _PercentValue=percentValue;
            _PackingUnit=PackingUnit;
            _Employee=employee;
            _ProductID=productID;
            _ProductName=productName;
            _Boms=boms;
            _ProductMass=productMass;
            _PlannedQuantity=plannedQuantity;
            _ActualQuantity=actualQuantity;
            _ErrorProduct=errorProduct;
            _Note=note;
            _TotalQuality=totalQuality;
            _ProductTestResult=productTestResult;
            _WorkingTime=workingTime;
            _TimeStamp=timeStamp;
            _ExecutionTime=executionTime;
        }
    }
}
