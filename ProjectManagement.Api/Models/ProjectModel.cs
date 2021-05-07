using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public string Detail { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
