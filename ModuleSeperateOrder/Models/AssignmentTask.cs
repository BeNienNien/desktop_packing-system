using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.ModuleSeperateOrder.Models;
public class AssignmentTask
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Workload { get; set; }
    public int ProductQuantity { get; set; }

    public AssignmentTask(string id, string name, decimal workload, int productQuantity = 0)
    {
        Id = id;
        Name = name;
        Workload = workload;
        ProductQuantity = productQuantity;
    }
}
