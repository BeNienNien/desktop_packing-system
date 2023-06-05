using PackingMachine.core.Domain.Model.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model.ImportExcel
{
    public class ReportDetail
    {
        
        public Employee Employee { get; set; }
        public Item Product { get; set; }
        public double WorkingTime { get; set; }
        public double PauseTime { get; set; }
        public string Department { get; set; }
        public string Note { get; set; }
        public string Policy { get; set; }
        public string NameEmployee { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Unit { get; set; }
        public int Quatity { get; set; }
    }
}
