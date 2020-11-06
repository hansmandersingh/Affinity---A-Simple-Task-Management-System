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

        public ActionResult UpdateTaskPercentage(int id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult UpdateTaskPercentage(int id , int percentageVal)
        {
            var task = TaskHelper.getATask(id);
            task.CompletedPercentage = percentageVal;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}