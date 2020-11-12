using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaskContent { get; set; }
        public DateTime Time { get; set; }
        [Range(0, 100, ErrorMessage ="Percentage must be between 0 and 100")]
        public int CompletedPercentage { get; set; }
        public bool IsCompleted { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Comment> Notes { get; set; }
        public Priority Priority { get; set; }
        public DateTime DeadLine { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public Task()
        {
            Notes = new HashSet<Comment>();
            Notifications = new HashSet<Notification>();
            Time = DateTime.Now;
            CompletedPercentage = 0;
        }
    }
}