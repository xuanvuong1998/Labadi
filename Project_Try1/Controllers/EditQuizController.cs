using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Try1.Models;

namespace Project_Try1.Controllers {
    public class EditQuizController : Controller {
        // GET: EditQuiz
        [Authorize]
        public ActionResult Index(FormCollection frmCl) {
            int id = int.Parse(frmCl["ID"]);

            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(id);
            return View("QuizDetail", q);
        }
        [Authorize]
        public ActionResult EditQuiz(string ID) {

            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));
            return View("EditQuiz", q);
        }
        [Authorize]
        public ActionResult EditDesp(string ID) {
            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));
            return View("EditDesp", q);
        }
        [Authorize]
        public ActionResult SaveDes(string ID, string TxtTitle, string TxtImage) {
            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));

            q.Title = TxtTitle;

            q.Image = TxtImage;

            quizes.UpdateDescription(q);

            return View("EditQuiz", q);
        }

        public ActionResult AddQuestion(string quizID) {
            ViewBag.QuizID = quizID;

            return View("AddQuestion");
            
        }

        public ActionResult AddQuestionToQuiz(FormCollection frm) {
            int quizID = int.Parse(frm["quizID"]);

            string c1 = frm["TxtC1"];
            string c2 = frm["TxtC2"];
            string c3 = frm["TxtC3"];
            string c4 = frm["TxtC4"];
            string ans = frm["TxtAns"];
            string image = frm["TxtImage"];

            Question q = new Question {
                QuizID = quizID,
                Content = frm["TxtContent"],
                AnsA = c1,
                AnsB = c2,
                AnsC = c3,
                AnsD = c4,
                Time = int.Parse(frm["TxtTime"]),
                Answer = ans,
                Image = image
            };

            QuestionDM queDM = new QuestionDM();

            q.ID = queDM.GetMaxID() + 1;
            
            queDM.AddQuestion(q);
            
            return View("EditQuiz", new QuizBank().FindQuizByID(quizID) );
            
        }

        [Authorize]
        public ActionResult EditQuestion(string queID) {
            QuestionDM queDM = new QuestionDM();
            Question que = queDM.FindQuestionByID(
                    int.Parse(queID));
            ViewBag.QuizID = que.QuizID;
            return View("EditQuestion", que);

        }

        [Authorize]
        public ActionResult DeleteQuestion(string queID) {
            QuestionDM queDM = new QuestionDM();

            Question q = queDM.FindQuestionByID(int.Parse(queID));
            queDM.DeleteQuestion(int.Parse(queID));

            ViewBag.QuizID = q.QuizID;
            return View("EditQuiz", new QuizBank().FindQuizByID(q.QuizID));

        }

        [Authorize]
        public ActionResult DuplicateQuestion(string queID) {
            QuestionDM queDM = new QuestionDM();
            Question q = queDM.FindQuestionByID(int.Parse(queID));
            queDM.DuplicateQuestion(q);
            ViewBag.QuizID = q.QuizID;
            return View("EditQuiz", new QuizBank().FindQuizByID(q.QuizID));

        }

        [Authorize]
        public RedirectToRouteResult SaveQuiz() {
            return RedirectToAction("Index", "MyQuiz");
        }

        [Authorize]
        public RedirectToRouteResult CancelEdit() {
            return RedirectToAction("Index", "MyQuiz");
        }

        [Authorize]
        public RedirectToRouteResult CancelEditDes() {
            return RedirectToAction("Index", "MyQuiz");
        }

        [Authorize]
        public ActionResult SaveQuestion(FormCollection frm) {
            QuestionDM queDM = new QuestionDM();
            Question que = queDM.FindQuestionByID(int.Parse(frm["queID"]));

            que.Answer = frm["TxtAns"];
            que.Content = frm["TxtContent"];
            que.Time = int.Parse(frm["TxtTime"]);
            que.Image = frm["TxtImage"];
            que.AnsA = frm["TxtC1"];
            que.AnsB = frm["TxtC2"];
            que.AnsC = frm["TxtC3"];
            que.AnsD = frm["TxtC4"];

            queDM.UpdateQuestion(que);

            Quiz q = new QuizBank().FindQuizByID(que.QuizID);
            return View("EditQuiz", q);

        }

    }
}