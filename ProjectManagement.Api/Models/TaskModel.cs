using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Models
{
    public class TaskModel
    {
        public long ProjectID { get; set; }

        public string Detail { get; set; }

        public TaskStatus Status { get; set; }

        public long? AssignedToUserID { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual UserModel AssignedToUser { get; set; }
    }
}
