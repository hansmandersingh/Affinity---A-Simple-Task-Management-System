﻿using Affinity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private static ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        // GET: UserManager
        public ActionResult Index()
        {
            return View();
        }

        public bool AddUser(string Email, string pwdHash)// requires Email and pwd in hash format to work.
        {
            
            var user = new ApplicationUser { UserName = Email, Email = Email };

            var result = UserManager.Create(user, pwdHash);

            if(result.Succeeded)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool DeleteUser(string UserId)
        {
            var user = db.Users.Find(UserId);

            if (user != null)
            {
                var result = UserManager.Delete(user);

                if(result.Succeeded)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }

        public void UpdateUser()
        {

        }
    }
}