using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using adminsys.Models;

namespace adminsys.Controllers
{
    public class HomeController : Controller
    {
        orangedev_AppDBEntities user = new orangedev_AppDBEntities();
        orangedev_adminsysEntities admin = new orangedev_adminsysEntities();
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                return View(user.TBLUserInfoes.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLAdminInfo tBLAdminInfo)
        {
            var validate = admin.TBLAdminInfoes.Where(x => x.Username.Equals(tBLAdminInfo.Username) && x.Email.Equals(tBLAdminInfo.Email) && x.Password.Equals(tBLAdminInfo.Password)).FirstOrDefault();
            if (validate != null)
            {
                Session["Id"] = tBLAdminInfo.Id.ToString();
                Session["Username"] = tBLAdminInfo.Username.ToString();
                Session["Email"] = tBLAdminInfo.Email.ToString();
                Session["Password"] = tBLAdminInfo.Password.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Invalid Username , Email or Password!";
                
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}