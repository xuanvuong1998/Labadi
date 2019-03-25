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

        private const int QuizPerPage = 8;

        public HomeController() {
            ViewBag.QuizPerPage = QuizPerPage;
        }
                
        public ActionResult Home()
        {
            return RedirectToAction("GetQuizzes");
        }
        
        public ActionResult GetQuizzes(int? pageNum) {
            pageNum = pageNum ?? 1;
            ViewBag.IsEndOfQuizzes = false;
            var quizzes = getQuizzesForPage(pageNum.Value);

            ViewBag.IsEndOfQuizzes = quizzes.Any();           

            if (Request.IsAjaxRequest()) {                
                return PartialView("DisplayQuiz", quizzes);
            }
            ViewBag.QuizBank = quizzes;
            return View("Home");
            
        }

        private List<Quiz> getQuizzesForPage(int pageNum) {
            QuizBank bank = new QuizBank();
            return bank.LoadQuizzesPerPage(QuizPerPage, pageNum);
        }


        public RedirectToRouteResult Logout()
        {
            Session["creator"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        
    }
}