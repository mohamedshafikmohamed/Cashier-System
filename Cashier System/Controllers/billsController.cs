using Cashier_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Controllers
{
    public class billsController : Controller
    {
        public List<string> product_in_bills = new List<string>();
        
      /*  public billsController(RoleManager<IdentityRole> _rolemanager)
        {
            rolemanager = _rolemanager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> createrole(roleviewmodel model)
        {
            IdentityRole role = new IdentityRole { Name = model.rolename };
            await rolemanager.CreateAsync(role);

            return RedirectToAction("index");
        }*/
        public IActionResult Index()
        {
            return View();
        }
     /*  public ActionResult bill(string code)
        {
            product_in_bills.Add(code);
            return RedirectToPage("store");
        }*/
     [HttpPost]
        public ActionResult Create_bill(List<int>codes)
        {
            //product_in_bills.Add(code);
            return RedirectToPage("store");
        }
    }
}
