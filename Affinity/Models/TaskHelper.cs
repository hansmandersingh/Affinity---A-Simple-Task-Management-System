using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public static class TaskHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static void createTask(string Name, string UserId)
        {
            Task task = new Task() { Name = Name, UserId = UserId };
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public static void updateTask(Task t)
        {
            Task task = db.Tasks.Find(t.Id);

            if(task != null)
            {
                task.Name = t.Name;
                task.User = t.User;
                task.UserId = t.UserId;
                task.Project = t.Project;
                task.ProjectId = t.ProjectId;
                task.DeadLine = t.DeadLine;
                task.IsCompleted = t.IsCompleted;
                task.CompletedPercentage = t.CompletedPercentage;
                task.Notes = t.Notes;
                task.Notifications = t.Notifications;
                task.Priority = t.Priority;
                task.Time = t.Time;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void deleteTask(int id)
        {
            Task task = db.Tasks.Find(id);

            if(task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
        }

        public static List<Task> getAllTasks()
        {
            return db.Tasks.Include(u => u.User).ToList();
        }

        public static List<Task> GetAllTasksByADeveloper(string userId)
        {
            var allTasks = db.Tasks.Where(i => i.User.Id == userId).Include(n => n.Notifications).ToList();

            return allTasks;
        }

        public static Task getATask(int id)
        {
            return db.Tasks.Find(id);
        }

        public static void AssignUserATask(int taskId, string userId)
        {
            Task task = db.Tasks.Find(taskId);

            if (task != null)
            {
                task.UserId = userId;
                db.SaveChanges();
            }
        }

        public static void UpdateTaskPercentage(int taskId, int percentageVal)
        {
            Task task = db.Tasks.Find(taskId);

            if (task != null)
            {
                task.CompletedPercentage = percentageVal;
                db.SaveChanges();
            }
        }
    }
}