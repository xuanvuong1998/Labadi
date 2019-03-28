using Project_Try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_Try1.Controllers
{
    public class MyQuizController : Controller
    {
       
        // GET: MyQuiz
        [Authorize]
        
        public ActionResult Index()
        {           
            QuizBank bank = new QuizBank();

            List<Quiz> list = bank.GetAllQuizesOfCreator((string)Session["creator"]);
            
            ViewBag.QuizList = list;
          
            return View();
        }

        [Authorize]
        public ActionResult FindByName(string TxtSearch) {
            QuizBank bank = new QuizBank();
            List<Quiz> list = bank.FindQuizzesByName(TxtSearch);

            ViewBag.QuizList = list;

            return View("Index");

        }
    }
}