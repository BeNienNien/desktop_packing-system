using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model.Api
{
    public class AllItems
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public AllItems(string id, string name)
        {
            Id = id;
            Name = name;    
        }
    }
}
