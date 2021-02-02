using Cashier_System.Data;
using Cashier_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cashier_System.Controllers
{
    public class AdminController : Controller
    {
        public ApplicationDbContext DbContext;
        RoleManager<IdentityRole> rolemanager;
        private readonly UserManager<IdentityUser> _UserManager;

        public AdminController(RoleManager<IdentityRole> _rolemanager, ApplicationDbContext _DbContext, UserManager<IdentityUser> Usermanager)
        {
            rolemanager = _rolemanager;
            _UserManager = Usermanager;
            DbContext = _DbContext;
        }
      
        [HttpGet]
        public ActionResult Accountants()
        {
            List<Users> u = new List<Users>();

            var x = DbContext.Users.ToList();
            foreach (var z in x)
            {
                Users u1 = new Users();
                u1.Email = z.Email;
                u1.Id = z.Id;
              //  u1.PasswordHash = z.PasswordHash;
                u.Add(u1);

            }
            return View(u);
        } 
        [HttpPost]
        public async Task<JsonResult> DeleteUser(string id)
        {
            var user = DbContext.Users.Remove(await _UserManager.FindByIdAsync(id));
            DbContext.SaveChanges();
            string x="fdf";
            return Json(x);
        }
    }
}
