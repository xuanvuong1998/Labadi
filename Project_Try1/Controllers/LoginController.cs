using Project_Try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_Try1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Fail() {
            return View("LoginFail");
        }

        AccountData ac = new AccountData();
        [HttpPost]
        public RedirectToRouteResult CheckLogin(string username, string password) {
            
            ViewBag.Title = "Login to website";
            if (ac.CheckLoginAccount(username, password)) {
                                
                FormsAuthentication.SetAuthCookie(username, false);
                Session["creator"] = username;
                return RedirectToAction("Home", "Home");
            } else {
                return RedirectToAction("Fail", "Login");
            }          
        }
    }
}