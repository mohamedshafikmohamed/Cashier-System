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
          }
        */
       
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
     public ActionResult bill(int Bill_Id)
        {
            
            BillViewModel model = new BillViewModel();
            model.products=new Dictionary<string, int>();
            model.Total=new Dictionary<string, float>();
            
          //  List<string> products = new List<string>();
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

        public ActionResult Create_bill()
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
             Dictionary<int, int>d= new Dictionary<int, int>();
            var b = DbContext.ProductBills.Where(x => x.UserId == _UserManager.GetUserId(HttpContext.User) && x.code == "").ToList();
            string products_id = "";
            Bills pb = new Bills();
            pb.products_ids = products_id;
            pb.user = User.Identity.Name;
            pb.Discount = Convert.ToInt32(Discount);
           
            float totalcost = 0;
            foreach (var p in b)
            {
                p.code = pb.Id.ToString();
                DbContext.ProductBills.Update(p);

              Product p1= DbContext.Store.Where(x => x.Id == p.ProductCode).ToList()[0];
                products_id +=(p1.Id)+" ";
                totalcost += p1.SellingPrice;
            }
            string []ids = products_id.Split(' ');
            foreach (var p in ids)
            {
                if (p == "") continue;
                Product product = (DbContext.Store.Where(x => x.Id == Convert.ToInt32(p)).FirstOrDefault());
                int id = (int)product.Id;

                if (d.ContainsKey(Convert.ToInt32(id))) { d[Convert.ToInt32(id)] += 1; }
                else { d[Convert.ToInt32(id)] = 1;}

            }
            Product product1;
            foreach (var p in d)
            {
                product1 = DbContext.Store.Where(x => x.Id == p.Key).FirstOrDefault();
            if (product1.Quantity-p.Value<0)
                {
                    ViewBag.QuantityError = "No Enough Quantity";
                    return RedirectToAction("Create_bill");
                }
                product1.Quantity -= p.Value;
                DbContext.Store.Update(product1);
            }
            
            DbContext.Bills.Add(pb);
            DbContext.SaveChanges();
            float z = 100;
            float tt = totalcost;
            float y = Convert.ToInt32(Discount);
            float t = (y/z);
            float m= (totalcost * t);
            totalcost -= m;
            y= Convert.ToInt32(Taxes);
            t = y / z;
            m = (tt * t);
            pb.products_ids = products_id;
            totalcost += m;
            pb.cost = totalcost;
            pb.date = DateTime.UtcNow.ToString();
            pb.Taxes = Convert.ToInt32(Taxes);
            DbContext.Bills.Update(pb);
            DbContext.SaveChanges();
            return RedirectToAction("Bill", new { Bill_Id = pb.Id });
        }

        
        [HttpGet]
        public ActionResult Edit_bill(int Bill_Id)
        {
            EditBillViewModel model = new EditBillViewModel();
            model.products = new List<Product_quantity>();
            Dictionary<string, int> m = new Dictionary<string, int>();
            List<int> l = new List<int>();

            model.bill = DbContext.Bills.Where(x => x.Id == Bill_Id).FirstOrDefault();
            string[] ids = model.bill.products_ids.Split(' ');
            foreach (var p in ids)
            {
                if (p == "") continue;
                Product product = (DbContext.Store.Where(x => x.Id == Convert.ToInt32(p)).FirstOrDefault());
                string name = product.Name;
                if (m.ContainsKey(name)) { m[name] += 1;  }
                else { m[name] = 1; l.Add((int)product.Id); }

            }
           
            
            int i = -1;
            foreach(var x in m)
            {
                i++;

                Product_quantity product_q = new Product_quantity();
                product_q.Id = l[i];
                product_q.name = x.Key;
                product_q.Quantity = x.Value;
                model.products.Add(product_q);
            }
            model.products_ids = l;


            return View(model);
        }
        

        [HttpPost]
        public ActionResult Edit_bill(EditBillViewModel model)
        {
          //  var b = DbContext.ProductBills.Where(x => x.code == _UserManager.GetUserId(HttpContext.User) && x.code == "").ToList();
            string ids = "";
            float totalcost = 0;
            int id = model.products_ids[0];
            Bills bill = new Bills();
            bill.date = model.bill.date;
            bill.Discount = model.bill.Discount;
            bill.Taxes = model.bill.Taxes;
            bill.Id = model.bill.Id;
            bill.user = model.bill.user;
           
          
           // bill.cost = totalcost;
            bill.date = DateTime.UtcNow.ToString();
            bill.Taxes = Convert.ToInt32(model.bill.Taxes);
            string UserId = DbContext.ProductBills.Where(u => u.ProductCode == id).FirstOrDefault().UserId;
            var removed_product = DbContext.ProductBills.Where(u => u.code == bill.Id.ToString()).ToList();
            foreach (var p in removed_product)
            {
                DbContext.ProductBills.Remove(p);
            }
            DbContext.SaveChanges();
            int j = 0;
            
            foreach (var p in model.products_ids)
            {
                
                for(int i=0;i<model.products[j].Quantity;i++)
                {
                    ids += p + " ";
                    ProductBills pr = new ProductBills();
                    pr.code = model.bill.Id.ToString();
                    pr.UserId = UserId;
                    pr.ProductCode = p;
                    DbContext.ProductBills.Add(pr);
                }
                DbContext.SaveChanges();
                j++;
            }
            bill.products_ids = ids;
            string[] b = ids.Split(' ');
            foreach (var p in b)
            {
                if (p == "") continue;


                Product p1 = DbContext.Store.Where(x => x.Id.ToString() == p).ToList()[0];

                totalcost += p1.SellingPrice;
            }
            float z = 100;
            float tt = totalcost;
           int y1 = (Convert.ToInt32(model.bill.Discount) );
            float t = y1/ z;
            float m = (totalcost * t);
            totalcost -= m;
            y1 = Convert.ToInt32(model.bill.Taxes);
            t =y1 / z;
            m = (tt * t);
            totalcost += m;

            bill.cost = totalcost;
            DbContext.Bills.Update(bill);
            DbContext.SaveChanges();
            return RedirectToAction("Bill", new { Bill_Id = bill.Id });
        }


        [HttpPost]
        public async Task<JsonResult> Delete_bill(int Id)
        {
            var products = DbContext.ProductBills.Where(x => x.code == Id.ToString()).ToList();
            foreach (var x in products)
            {
                DbContext.ProductBills.Remove(x);
            }
            DbContext.Bills.Remove(DbContext.Bills.Where(z => z.Id == Id).FirstOrDefault());
            string p = "";
           await  DbContext.SaveChangesAsync();
                return Json(p);
        }

        [HttpPost]
        public async Task<IActionResult> Addproducttobill(string code)
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
        public PartialViewResult GetPartial()
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
