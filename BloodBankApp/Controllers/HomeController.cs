using BloodBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BloodBankApp.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Login(User model, string returnUrl)
        {
            using (var db = new BloodBankDbContext())
            {

                var dataItem = db.Users.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();

                if (dataItem != null)
                {
                    FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User/Password");
                    return View();
                }
            }


        }


        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }





        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}