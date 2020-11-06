﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}