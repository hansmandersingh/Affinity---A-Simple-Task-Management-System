using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotificationDetails { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int? TaskId { get; set; }
        public Task Task { get; set; }
        public bool IsWatched { get; set; }
        public bool IsBugNotif { get; set; }
        public bool IsDeadlineNotif { get; set; }
        public bool IsCompletedNotif { get; set; }
        public bool IsTaskNotif { get; set; }
    }
}