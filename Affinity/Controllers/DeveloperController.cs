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
    [Authorize(Roles = "developer")]
    public class DeveloperController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Developer
        public ActionResult Index(string sortingOrder)
        {
            ICollection<Task> allTasks;

            switch(sortingOrder)
            {
                case "HighToLow":
                    allTasks = TaskHelper.GetAllTasksByADeveloper(User.Identity.GetUserId()).OrderByDescending(a => a.Priority).ToList();
                    break;
                case "LowToHigh":
                    allTasks = TaskHelper.GetAllTasksByADeveloper(this.User.Identity.GetUserId()).OrderBy(o => o.Priority).ToList();
                    break;
                default:
                    allTasks = TaskHelper.GetAllTasksByADeveloper(this.User.Identity.GetUserId());
                    break;
            }

            foreach(var task in allTasks)
            {
                if ((DateTime.Now - task.DeadLine).Days <= 1)
                {
                    Notification notification = new Notification()
                    {
                        TaskId = task.Id,
                        Task = task,
                        ProjectId = task.ProjectId, 
                        Project = task.Project,
                        NotificationDetails = "Heads up you are about to be at your task deadline." 
                    };

                    if (!task.Notifications.Any(s => s.TaskId == notification.TaskId && s.ProjectId == notification.ProjectId))
                    {
                        task.Notifications.Add(notification);
                        TaskHelper.updateTask(task);
                        db.SaveChanges();
                    }
                }
            }
            
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
            TaskHelper.updateTask(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MarkTaskAsCompleted(int taskId , bool IsComp)
        {
            var task = TaskHelper.getATask(taskId);
            task.IsCompleted = IsComp;
            TaskHelper.updateTask(task);
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

        public ActionResult MarkNotificationAsWatched(int notificationId)
        {
            return RedirectToAction("Details", "Notifications", new { id = notificationId });
        }
    }
}