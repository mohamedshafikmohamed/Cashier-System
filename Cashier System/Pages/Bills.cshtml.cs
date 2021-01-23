using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cashier_System.Data;
using Cashier_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cashier_System.Pages
{
    public class BillsModel : PageModel
    {
        public ApplicationDbContext DbContext;
       
        public List<Product> products { get; set; }

        public BillsModel(  ApplicationDbContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task OnGet()
        {
            products =await DbContext.Store.ToListAsync();
        }
      
    }
}
