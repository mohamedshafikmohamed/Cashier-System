using Cashier_System.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;


using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

using Cashier_System.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;


namespace Cashier_System.Controllers
{
   
    public class StoreController : Controller
    {
        public ApplicationDbContext DbContext;
        private readonly IWebHostEnvironment Ihosting;
        
        public StoreController(ApplicationDbContext _DbContext, IWebHostEnvironment _Ihosting, RoleManager<IdentityRole> _rolemanager)
        {
            DbContext = _DbContext;
            Ihosting = _Ihosting;
           
        }

        public IActionResult Create()
        {
            return View();
        }
            [HttpPost]
        
        public IActionResult Create(Productviewmodel model)
        {
            
            var product = new Product();
            
            product.Gain = (model.SellingPrice - model.PurchasingPrice) * model.Quantity;
            product.Id = null;
            product.PurchasingPrice = model.PurchasingPrice;
            product.Quantity = model.Quantity;
            product.SellingPrice = model.SellingPrice;
            product.Name = model.Name;

            DbContext.Store.Add(product);
            DbContext.SaveChanges();
            
            Upload_photo(model.photo, (int)product.Id);
            product.photo = product.Id.ToString() + model.photo.FileName;
            DbContext.Store.Update(product);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
           
         
        }
        public IActionResult Updateproduct(int Id)
        {
            var product=DbContext.Store.Find(Id);
            Productviewmodel model = new Productviewmodel();
            model.Id = (int)product.Id;
            model.SellingPrice = product.SellingPrice;
            model.PurchasingPrice = product.PurchasingPrice;
            model.Name = product.Name;
            model.Quantity = product.Quantity;

            model.img = product.photo;

            return View(model);
        }
            [HttpPost]
         public IActionResult Updateproduct(Productviewmodel model)
          {
            var product = new Product();

            product.Gain = (model.SellingPrice - model.PurchasingPrice) * model.Quantity;
            product.Id = model.Id;
            product.PurchasingPrice = model.PurchasingPrice;
            product.Quantity = model.Quantity;
            product.SellingPrice = model.SellingPrice;
            product.Name = model.Name;
            if (model.photo != null)
            { Upload_photo(model.photo, (int)product.Id);
                product.photo = product.Id.ToString() + model.photo.FileName;
            }
            else
            {
                product.photo = model.img;
            }
            DbContext.Store.Update(product);
            DbContext.SaveChanges();

              return RedirectToAction("Index");
        }
        public JsonResult Delete(int Id)
        {
            var p = DbContext.Store.First(a => a.Id == Id);
            DbContext.Store.Remove(p);
            DbContext.SaveChanges();
            return Json(p);
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View(DbContext.Store.ToList());
        }

        public void Upload_photo(IFormFile photo,int code)
        {
            string p = Path.Combine(Ihosting.WebRootPath, "images");
            
            string p2 = Path.Combine(p, code.ToString()+ photo.FileName);
            photo.CopyTo(new FileStream(p2, FileMode.Create));
          //  photo.Delete(Path.Combine(rootFolder, authorsFile));

        }

        }

}
