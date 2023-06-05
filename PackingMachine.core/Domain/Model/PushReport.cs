using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model
{
    public class PushReport
    {
        public class Employee
        {
            public string id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
        }

        public class Item
        {
            public string itemId { get; set; }
            public int plannedQuantity { get; set; }
            public int actualQuantity { get; set; }
            public string note { get; set; }
            public int standardWeight { get; set; }
        }

        public class Root
        {
            public DateTime date { get; set; }
            public Employee employee { get; set; }
            public string packingUnitId { get; set; }
            public List<Item> items { get; set; }
            public int workingTime { get; set; }
        }
    }
}
