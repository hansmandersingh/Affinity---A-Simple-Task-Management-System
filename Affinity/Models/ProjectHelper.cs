using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    
    public static class ProjectHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();


        public static void CreateProject(string Name, string Description)
        {
            Project project = new Project() { Name = Name, Description = Description };

            db.Projects.Add(project);
            db.SaveChanges();
        }

        public static void UpdateProject(Project pro)
        {
            Project project = db.Projects.Find(pro.Id);

            if (project != null)
            {
                project.Name = pro.Name;
                project.Description = pro.Description;
                project.Tasks = pro.Tasks;
                project.Notifications = pro.Notifications;
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);

            if(project != null)
            {
                db.Projects.Remove(project);
                db.SaveChanges();
            }
        }
        public static List<Project> GetAllProjects()
        {
            return db.Projects.ToList();
        }

        public static Project GetAProject(int id)
        {
            return db.Projects.Find(id);
        }
    }
}