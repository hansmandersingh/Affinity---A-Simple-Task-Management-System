using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affinity.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public double budget { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }  
        public int Salary { get; set; }
    }
}