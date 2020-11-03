using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    
    public class ProjectHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public void CreateProject(string Name, string Description)
        {
            Project project = new Project() { Name = Name, Description = Description };

            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void UpdateProject(Project pro)
        {
            Project project = db.Projects.Find(pro.Id);

            if (project != null)
            {
                project.Name = pro.Name;
                project.Description = pro.Description;
                project.Tasks = pro.Tasks;
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);

            if(project != null)
            {
                db.Projects.Remove(project);
                db.SaveChanges();
            }
        }
        public List<Project> GetAllProjects()
        {
            return db.Projects.ToList();
        }

        public Project GetAProject(int id)
        {
            return db.Projects.Find(id);
        }
    }
}