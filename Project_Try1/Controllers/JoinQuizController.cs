using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Try1.Controllers
{
    public class JoinQuizController : Controller
    {
        // GET: JoinQuiz
        public ActionResult Join()
        {
            return View("Join");
        }

        public ActionResult JoinSuccess() {
            return View("WaittingGame");
        }
    }
}