using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Models
{
    public class BillViewModel
    {
        public List<Product> products { get; set; }
        public Bills bill { get; set; }
    }
}
