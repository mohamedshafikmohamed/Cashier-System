using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Models
{
    public class Product
    {
       [Display(Name = "كود المنتج ")]

        
        public int? Id { get; set; }
        [Display(Name = "صورة المنتج ")]
      
        public string photo { get; set; }
        [Display(Name = "اسم المنتج ")]
        [Required]

        public string Name { get; set; }
        [Display(Name = " الكمية ")]
        public int Quantity { get; set; }
        [Display(Name = " سعر الشراء ")]
        public float PurchasingPrice { get; set; }
        [Display(Name = " سعر البيع ")]
        public float SellingPrice { get; set; }
        public float Gain { get; set; }
        public char IsDeleted { get; set; }
    }
}
