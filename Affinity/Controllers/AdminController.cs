using Affinity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affinity.Controllers
{
    [Authorize]
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
    }
}