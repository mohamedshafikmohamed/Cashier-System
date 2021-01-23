using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cashier_System.Data;
using Cashier_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cashier_System.Pages
{
    public class createroleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly Cashier_System.Data.ApplicationDbContext _context;
        public roleviewmodel Product { get; set; }
        public createroleModel(RoleManager<IdentityRole> _rolemanager,ApplicationDbContext context)
        {
            _context = context;
            rolemanager = _rolemanager;
        }
        public void OnGet()
        {

        }
        

        public async Task OnPostAsync()
        {
            IdentityRole role = new IdentityRole { Name = "Sales" };
            await rolemanager.CreateAsync(role);
            _context.Roles.Add(role);
            _context.SaveChanges();
        }
    }
}
