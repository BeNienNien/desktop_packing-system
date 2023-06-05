using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model
{
    public class PackingUnit
    {
        public string Id { get; set; }

        public PackingUnit(string id)
        {
            Id = id;
        }
    }
}
