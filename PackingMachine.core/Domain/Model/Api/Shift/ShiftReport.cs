using System;
using System.Collections.Generic;

namespace PackingMachine.core.Domain.Model.Api.Shift
{
    public class ShiftReport
    {
        public DateTime date { get; set; }
        public Employee employee { get; set; }
        public String packingUnitId { get; set; }
        public List<ItemShift> items { get; set; }
        public double workingTime { get; set; }
        public ShiftReport (DateTime _date,Employee _employee,string _packingUnitId,List<ItemShift> _items,double _workingTime)
        {
            date=_date;
            employee=_employee;
            packingUnitId=_packingUnitId;
            items=_items;
            workingTime=_workingTime;
        }
    }
}
