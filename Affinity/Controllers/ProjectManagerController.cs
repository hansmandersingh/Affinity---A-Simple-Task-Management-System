using Affinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

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
            return View(allProjects);
        }

        public ActionResult GetAllTasksByProject(int proId)
        {
            ICollection<Task> allTasks;
            allTasks = TaskHelper.getAllTasks();
            var allTasksinDesc = allTasks.Where(t => t.ProjectId == proId).OrderByDescending(i => i.CompletedPercentage).ToList();
            return View(allTasksinDesc);
        }

        public ActionResult HideCompletedTask()
        {
            var allTasksWithoutHidden = TaskHelper.getAllTasks();
            return View(allTasksWithoutHidden);
        }
    }
}