using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cashier_System.Data;
using Cashier_System.Models;

namespace Cashier_System.Pages
{
    public class StoreModel : PageModel
    {
        private readonly Cashier_System.Data.ApplicationDbContext _context;

        public StoreModel(Cashier_System.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
          
            Product = await _context.Store.ToListAsync();
        }
    }
}
