using Affinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affinity.Controllers
{
    [Authorize(Roles = "project manager")]
    public class ProjectManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProjectManager
        public ActionResult Index()
        {
            return View();
        }
    }
}