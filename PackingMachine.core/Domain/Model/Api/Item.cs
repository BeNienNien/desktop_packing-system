using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model.Api
{
    public class Item
    {
            public string id { get; set; }
            public string name { get; set; }
            public List<Boms> boms { get; set; }
            public double standardWeight { get; set; }
    }
}
