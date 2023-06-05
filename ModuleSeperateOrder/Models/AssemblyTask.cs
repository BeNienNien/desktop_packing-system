using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.ModuleSeperateOrder.Models;
public class AssemblyTask
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Workload { get; set; }
    public int Priority { get; set; }
    public int ProductQuantity { get; set; }

    public AssemblyTask(string id,string name, decimal workload, int priority, int productQuantity = 0)
    {
        Id = id;
        Name = name;
        Workload = workload;
        Priority = priority;
        ProductQuantity = productQuantity;
    }
}
