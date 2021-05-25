using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Entity.ViewModel;
using Microsoft.AspNet.Identity;

namespace Abc.MvcWebUI.Controllers
{


    [Authorize(Roles = "admin")]
    public class CommentsController : Controller
    {
        DataContext db = new DataContext();


        // GET: Commments
        [AllowAnonymous]
        public ActionResult Index(int id)//yapılan yorumları anlık olarak getirmeyi beceremedim ama zaten yapılan yorum önce kontrol altına alıcanak dahasonra siteye eklenicek
        {
            var comment = db.Comments.Where(i => i.ProductId == id).Select(a => new CommentViewModel()
            {
                Description = a.Description,
                CreateTime = a.CreateTime,
                Star = a.Star,
                Product = a.Product,
                UserName = User.Identity.Name
            }).ToList();
            return View(comment);

        }



        public ActionResult AdminIndex()//yapılan yorumları anlık olarak getirmeyi beceremedim ama zaten yapılan yorum önce kontrol altına alıcanak dahasonra siteye eklenicek
        {
            var comment = db.Comments.Select(i => new CommentViewModel()
            {
                Id = i.Id,
                UserName = i.UserName,
                Description = i.Description,
                Star = i.Star,
                CreateTime = i.CreateTime,
                IsApproved = i.IsApproved,
                Product = i.Product,
                ProductId = i.ProductId,
                UserId = i.UserId

            }).ToList();
            return View(comment);

        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult AddComment([Bind(Include = "ProductId,UserId,Description,Star")] CommentViewModel model)
        {
            if (Request.IsAuthenticated)
            {


                if (ModelState.IsValid)
                {
                    var user = User.Identity.GetUserId();
                    Comment comment = new Comment
                    {
                        Description = model.Description,
                        Star = model.Star,
                        UserId = user,
                        UserName = User.Identity.Name,
                        ProductId = model.ProductId,
                        CreateTime = DateTime.Now
                    };

                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return PartialView("_YorumYapıldı");
                }
            }

            return View(model);

        }

        [AllowAnonymous]
        public ActionResult AddComment(int id)
        {
            return View();
        }




        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
        }









        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int Id, bool IsApproved)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Comment comment = db.Comments.Find(Id);
            if (comment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(comment.ProductId);

            comment.IsApproved = IsApproved;
            comment.Product = product;
            db.SaveChanges();
            return RedirectToAction("AdminIndex");


            return View(comment);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Comment comment = db.Comments.Find(id);
            Product product = db.Products.Find(comment.ProductId);
            ViewBag.Name = product.Name;

            return View(comment);
        }








    }
}