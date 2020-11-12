using Affinity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Affinity.Controllers
{
    [Authorize(Roles = "project manager")]
    public class ProjectManagerController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProjectManager
        public ActionResult Index()
        {
            ICollection<Project> allProjects;
            allProjects = ProjectHelper.GetAllProjects();

            foreach(var project in allProjects)
            {
                if ((DateTime.Now - project.DeadLine).Days >= 1 && project.Tasks.Any(p => p.IsCompleted == false))
                {
                    Notification notification = new Notification() { ProjectId = project.Id, IsWatched = false, NotificationDetails = "Project is overdue with unfinished tasks.", IsBugNotif = false , IsDeadlineNotif = true };
                    if (!project.Notifications.Any(n => n.IsDeadlineNotif == true && n.ProjectId == project.Id))
                    {
                        project.Notifications.Add(notification);
                        ProjectHelper.UpdateProject(project);
                        db.SaveChanges();
                    }
                }
            }

            ViewBag.NumberOfNotif = db.Notifications.Count(n => n.TaskId == null && n.IsWatched == false);
            return View(allProjects);
        }

        public ActionResult GetAllTasksByProject(int proId, string sort)
        {
            ICollection<Task> allTasks;
            ViewBag.ProjectId = proId;
            switch (sort)
            {
                case "SortAccordingCompletionPercentage":
                    allTasks = TaskHelper.getAllTasks().Where(t => t.ProjectId == proId).OrderByDescending(i => i.CompletedPercentage).ToList();
                    break;
                default:
                    allTasks = TaskHelper.getAllTasks().Where(t => t.ProjectId == proId).OrderByDescending(i => i.Time).ToList();
                    break;
            }
            return View(allTasks);
        }
        

        public ActionResult HideCompletedTask()
        {
            var allTasksWithoutHidden = TaskHelper.getAllTasks();
            return View(allTasksWithoutHidden);
        }
        public ActionResult MarkProjectAsCompleted(int projectId)
        {
            var project = ProjectHelper.GetAProject(projectId);
            Notification notification = new Notification() { IsCompletedNotif = true, ProjectId = projectId, NotificationDetails = "A Project has been completed." };

            project.IsCompleted = true;
            ProjectHelper.UpdateProject(project);
            project.Notifications.Add(notification);
            ProjectHelper.UpdateProject(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MarkNotificationAsWatched(int notifId)
        {
            var notification = db.Notifications.FirstOrDefault(i => i.Id == notifId);

            notification.IsWatched = true;
            db.Entry(notification).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("NotificationsPage");
        }

        public ActionResult NotificationsPage()
        {
            var notifications = db.Notifications.Where(n => n.TaskId == null || n.ProjectId == null).ToList();
            return View(notifications);
        }
    }
}