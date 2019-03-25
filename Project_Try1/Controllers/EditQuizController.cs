using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Project_Try1.Models;

namespace Project_Try1.Controllers
{
    public class EditQuizController : Controller
    {
        // GET: EditQuiz
        [Authorize]
        public ActionResult Index(FormCollection frmCl)
        {
            int id = int.Parse(frmCl["ID"]);
          
            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(id);
            return View("EditQuiz", q);
        }

        public ActionResult Index2(string quizID) {
            int id = int.Parse(quizID);
            
            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(id);
            return View("EditQuiz", q);
        }

        
        [Authorize]
        public ActionResult DeleteQuiz(string quizID)
        {
            QuizBank bank = new QuizBank();
            bank.DeleteQuiz(int.Parse(quizID));            
            return Redirect("/MyQuiz/Index");
        }

        [Authorize]
        public ActionResult EditQuiz(string ID)
        {

            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));
            return View("EditQuiz", q);
        }
        [Authorize]
        public ActionResult EditDesp(string ID)
        {
            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));
            return View("EditDesp", q);
        }
        [Authorize]
        public ActionResult SaveDes(HttpPostedFileBase file, string ID, string TxtTitle, string TxtDescQuiz)
        {
            QuizBank quizes = new QuizBank();

            Quiz q = quizes.FindQuizByID(int.Parse(ID));

            q.Title = TxtTitle;
            q.Desp = TxtDescQuiz;


            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/resources/images/QuizImages"),
                        Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    // WebImage belong to WebHelper class which supports the crop, flip, watermark operation etc.
                    WebImage img = new WebImage(file.InputStream);
                    if (img.Width > 1200)
                        img.Resize(1200, 600);
                    img.Save(path);

                    q.Image = file.FileName;

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            quizes.UpdateDescription(q);

            return View("EditQuiz", q);
        }

        public ActionResult AddQuestion(string quizID)
        {
            ViewBag.QuizID = quizID;

            return View("AddQuestion");

        }


        [Authorize]
        public ActionResult AddQuestionToQuiz(HttpPostedFileBase file, FormCollection frm)
        {

            QuestionDM queDM = new QuestionDM();
            
            int quizID = int.Parse(frm["quizID"]);

            string c1 = frm["TxtC1"];
            string c2 = frm["TxtC2"];
            string c3 = frm["TxtC3"];
            string c4 = frm["TxtC4"];
            string ans = frm["TxtAns"];
            string image = "";
            
            if (file != null && file.ContentLength > 0)
            {
                try
                {                    
                    string path = Path.Combine(Server.MapPath("~/resources/images/QuestionImages"),
                        Path.GetFileName(file.FileName));                    
                    file.SaveAs(path);

                    // WebImage belong to WebHelper class which supports the crop, flip, watermark operation etc.
                    WebImage img = new WebImage(file.InputStream);
                    if (img.Width > 1200)
                        img.Resize(1200, 600);
                    img.Save(path);

                    image = file.FileName;

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }else if (file == null)
            {
                image = "default.png";
            }

            Question q = new Question
            {
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

            

            q.ID = queDM.GetMaxID() + 1;

            queDM.AddQuestion(q);

            return View("EditQuiz", new QuizBank().FindQuizByID(quizID));

        }

        [Authorize]
        public ActionResult EditQuestion(string queID)
        {
            QuestionDM queDM = new QuestionDM();
            Question que = queDM.FindQuestionByID(
                    int.Parse(queID));
            ViewBag.QuizID = que.QuizID;
            return View("EditQuestion", que);

        }

        [Authorize]        
        public ActionResult DeleteQuestion(string queID, string quizID)
        {
            QuestionDM queDM = new QuestionDM();
                        
            Question q = queDM.FindQuestionByID(int.Parse(queID));

            if (q != null) {
                queDM.DeleteQuestion(int.Parse(queID));
            }
                                               
            //return RedirectToAction("Index2", new { quizID = q.QuizID });
            //return Redirect("/EditQuiz/Index2?quizID=" + q.QuizID);
            return View("EditQuiz", new QuizBank().FindQuizByID(int.Parse(quizID)));

        }

        [Authorize]
        public ActionResult DuplicateQuestion(string queID)
        {
            QuestionDM queDM = new QuestionDM();
            Question q = queDM.FindQuestionByID(int.Parse(queID));
            queDM.DuplicateQuestion(q);
            ViewBag.QuizID = q.QuizID;
            return View("EditQuiz", new QuizBank().FindQuizByID(q.QuizID));

        }

        [Authorize]
        public RedirectToRouteResult SaveQuiz()
        {
            return RedirectToAction("Index", "MyQuiz");
        }

        [Authorize]
        public RedirectToRouteResult CancelEdit()
        {
            return RedirectToAction("Index", "MyQuiz");
        }

        [Authorize]
        public RedirectToRouteResult CancelEditDes()
        {
            return RedirectToAction("Index", "MyQuiz");
        }

        [Authorize]
        public ActionResult SaveQuestion(HttpPostedFileBase file,FormCollection frm)
        {
            QuestionDM queDM = new QuestionDM();
            Question que = queDM.FindQuestionByID(int.Parse(frm["queID"]));

            que.Answer = frm["TxtAns"];
            que.Content = frm["TxtContent"];
            que.Time = int.Parse(frm["TxtTime"]);
            que.AnsA = frm["TxtC1"];
            que.AnsB = frm["TxtC2"];
            que.AnsC = frm["TxtC3"];
            que.AnsD = frm["TxtC4"];

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/resources/images/QuestionImages"),
                        Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    // WebImage belong to WebHelper class which supports the crop, flip, watermark operation etc.
                    WebImage img = new WebImage(file.InputStream);
                    if (img.Width > 1200)
                        img.Resize(1200, 600);
                    img.Save(path);

                    que.Image = file.FileName;

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else if (file == null)
            {
                que.Image = "default.png";
            }
            queDM.UpdateQuestion(que);

            Quiz q = new QuizBank().FindQuizByID(que.QuizID);
            return View("EditQuiz", q);

        }

    }
}