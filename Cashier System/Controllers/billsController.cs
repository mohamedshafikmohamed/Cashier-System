using Cashier_System.Data;
using Cashier_System.Models;
using Microsoft.AspNetCore.Authorization;
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
       
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Mybills()
        {
           string email = User.Identity.Name;
           var x= DbContext.Bills.Where(x => x.user==email).ToList();
            return View(x);
        } 
        [HttpGet]
        public ActionResult Accountants()
        {
            List<Users> u = new List<Users>();
            
           var x= DbContext.Users.ToList();
            foreach(var z in x)
            {
                Users u1 = new Users();
                u1.Email = z.Email;
                u1.Id = z.Id;
                u1.PasswordHash = z.PasswordHash;
                u.Add(u1);

            }
            return View(u);
        }

        [HttpGet]
     public ActionResult bill(int Bill_Id)
        {
            
            BillViewModel model = new BillViewModel();
            model.products=new Dictionary<string, int>();
            model.Total=new Dictionary<string, float>();
            
            List<string> products = new List<string>();
            model.bill= DbContext.Bills.Where(x => x.Id == Bill_Id).FirstOrDefault();
            string[] ids = model.bill.products_ids.Split(' ');
            foreach (var p in ids) {
                if (p == "") continue;
                Product  product = (DbContext.Store.Where(x => x.Id == Convert.ToInt32(p)).FirstOrDefault());
                string name = product.Name;
                
                if (model.products.ContainsKey(name)) { model.products[name] += 1; model.Total[name] += product.SellingPrice; ; }
                else { model.products[name] = 1; model.Total[name] = 0; model.Total[name] += product.SellingPrice; }
              
                    }
           

                return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult bills()
        {


            return View(DbContext.Bills.ToList());
        }
        [HttpGet]

        public ActionResult Create_bill(/*List<int>codes*/)
        {
           
            CreateBillViewModel model=new CreateBillViewModel();
          
            model.AllProduct = DbContext.Store.ToList();
            model.BillsProduct = s();
            GetPartial();
            return View(model);
        }
      
       
        [HttpPost]
        public ActionResult Create_bill(string Discount,string Taxes)
        {
            var b = DbContext.ProductBills.Where(x => x.UserId == _UserManager.GetUserId(HttpContext.User) && x.code == "").ToList();
            string products_id = "";
            Bills pb = new Bills();
            pb.products_ids = products_id;
            pb.user = User.Identity.Name;
            pb.Discount = Convert.ToInt32(Discount);
            DbContext.Bills.Add(pb);
            DbContext.SaveChanges();
            float totalcost = 0;
            foreach (var p in b)
            {
                p.code = pb.Id.ToString();
                DbContext.ProductBills.Update(p);

              Product p1= DbContext.Store.Where(x => x.Id == p.ProductCode).ToList()[0];
                products_id +=(p1.Id)+" ";
                totalcost += p1.SellingPrice;
            }
            float z = 100;
            float y = Convert.ToInt32(Discount);
            float t = (y/z);
            float m= (totalcost * t);
            totalcost -= m;
            y= Convert.ToInt32(Taxes);
            t = y / z;
            m = (totalcost * t);
            pb.products_ids = products_id;
            totalcost += m;
            pb.cost = totalcost;
            pb.date = DateTime.UtcNow.ToString();
            pb.Taxes = Convert.ToInt32(Taxes);
            DbContext.Bills.Update(pb);
            DbContext.SaveChanges();
            return RedirectToAction("Bill", new { Bill_Id = pb.Id });
        }  
        [HttpPost]
        public async Task<JsonResult> Addproducttobill(string code)
        {
            ProductBills p = new ProductBills();
            p.ProductCode =Convert.ToInt32( code);
            p.UserId = _UserManager.GetUserId(HttpContext.User);
            p.code = "";
            //   p.Id = 457;
            await DbContext.ProductBills.AddAsync(p);
            await DbContext.SaveChangesAsync();
            
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

        [HttpPost]
        public async Task<JsonResult> Deleteproductfrombill(int id)
        {
            
            var p=DbContext.ProductBills.Where(x => (x.code == "" )&& (x.ProductCode == id) && (x.UserId == _UserManager.GetUserId(HttpContext.User))).FirstOrDefault();
           
             DbContext.ProductBills.Remove(p);
            await DbContext.SaveChangesAsync();
            GetPartial();
            return Json(p);
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

        [HttpPost]
        public ActionResult Print_Bill(/*List<int>codes*/)
        {

            CreateBillViewModel model = new CreateBillViewModel();

            model.AllProduct = DbContext.Store.ToList();
            model.BillsProduct = s();
            GetPartial();
            return View(model);
        }
    }
}
