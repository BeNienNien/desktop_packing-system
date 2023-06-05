using System;
using System.Collections.Generic;

namespace PackingMachine.core.Domain.Model
{
    public class ChangeInforOder
    {
        public string _employee { get; set; }
        public string _productID { get; set; }
        public string _productName { get; set; }
        public List<string> _boms { get; set; }
        public double _productMass { get; set; }
        public int _plannedQuantity { get; set; }
        public int _actualQuantity { get; set; }
        public int _errorProduct { get; set; }
        public string _note { get; set; }
        public double _percentValue { get; set; }
        public double _totalQuatity { get; set; }
        public string _productTestResult { get; set; }
        public double _workingTime { get; set; }
        public double _executionTime { get; set; }
        public DateTime _timeStamp { get; set; }
        public ChangeInforOder (string Employee,string ProductID,string ProductName,List<string> Boms,double ProductMass,int PlannedQuantity,int ActualQuantity,int ErrorProduct,string Note,double percentValue,double totalQuatity,string productTestResult,double workingTime,DateTime timeStamp,double executionTime)
        {
            _employee=Employee;
            _productID=ProductID;
            _productName=ProductName;
            _boms=Boms;
            _productMass=ProductMass;
            _plannedQuantity=PlannedQuantity;
            _actualQuantity=ActualQuantity;
            _errorProduct=ErrorProduct;
            _note=Note;
            _percentValue=percentValue;
            _totalQuatity=totalQuatity;
            _productTestResult=productTestResult;
            _workingTime=workingTime;
            _timeStamp=timeStamp;
            _executionTime=executionTime;
        }
    }
}
