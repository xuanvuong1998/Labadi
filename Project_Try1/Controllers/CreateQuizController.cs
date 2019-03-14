using Project_Try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Try1.Controllers {
    public class CreateQuizController : Controller {
        // GET: CreateQuiz
        public ActionResult Index() {
            return View("CreateNewQuiz");
        }

        public ActionResult QuestionDetails() {
            return View("QuestionDetails");
        }
        public ActionResult AddQuestion(FormCollection frm) {
            string title = frm["TxtTitle"];
            string image = frm["TxtImage"];


            //ViewBag.Title = title;
            //ViewBag.Image = image;

            Session["Title"] = title;
            Session["Image"] = image;

            return View("AddQuestion");

        }

        public ActionResult AddQuestionToQuiz(FormCollection frm) {
            dynamic list = Session["QuestionList"];
            
            if (list == null) {
                list = new List<Question>();
            }

            string c1 = frm["TxtC1"];
            string c2 = frm["TxtC2"];
            string c3 = frm["TxtC3"];
            string c4 = frm["TxtC4"];
            string ans = frm["TxtAns"];
            string image = frm["TxtImage"];

            Question q = new Question {
                Content = frm["TxtContent"],                
                AnsA = c1,
                AnsB = c2,
                AnsC = c3,
                AnsD = c4,
                Time = int.Parse(frm["TxtTime"]),
                Answer = ans,
                Image = image
            };
            
            list.Add(q);
            Session["QuestionList"] = list;

            return View("AddQuestion");
        }

        public RedirectToRouteResult CancelCreatingQuiz() {
            Session["QuestionList"] = null;
            Session["Title"] = null;
            Session["Image"] = null;            
            

            return RedirectToAction("Index", "MyQuiz");
        }

        public ActionResult CancelCreatingQuestion() {

            return View("AddQuestion");
        }

        public RedirectToRouteResult SaveQuiz() {
            var queList = (List<Question>) Session["Questionlist"];

            QuestionDM queDM = new QuestionDM();
            QuizBank quizBank = new QuizBank();

            Quiz q = new Quiz {
                ID = quizBank.GetMaxID() + 1,
                Title = (string) Session["Title"],
                Image = (string) Session["Image"],
                Creator = (string) Session["Creator"],     
                QuestionList = new List<Question>()
            };

            
            int maxID = queDM.GetMaxID();
            foreach(var item in queList) {
                item.ID = ++maxID;
                item.QuizID = q.ID;
                q.QuestionList.Add(item);                               
            }

            quizBank.AddNewQuiz(q);

            Session["Title"] = null;
            Session["Image"] = null;            
            Session["QuestionList"] = null;
            

            return RedirectToAction("index", "MyQuiz");
            
        }

    }
}