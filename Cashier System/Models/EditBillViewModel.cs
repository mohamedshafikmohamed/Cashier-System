using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Models
{
    public class EditBillViewModel
    {
        public List<Product_quantity> products { get; set; }
        public List<int> products_ids { get; set; }
        public Bills bill { get; set; }
    }
}
