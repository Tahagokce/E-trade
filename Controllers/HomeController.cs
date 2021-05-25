using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;




namespace Abc.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();

        // GET: Home
        public ActionResult Index()
        {

            var bag = _context.Products.Where(i => i.IsHome && i.IsApproved).Select(a => new ProductViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description.Length > 50 ? a.Description.Substring(0, 47) + "..." : a.Description,
                IsHome = a.IsHome,
                Price = a.Price,
                İmage = a.İmage,
                Stock = a.Stock
            });
            return View(bag.ToList());
        }

        public ActionResult Details(int? id)
        {
            return View(_context.Products.FirstOrDefault(i => i.Id == id));
        }

        public ActionResult List(int? id)
        {


            var bag = _context.Products.Where(i => i.IsApproved).Select(a => new ProductViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description.Length > 50 ? a.Description.Substring(0, 47) + "..." : a.Description,
                IsHome = a.IsHome,
                Price = a.Price,
                İmage = string.IsNullOrEmpty(a.İmage) ? "unnamed.jpg" : a.İmage,
                Stock = a.Stock,
                CategoryId = a.CategoryId
            }).AsQueryable();

            if (id != null)
            {
                bag = bag.Where(i => i.CategoryId == id);
            }



            return View(bag.ToList());
        }

        public PartialViewResult GetCategories()
        {



            var bag = _context.Categories.Include(i => i.Products).ToList();


            var bag2 = _context.Categories.Select(i => new GetCategoryViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Products = i.Products.Select(a => new ProductViewModel()
                {
                    IsApproved = a.IsApproved,
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Stock = a.Stock,
                    İmage = a.İmage ?? "unnamed.jpg",
                    Description = a.Description

                    ///link sorgula count ata kategoriler için



                }).Where(v => v.IsApproved == true).ToList()
            }).ToList();


            ViewBag.AllCount = _context.Products.Count(i => i.IsApproved == true);





            return PartialView(bag2);
        }


    }
}