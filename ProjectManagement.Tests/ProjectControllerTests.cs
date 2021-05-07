using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagement.Api.Controllers;
using ProjectManagement.Data.Implementation;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectManagement.API.UnitTests
{

    public class ProjectControllerFixture
    {
      public  ProjectController projectController => new ProjectController();
    }
    public  class ProjectControllerTests:IClassFixture<ProjectControllerFixture>
    {

       readonly ProjectControllerFixture _projectControllerFixture;
     public   ProjectControllerTests(ProjectControllerFixture projectControllerFixture)
        {
          
            _projectControllerFixture = projectControllerFixture;
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {

            
        }
    }
}
