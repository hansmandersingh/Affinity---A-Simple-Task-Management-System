using Affinity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    if (!task.Notifications.Any(s => s.TaskId == task.Id)) //fix this
                    {
                        Notification notification = new Notification()
                        {
                            TaskId = task.Id,
                            Task = task,
                            NotificationDetails = "Heads up you are about to be at your task deadline.",
                            ProjectId = task.ProjectId,
                            IsDeadlineNotif = true,
                        };

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
            Notification notification = new Notification() { IsCompletedNotif = true, TaskId = taskId, ProjectId = task.ProjectId , NotificationDetails = "A task has been completed from a project." };
            var project = ProjectHelper.GetAProject(task.ProjectId);

            
            task.IsCompleted = IsComp;
            TaskHelper.updateTask(task);
            project.Notifications.Add(notification);
            ProjectHelper.UpdateProject(project);
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
            Comment comment = new Comment() { Note = commentText, TaskId = task.Id, UserId = this.User.Identity.GetUserId() , IsBugNote = false };
            db.Comments.Add(comment);
            db.SaveChanges();
            ViewBag.TaskId = id;

            return RedirectToAction("Index");
        }

        public ActionResult MarkNotificationAsWatched(int notificationId, int taskId)
        {
            var task = TaskHelper.getATask(taskId);

            task.Notifications.FirstOrDefault(n => n.Id == notificationId).IsWatched = true;
            TaskHelper.updateTask(task);
            db.SaveChanges();
            return RedirectToAction("Details", "Notifications", new { id = notificationId });
        }

        public ActionResult AddANote(int taskId)
        {
            ViewBag.taskId = taskId;
            return View();
        }

        [HttpPost]
        public ActionResult AddANote(int taskId, string Note)
        {
            Comment comment = new Comment() { Note = Note, TaskId = taskId, UserId = this.User.Identity.GetUserId(), IsBugNote = true };
            var task = TaskHelper.getATask(taskId);
            var project = ProjectHelper.GetAProject(task.ProjectId);
            Notification notification = new Notification() { NotificationDetails = Note, ProjectId = project.Id, TaskId = taskId, IsWatched = false, IsBugNotif = true };

            task.Notes.Add(comment);
            TaskHelper.updateTask(task);
            project.Notifications.Add(notification);
            ProjectHelper.UpdateProject(project);
            db.SaveChanges();
            ViewBag.taskId = taskId;
            return RedirectToAction("Index", "Developer");
         }

    }
}