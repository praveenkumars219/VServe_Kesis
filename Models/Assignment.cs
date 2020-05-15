using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl.Models
{
    public class Assignment:BaseClass
    {
        public JobDetail AssignedJob { get; set; }
        public JobStatus WorkStatus { get; set; }
        public string AssignedUserId { get; set; }

    }

    public enum JobStatus
    {
        Applied = 0,
        InProgress = 1,
        Completed = 2,  
    }
}
