using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Try1.Models;

namespace Project_Try1.Controllers {
    public class EditQuizController : Controller {
        // GET: EditQuiz
        public ActionResult Index(FormCollection frmCl) {
            int id = int.Parse(frmCl["ID"]);

            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(id);
            return View("QuizDetail", q);
        }

        public ActionResult EditQuiz(string ID) {

            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));
            return View("EditQuiz", q);
        }

        public ActionResult EditDesp(string ID) {
            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));
            return View("EditDesp", q);
        }

        public ActionResult SaveDes(string ID, string TxtTitle, string TxtImage) {
            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));

            q.Title = TxtTitle;

            q.Image = TxtImage;

            quizes.UpdateDescription(q);

            return View("EditQuiz", q);
        }

        public ActionResult EditQuestion(string queID) {
            QuestionDM queDM = new QuestionDM();
            Question que = queDM.FindQuestionByID(
                    int.Parse(queID));
            return View("EditQuestion", que);

        }

        public ActionResult DeleteQuestion(string queID) {
            QuestionDM queDM = new QuestionDM();

            Question q = queDM.FindQuestionByID(int.Parse(queID));
            queDM.DeleteQuestion(int.Parse(queID));
                        
            return View("EditQuiz", new QuizBank().FindQuizByID(q.QuizID));

        }

        public ActionResult DuplicateQuestion(string queID) {
            QuestionDM queDM = new QuestionDM();
            Question q = queDM.FindQuestionByID(int.Parse(queID));
            queDM.DuplicateQuestion(q);
            return View("EditQuiz", new QuizBank().FindQuizByID(q.QuizID));

        }

        public RedirectToRouteResult SaveQuiz() {
            return RedirectToAction("Index", "MyQuiz");
        }

        public RedirectToRouteResult CancelEdit() {
            return RedirectToAction("Index", "MyQuiz");
        }

        public RedirectToRouteResult CancelEditDes() {
            return RedirectToAction("Index", "MyQuiz");
        }

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