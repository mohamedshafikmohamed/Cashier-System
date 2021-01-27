using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Models
{
    public class CreateBillViewModel
    {
        public IEnumerable<Product> AllProduct { get; set; }
        public IEnumerable<Product> BillsProduct { get; set; }
    }
}
