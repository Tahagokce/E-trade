using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Abc.MvcWebUI.Controllers
{

    [Authorize(Roles = "admin")]
    public class AccountController : Controller
    {
        // GET: Account

        private UserManager<ApplicationUser> UserManager;    //managerlermizi oluşturuyoruz User manager birde user Storye ihtiyaç duyuyor ctor'da userStorymizi oluşturuyoruz
        private RoleManager<ApplicationRole> RoleManager;



        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext()); // userManagerin çalışması için oluşturulan userStoryimiz
            UserManager = new UserManager<ApplicationUser>(userStore);              //store ile bağdaştırıyotruz


            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {

            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)// formdan gelen verileri kontrol ediyoruz hatalı değil ise kayıt işlemlerini yapmaya hazırız demektir
            {
                var user = new ApplicationUser();  //modelden gelen verileri appuserre paketliyoruz ama passwordu güvenlik amaçlı olarak ıdentity resulta paketliceğiz
                user.Name = model.Name;
                user.SurName = model.Name;
                user.UserName = model.UserName;
                user.Email = model.Email;
                

                IdentityResult result = UserManager.Create(user, model.Password);//modeli oluşturuyourz create metotuyla ve sonra oluştumu diye sorgulamak için resulta atıyoruz
                if (result.Succeeded)               //kayıt işlemi gerçekleştimi? onagöre yönlendiricez
                {// kullanıcı oluştu rol atama işlemlerini burada gerçekleştirebilirsin


                    if (RoleManager.RoleExists("User")) // user adında rol varmı bunun kontrolünü sağlamak amaçlı kullanılır
                    {
                        UserManager.AddToRole(user.Id, "User");// varsa user'in idsini bu role atarız
                    }

                    return RedirectToAction("Login", "Account"); //rol yoksa login sayfasına geri yönlendirilir

                }
                else  //create olmadıysa succeeded false ise bir hata var demek hatayı belirtmemia geregir
                {

                    ModelState.AddModelError("modelcreateerror", "Kullanıcı oluşturulmadı!");// buradaki hatanın modellerle ilişkisi olmadığı için
                                                                                             //yeni bir hata oluşturmamız gerek oluşturma şekli budur
                }

            }
            return View(model);// model.is valid değilse hatalı veriler get sayfasına doldurulup gönderilir
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid) //model hatalımı kontrol ettik 
            {
                var userResult = UserManager.Find(model.UserName, model.Password);  //usermanager ile kullanıcı kontrolünü sağladık kullanıcı varsa login işlemlerini yapıcaz 
                if (userResult != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication; //clasik login işlemleri  
                    var identityClaims = UserManager.CreateIdentity(userResult, "ApplicationCookie");//bulunan kullanıcıya cookieleri atanır
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;     //cookilerin kabul etilmesini kullanıcı kendi sağlıyor
                    authManager.SignIn(authProperties,identityClaims);//girişi sağlıyoruz http kendisi sağlar bu yetkiyi userManagerden alakasız

                  

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("loginUsererrorr", "Kullanıcı bulunamadı");// buradaki hatanın modellerle ilişkisi olmadığı için
                    

                }



            }


            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication; //authmanagermizi oluşturup çıkış yapıyoruz
            authManager.SignOut();


            return RedirectToAction("Index","Home");
        }
    }





}