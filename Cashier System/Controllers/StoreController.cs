using Cashier_System.Data;
using DevExtreme.AspNet.Data;
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
using DevExtreme.AspNet.Mvc;
using Cashier_System.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Cashier_System.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StoreController : Controller
    {
        public ApplicationDbContext DbContext;
        private readonly IWebHostEnvironment Ihosting;

        public StoreController(ApplicationDbContext _DbContext, IWebHostEnvironment _Ihosting)
        {
            DbContext = _DbContext;
            Ihosting = _Ihosting;
        }
        [HttpGet]
        public Object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(DbContext.Store, loadOptions);
         
        }
           [HttpPost]
        public IActionResult Post(string values)
        {
            var newEmployee = new Product();
            JsonConvert.PopulateObject(values, newEmployee);



            DbContext.Store.Add(newEmployee);
            DbContext.SaveChanges();

            return Ok();
           
         
        }
        [HttpPut]
        public IActionResult Updateproduct(string key, string values)
        {
            var order = DbContext.Store.First(o => o.Id == key);
            JsonConvert.PopulateObject(values, order);

            

            DbContext.SaveChanges();

            return Ok(order);
        }
        public void Delete(string key)
        {
            var p = DbContext.Store.First(a => a.Id == key);
            DbContext.Store.Remove(p);
            DbContext.SaveChanges();
        }
        public void Upload_photo(IFormFile photo)
        {
            string p = Path.Combine(Ihosting.WebRootPath, "images");
            string p2 = Path.Combine(p, photo.FileName);
            photo.CopyTo(new FileStream(p2, FileMode.Create));

        }
        }
    }
