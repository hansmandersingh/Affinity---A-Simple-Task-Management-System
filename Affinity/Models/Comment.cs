using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}