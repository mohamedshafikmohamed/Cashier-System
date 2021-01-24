using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Models
{
    public class Bills
    {

        //  public virtual IdentityUser  { get; set; }
        [Display(Name = "كود الفاتورة ")]
        public int Id { get; set; }
        

        public string products_names { get; set; }


        [Display(Name = " المدفوع")]
        public float cost { get; set; }

    }
}
