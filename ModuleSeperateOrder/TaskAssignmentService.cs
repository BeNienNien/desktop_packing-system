using PackingMachine.ModuleSeperateOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.ModuleSeperateOrder;
public static class TaskAssignmentService
{
    public static List<Assignment> AssignTasks(List<Employee> assemblyEmployees, List<AssemblyTask> assemblyTasks)
    {
        // Clone input list
        var employees = new List<Employee>();
        var tasks = new List<AssemblyTask>();

        foreach (var assemblyEmployee in assemblyEmployees)
        {
            employees.Add(new Employee(assemblyEmployee.Id, assemblyEmployee.Performance));
        }
        foreach (var assemblyTask in assemblyTasks)
        {
            tasks.Add(new AssemblyTask(assemblyTask.Id,assemblyTask.Name, assemblyTask.Workload, assemblyTask.Priority));
        }

        if (employees.Sum(e => e.Performance) % 1 != 0)
        {
            throw new Exception("Sum of employees'performance is not an integer.");
        }
        if (tasks.Sum(t => t.Workload) % 1 != 0)
        {
            throw new Exception("Sum of tasks'workload is not an integer.");
        }
        if (employees.Sum(e => e.Performance) != tasks.Sum(t => t.Workload))
        {
            throw new Exception("Sum of tasks'workload is not equal to sum of employees'performance.");
        }

        var wholeTasks = tasks.Where(t => t.Workload % 1 == 0).ToList();

        var oddTasks = tasks.Where(e => e.Workload % 1 != 0).ToList();
        foreach (var task in oddTasks)
        {
            var wholeWorkload = Math.Floor(task.Workload);

            if (wholeWorkload > 0)
            {
                var oddWorkload = task.Workload % 1;
                wholeTasks.Add(new AssemblyTask(task.Id,task.Name, wholeWorkload, task.Priority));
                task.Workload = oddWorkload;
            }
        }

        var fullEmployees = employees.Where(e => e.Performance % 1 == 0).ToList();
        var partialEmployees = employees.Where(e => e.Performance % 1 != 0).ToList();

        var assignments = new List<Assignment>();
        var assignedEmployees = new List<Employee>();

        foreach (var employee in fullEmployees)
        {
            if (!oddTasks.Any())
            {
                break;
            }

            var assignment = new Assignment(employee);
            var removedTasks = new List<AssemblyTask>();

            foreach (var task in oddTasks)
            {
                var remainingLoad = assignment.Employee.Performance - assignment.Tasks.Sum(t => t.Workload);

                if (remainingLoad <= 0)
                {
                    break;
                }

                if (remainingLoad < task.Workload)
                {
                    task.Workload = task.Workload - remainingLoad;
                    assignment.Tasks.Add(new AssignmentTask(task.Id, task.Name, remainingLoad));
                }
                else
                {
                    assignment.Tasks.Add(new AssignmentTask(task.Id, task.Name, task.Workload));
                    removedTasks.Add(task);
                    task.Workload = 0;
                }
            }
            removedTasks.ForEach(t => oddTasks.Remove(t));
            assignments.Add(assignment);
            assignedEmployees.Add(employee);
        }
        assignedEmployees.ForEach(t => fullEmployees.Remove(t));

        wholeTasks = wholeTasks.OrderByDescending(t => t.Priority).ToList();
        foreach (var employee in partialEmployees)
        {
            var removedTasks = new List<AssemblyTask>();
            var assignment = new Assignment(employee);

            foreach (var task in wholeTasks)
            {
                var remainingLoad = assignment.Employee.Performance - assignment.Tasks.Sum(t => t.Workload);

                if (remainingLoad <= 0)
                {
                    break;
                }

                if (remainingLoad < task.Workload)
                {
                    task.Workload = task.Workload - remainingLoad;
                    assignment.Tasks.Add(new AssignmentTask(task.Id, task.Name, remainingLoad));
                }
                else
                {
                    assignment.Tasks.Add(new AssignmentTask(task.Id, task.Name, task.Workload));
                    removedTasks.Add(task);
                    task.Workload = 0;
                }
            }

            removedTasks.ForEach(t => wholeTasks.Remove(t));
            assignments.Add(assignment);
        }

        foreach (var employee in fullEmployees)
        {
            var assignment = new Assignment(employee);
            var task = wholeTasks.First();
            assignment.Tasks.Add(new AssignmentTask(task.Id, task.Name, employee.Performance));
            task.Workload -= employee.Performance;
            if (task.Workload == 0)
            {
                wholeTasks.Remove(task);
            }
            assignments.Add(assignment);
        }

        foreach (var assignment in assignments)
        {
            foreach (var task in assignment.Tasks)
            {
                var originalTask = assemblyTasks.First(t => t.Id == task.Id);
                task.ProductQuantity = (int) (originalTask.ProductQuantity * task.Workload / originalTask.Workload);
            }
        }

        return assignments;
    }
}
