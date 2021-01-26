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
    public class billsController : Controller
    {
        public ApplicationDbContext DbContext;
        RoleManager<IdentityRole> rolemanager;
        private readonly UserManager<IdentityUser> _UserManager;

        public billsController(RoleManager<IdentityRole> _rolemanager, ApplicationDbContext _DbContext, UserManager<IdentityUser> Usermanager)
          {
              rolemanager = _rolemanager;
            _UserManager = Usermanager;
            DbContext = _DbContext;
          }
        /*
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
        [HttpPost]
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }
        public IActionResult Index()
        {

            return View();
        }
    
     /*  public ActionResult bill(string code)
        {
            product_in_bills.Add(code);
            return RedirectToPage("store");
        }*/
     [HttpGet]
        public ActionResult Create_bill(/*List<int>codes*/)
        {
            //product_in_bills.Add(code);
            BillsViewModel model=new BillsViewModel();
          
            model.AllProduct = DbContext.Store.ToList();
            model.BillsProduct = s();

            return View(model);
        }
        [HttpPost]
        public ActionResult Create_bill(string code)
        {
            ProductBills p = new ProductBills();
            p.ProductCode = Convert.ToInt32(code);
            p.UserId = _UserManager.GetUserId(HttpContext.User);
            p.code = "";
            DbContext.Add(p);
             DbContext.SaveChangesAsync();

            return RedirectToPage("store");
        }  
        [HttpPost]
        public async Task<JsonResult> Addproducttobill(string code)
        {
            ProductBills p = new ProductBills();
            p.ProductCode =Convert.ToInt32( code);
            p.UserId = _UserManager.GetUserId(HttpContext.User);
            p.code = "";
         //   p.Id = 457;
            DbContext.ProductBills.Add(p);
            await DbContext.SaveChangesAsync();
            GetPartial();
            return Json(p);
        }
        public List<Product> s()
        {
            DbContext.ProductBills.RemoveRange(DbContext.ProductBills.Where(x => (x.code == ""||x.code==null) && x.UserId == _UserManager.GetUserId(HttpContext.User)));
            DbContext.SaveChanges();
            var biilsproductObjects = DbContext.ProductBills.Where(x => x.UserId == _UserManager.GetUserId(HttpContext.User)).ToList();
            List<Product> pr = new List<Product>();
            foreach (var p in biilsproductObjects)
            {

                pr.Add(DbContext.Store.Where(x => x.Id == p.ProductCode).ToList()[0]);


            }
         
            return pr;
        }
        public IActionResult GetPartial()
        {
          
            var b = DbContext.ProductBills.Where(x => x.UserId == _UserManager.GetUserId(HttpContext.User)&&x.code=="").ToList();

            List<Product> pr = new List<Product>();
            foreach (var p in b)
            {

                pr.Add(DbContext.Store.Where(x => x.Id == p.ProductCode).ToList()[0]);


            }

            return PartialView("_BillProducts", pr);
        }
    }
}
