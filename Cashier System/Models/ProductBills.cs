using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Models
{
    public class ProductBills
    {
        [Key]
        public int Id { get; set; }
        
        public string code { get; set; }
        public int ProductCode { get; set; }
        public string UserId { get; set; }


    }
}
