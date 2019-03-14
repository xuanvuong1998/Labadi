using Project_Try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Try1.Controllers
{
    public class RegisterController : Controller {
        // GET: Register
        public ActionResult Register() {
            return View();
        }

        AccountData ac = new AccountData();

        [HttpPost]
        public RedirectToRouteResult AddNewAccount(FormCollection frmcol) {
            string fullname = frmcol["first_name"] + " " + frmcol["last_name"];
            string username = frmcol["username"];
            string password = frmcol["password"];
            string phone = frmcol["phone"];
            string email = frmcol["email"];
            if (ac.AddNewAccount(username, password, fullname, phone, email)) {
                ViewBag.Message = "User " + username + " create account successfully";
                ViewBag.Title = "Homeeeee";
            } else {
                ViewBag.Message = "User " + username + " create account fail";
                ViewBag.UserName = username;
                ViewBag.Title = "Homeeeee";
            }

            return RedirectToAction("Index", "Login");
        }

        public JsonResult CheckUserNameAvailability(string userdata) {            
            var isExisted = ac.checkDuplicateAccount(userdata);
            if (isExisted) {
                return Json(1);
            } else {
                return Json(0);
            }
        }
        public JsonResult CheckUserEmailAvailability(string userdata) {            
            var isExisted = ac.checkDuplicateEmail(userdata);
            if (isExisted) {
                return Json(1);
            } else {
                return Json(0);
            }
        }
    }
}