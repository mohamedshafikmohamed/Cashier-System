using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Models
{
    public class BillViewModel
    {
        public Dictionary<string, int> products { get; set; }
        public Dictionary<string, float> Total { get; set; }
        public Bills bill { get; set; }
    }
}
