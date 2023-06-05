using PackingMachine.core.Domain.Model.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Communicaton
{
    public class Products
    {
        public List<AllItems> items { get; set; }
    }
}
