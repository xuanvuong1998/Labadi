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
        
        
        public ActionResult Play(string quizID) {
                        
            if (quizID == null) {
                return Redirect("/Home/Home");
            }
            QuizBank bank = new QuizBank();

            Quiz q = bank.FindQuizByID(int.Parse(quizID));
            return View("Index", q);
        
        }
        
        public ActionResult Start(string quizID, string quizPIN) {
            QuizBank bank = new QuizBank();

            Quiz q = bank.FindQuizByID(int.Parse(quizID));

            ViewBag.quizPIN = quizPIN;
            
            return View("GameBoard", q);

        }               
        
    }
}