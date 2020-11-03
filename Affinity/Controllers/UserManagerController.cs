using Affinity.Models;
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
        private RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
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

        public bool UpdateUser(ApplicationUser user)
        {
            var userToUpdate = UserManager.FindById(user.Id);

            if(userToUpdate != null)
            {
                userToUpdate.UserName = user.UserName;
                userToUpdate.Email = user.Email;
                userToUpdate.PhoneNumber = user.PhoneNumber;
                userToUpdate.PasswordHash = user.PasswordHash;
            }

            var result = UserManager.Update(userToUpdate);

            if(result.Succeeded)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public List<string> GetAllRolesForUser(string userId)
        {
            return UserManager.GetRoles(userId).ToList();
        }
        public bool AssignRoleToUser(string userId, string roleName)
        {
            var result = UserManager.AddToRole(userId, roleName);

            if (result.Succeeded)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public bool DeleteUserFromRole(string userId, string roleName)
        {
            roleName = roleName.ToLower();
            if (RoleManager.RoleExists(roleName) && UserManager.FindById(userId) != null)
            {
                if (UserManager.IsInRole(userId, roleName))
                {
                    var result = UserManager.RemoveFromRole(userId, roleName);

                    if (result.Succeeded)
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
            } else
            {
                return false;
            }
        }
        public bool CheckIfUserIsInRole(string userId, string roleName)
        {
            roleName = roleName.ToLower();
            return UserManager.IsInRole(userId, roleName);
        }

        public bool CreateRole(string roleName)
        {
            if(RoleManager.RoleExists(roleName))
            {
                return true;
            } else
            {
                var result = RoleManager.Create(new IdentityRole(roleName));

                if (result.Succeeded)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        public bool DeleteRole(string roleName)
        {
            if (RoleManager.RoleExists(roleName))
            {
                var users = RoleManager.FindByName(roleName).Users.Select(u => u.UserId).ToList();

                users.ForEach(userId => { UserManager.RemoveFromRole(userId, roleName); });

                var result = RoleManager.Delete(RoleManager.FindByName(roleName));

                if (result.Succeeded)
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
    }
}