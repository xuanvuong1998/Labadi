using Project_Try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Project_Try1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Message = TempData["error"];
            return View();
        }
      

        AccountData ac = new AccountData();
        [HttpPost]
        public RedirectToRouteResult CheckLogin(string username, string password) {
            
            ViewBag.Title = "Login to website";
            if (ac.CheckLoginAccount(username, password)) {
                                
                FormsAuthentication.SetAuthCookie(username, false);
                Session["creator"] = username;
                return RedirectToAction("Home", "Home");
            } else
            {
                TempData["error"] = "Username or password was wrong! Try again! ";
                //return RedirectToAction("Index", "Login");
                return RedirectToAction("Index", "Login");
            }          
        }
    }
}