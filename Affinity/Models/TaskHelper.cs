using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public class TaskHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void createTask(string Name, string UserId)
        {
            Task task = new Task() { Name = Name, UserId = UserId };
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void updateTask(Task t)
        {
            Task task = db.Tasks.Find(t.Id);

            if(task != null)
            {
                task.Name = t.Name;
                task.User = t.User;
                task.UserId = t.UserId;
                task.Project = t.Project;
                task.ProjectId = t.ProjectId;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void deleteTask(int id)
        {
            Task task = db.Tasks.Find(id);

            if(task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
        }

        public List<Task> getAllTasks()
        {
            return db.Tasks.ToList();
        }

        public Task getATask(int id)
        {
            return db.Tasks.Find(id);
        }
    }
}