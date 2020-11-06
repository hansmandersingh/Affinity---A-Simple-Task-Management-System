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
            ViewBag.id = id;
            return View(id);
        }

        [HttpPost]
        public ActionResult UpdateTaskPercentage(int id , int percentageVal)
        {
            ViewBag.id = id;
            var task = TaskHelper.getATask(id);
            task.CompletedPercentage = percentageVal;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult MarkTaskAsCompleted(int taskId , bool IsComp)
        {
            var task = TaskHelper.getATask(taskId);
            task.IsCompleted = IsComp;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddComment(int id)
        {
            ViewBag.TaskId = id;
            var task = TaskHelper.getATask(id);
            return View(task);
        }

        [HttpPost]
        public ActionResult AddComment(int id, string commentText)
        {
            var task = TaskHelper.getATask(id);
            Comment comment = new Comment() { Note = commentText, TaskId = task.Id, UserId = this.User.Identity.GetUserId() };
            db.Comments.Add(comment);
            db.SaveChanges();
            ViewBag.TaskId = id;

            return RedirectToAction("Index");
        }
    }
}