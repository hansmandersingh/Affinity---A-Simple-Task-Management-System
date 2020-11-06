using Affinity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affinity.Controllers
{
    [Authorize(Roles = "developer")]
    public class DeveloperController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Developer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAllTasks()
        {
            var allTasks = TaskHelper.GetAllTasksByADeveloper(this.User.Identity.GetUserId());
            return View(allTasks);
        }

        public ActionResult UpdateTask()
        {
            return View();
        }
        
    }
}