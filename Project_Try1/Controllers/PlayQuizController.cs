using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Try1.Models;

namespace Project_Try1.Controllers
{
    public class PlayQuizController : Controller
    {
        // GET: PlayQuiz
        [Authorize]
        public ActionResult Index(string quizID)
        {
            QuizBank bank = new QuizBank();

            Quiz q = bank.FindQuizByID(int.Parse(quizID));

            return View("Index", q);
        }
    }
}