﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaskContent { get; set; }
        public DateTime Time { get; set; }
        public int CompletedPercentage { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Task()
        {
            Time = DateTime.Now;

        }
    }
}