using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public Priority Priority { get; set; }
        public DateTime DeadLine { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public Project()
        {
            Time = DateTime.Now;
            Tasks = new HashSet<Task>();
            Notifications = new HashSet<Notification>();
        }
    }
}