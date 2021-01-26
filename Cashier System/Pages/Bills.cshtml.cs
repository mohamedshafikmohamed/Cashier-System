using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cashier_System.Data;
using Cashier_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cashier_System.Pages
{
    public class BillsModel : PageModel
    {
        public ApplicationDbContext DbContext;
        private readonly UserManager<IdentityUser> _UserManager;
        public List<Product> products { get; set; }
      
        public List<Product> Billsproducts { get; set; }
        [BindProperty]
        public  string codes { get; set; }

        public BillsModel(UserManager<IdentityUser> Usermanager, ApplicationDbContext _DbContext)
        {
            _UserManager = Usermanager;
            DbContext = _DbContext;
        }

        public async Task OnGet()
        {
            DbContext.ProductBills.RemoveRange(DbContext.ProductBills.Where(x => x.code == ""&&x.UserId == _UserManager.GetUserId(HttpContext.User)));
            DbContext.SaveChanges();
           var biilsproductObjects =  DbContext.ProductBills.Where(x => x.UserId == _UserManager.GetUserId(HttpContext.User)).ToList();
            List<Product> pr = new List<Product>();
            foreach (var p in biilsproductObjects)
            {
               
                    pr.Add(DbContext.Store.Where(x => x.Id == p.ProductCode).ToList()[0]);
                

            }
            Billsproducts = pr;
            products =await DbContext.Store.ToListAsync();
        }
        public async Task OnPost()
        {
            await DbContext.SaveChangesAsync();
            codes += "4";
            
        }
       
     
        public async Task OnPostAddBillsProducts(string productid)
        {
            ProductBills p = new ProductBills();
            p.ProductCode = Convert.ToInt32(productid);
            p.UserId = _UserManager.GetUserId(HttpContext.User);
            p.code = "";
            DbContext.Add(p);
            await  DbContext.SaveChangesAsync();

            
            
        }

    }
}
