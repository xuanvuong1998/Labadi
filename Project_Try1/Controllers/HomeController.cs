using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_Try1.Models;

namespace Project_Try1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Test
                
        public ActionResult Home()
        {

            QuizBank bank = new QuizBank();            
            List<Quiz> list = bank.LoadTopQuizs(100);           
            ViewBag.QuizBank = list;
            
            return View();
        }
        public RedirectToRouteResult Logout()
        {
            Session["creator"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}