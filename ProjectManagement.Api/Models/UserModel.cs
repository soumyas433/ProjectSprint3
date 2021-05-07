using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Models
{
    public class UserModel 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
