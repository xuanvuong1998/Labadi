using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Try1.Models;

namespace Project_Try1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Test

        [Authorize]
        public ActionResult Home()
        {

            QuizBank bank = new QuizBank();
            //QuestionDM que = new QuestionDM();
            List<Quiz> list = bank.LoadTopQuizs(50);
            //que.LoadQuestionList();
            //List<Question> list = que.GetQuestions();
            ViewBag.QuizBank = list;
            
            return View();
        }
        public RedirectToRouteResult Logout()
        {

            Session["creator"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}