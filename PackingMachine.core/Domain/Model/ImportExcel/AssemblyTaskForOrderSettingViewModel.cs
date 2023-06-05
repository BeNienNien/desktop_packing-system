using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model.ImportExcel
{
    public class AssemblyTaskForOrderSettingViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Workload { get; set; }
        public string Priority { get; set; }
        public string ProductQuantity { get; set; }
    }
}
