using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public double budgetAmount { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Salary { get; set; }
    }
}