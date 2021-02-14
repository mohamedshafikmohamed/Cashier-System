using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Models
{
    public class Productviewmodel
    {
        [Display(Name = "كود المنتج ")]


        public int Id { get; set; }
        [Display(Name = "صورة المنتج ")]
        public IFormFile photo { get; set; }
        [Display(Name = "اسم المنتج ")]

        public string Name { get; set; }
        [Display(Name = " الكمية ")]
        public int Quantity { get; set; }
        [Display(Name = " سعر الشراء ")]
        public float PurchasingPrice { get; set; }
        [Display(Name = " سعر البيع ")]
        public float SellingPrice { get; set; }
        public float Gain { get; set; }
        public char IsDeleted { get; set; }
        public string img { get; set; }
    }
}
