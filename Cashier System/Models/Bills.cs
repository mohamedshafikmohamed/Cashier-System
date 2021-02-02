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
        public string products_ids { get; set; }
        [Display(Name = " المدفوع")]
        public float cost { get; set; }
        [Display(Name = " الخصم")]
        public float Discount { get; set; }
        [Display(Name = " المحاسب")]
        public string user  { get; set; }
        [Display(Name = " الضرائب")]
        public float Taxes  { get; set; }
        [Display(Name = " موعد خروج الفاتورة")]
        public string date  { get; set; }
        public bool IsDeleted { get; set; }



    }
}
