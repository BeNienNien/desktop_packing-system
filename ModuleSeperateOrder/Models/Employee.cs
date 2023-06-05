using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.ModuleSeperateOrder.Models;
public class Employee
{
    public string Id { get; set; }
    public decimal Performance { get; set; }

    public Employee(string id, decimal performance)
    {
        Id = id;
        Performance = performance;
    }
}
