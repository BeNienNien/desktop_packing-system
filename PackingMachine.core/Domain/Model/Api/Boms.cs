using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model.Api
{
    public class Boms
    {

            public string id { get; set; }
            public string name { get; set; }
            public List<object> boms { get; set; }
            public double standardWeight { get; set; }


    }
}
