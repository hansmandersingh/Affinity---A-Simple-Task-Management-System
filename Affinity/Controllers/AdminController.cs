using Affinity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Affinity.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            //UserManager.GetAllUsers()
            return View();
        }

        public ActionResult AllUsers()
        {
            var AllUsers = UserManager.GetAllUsers();
            return View(AllUsers);
        }

        public ActionResult ShowAllRoles()
        {
            var allRoles = db.Roles.Select(s => s.Name).ToList();

            return View(allRoles);
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(string roleName)
        {
            UserManager.CreateRole(roleName);
            db.SaveChanges();
            return RedirectToAction("ShowAllRoles");
        }

        public ActionResult AddUserToRole()
        {
            ViewBag.UsersList = db.Users.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddUserToRole(string userId, string roleName)
        {
            UserManager.AssignRoleToUser(userId, roleName);
            db.SaveChanges();
            ViewBag.UsersList = db.Users.ToList();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteRole(string roleName)
        {
            UserManager.DeleteRole(roleName);
            db.SaveChanges();
            return RedirectToAction("ShowAllRoles");
        }

        public ActionResult AssignATaskByDeveloper()
        {
            ViewBag.taskList = db.Tasks.ToList();
            ViewBag.UserList = db.Users.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AssignATaskByDeveloper(int taskId, string userId)
        {
            
            if (UserManager.CheckIfUserIsInRole(userId,"developer"))
            {
                TaskHelper.AssignUserATask(taskId, userId);
                db.SaveChanges();
                ViewBag.taskList = db.Tasks.ToList();
                ViewBag.UserList = db.Users.ToList();
                return RedirectToAction("Index");
            } else
            {
                ViewBag.taskList = db.Tasks.ToList();
                ViewBag.UserList = db.Users.ToList();
                return View();
            }
            
        }
    }
}