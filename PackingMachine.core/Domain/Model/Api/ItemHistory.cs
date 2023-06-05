using PackingMachine.core.Domain.Model.Api.Shift;
using System.Collections.Generic;

namespace PackingMachine.core.Domain.Model.Api
{
    public class ItemHistory
    {
        public List<ShiftReport> Items { get; set; }

        public double totalItems { get; set; }
    }
}
