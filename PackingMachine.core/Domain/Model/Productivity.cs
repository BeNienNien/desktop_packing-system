using PackingMachine.core.Domain.Model.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Doman.Model
{
    public class Productivity
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public double WorkingTime { get; set; }
        public double PauseTime { get; set; }
        public string Remind { get; set; }
        public string Note { get; set; }

        public Productivity(string employeeId, string employeeName, string department, double workingTime, double pauseTime, string remind, string note)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            Department = department;
            WorkingTime = workingTime;
            PauseTime = pauseTime;
            Remind = remind;
            Note = note;
        }
    }
}
