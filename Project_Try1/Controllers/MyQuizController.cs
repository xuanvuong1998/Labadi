using Project_Try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Try1.Controllers
{
    public class MyQuizController : Controller
    {
        // GET: MyQuiz
        public ActionResult Index()
        {

            QuizBank bank = new QuizBank();

            List<Quiz> list = bank.GetAllQuizesOfCreator((string)Session["creator"]);

            
            ViewBag.QuizList = list;
            
            return View();
        }
    }
}