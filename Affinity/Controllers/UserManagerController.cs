using Affinity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affinity.Controllers
{
    public class UserManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: UserManager
        public ActionResult Index()
        {
            return View();
        }

        public void AddUser(string email, string pwdHash)
        {
            
        }
    }
}