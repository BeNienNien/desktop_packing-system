using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.ModuleSeperateOrder.Models;
public class Assignment
{
    public Employee Employee { get; set; }
    public List<AssignmentTask> Tasks { get; set; }

    public Assignment(Employee employee)
    {
        Employee = employee;
        Tasks = new List<AssignmentTask>();
    }
}
