using Newtonsoft.Json;
using ProjectManagement.Api;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace ProjectManagement.API.IntegrationTests.Controllers
{
    public class ProjectControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProjectControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async System.Threading.Tasks.Task CanGetProjects()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/project");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var project = JsonConvert.DeserializeObject<IEnumerable<Project>>(stringResponse);
            Assert.Contains(project, p => p.Name == "TestProject1");
            Assert.Contains(project, p => p.Detail == "This is Test project 1");
        }


        [Theory]
        [InlineData("/api/project")]
        [InlineData("/api/project/1")]
        public async System.Threading.Tasks.Task GetHttpRequest(string url)
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync(url);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            Assert.Equal("application/json; charset=utf-8", httpResponse.Content.Headers.ContentType.ToString());
           
        }



        [Fact]
        public async System.Threading.Tasks.Task CanGetProjects_Negative_testCase()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/Projects");
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }


        [Fact]
        public async System.Threading.Tasks.Task CanGetProjectById()
        {

            var postRequest = new
            {
                Url = "/api/Project",
                Body = new
                {
                    Name="Test1Pr",
                    Detail = "TestGetByID",
                    CreatedOn = DateTime.Today
                }
            };

            // Act
            var postResponse = await _client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<Project>(jsonFromPostResponse);

            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync(string.Format("/api/Project/{0}", singleResponse.ID));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var project = JsonConvert.DeserializeObject<Project>(stringResponse);
            Assert.Equal("TestGetByID", project.Detail);
        }


        [Fact]
        public async System.Threading.Tasks.Task CanGetTaskById_NegativetestCase()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/Project/45");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var project = JsonConvert.DeserializeObject<Project>(stringResponse);
            Assert.Null(project);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDeleteProjectsync()
        {
            // Arrange

            var postRequest = new
            {
                Url = "/api/Project",
                Body = new
                {
                    Detail = "Testrergergergr",
                    CreatedOn = DateTime.Today,
                    Name = "Test1Pr1"
                }
            };

            // Act
            var postResponse = await _client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<Project>(jsonFromPostResponse);

            var deleteResponse = await _client.DeleteAsync(string.Format("/api/Project/{0}", singleResponse.ID));
            // var deleteResponse = await _client.DeleteAsync(string.Format("/api/Project/{0}", postRequest.Body.ID));
            // Assert
            postResponse.EnsureSuccessStatusCode();



            deleteResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDeleteProjectsync_NegativetestCase()
        {

            var deleteResponse = await _client.DeleteAsync(string.Format("/api/Project/{0}", 0));

            Assert.Equal(HttpStatusCode.BadRequest, deleteResponse.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPostProjectAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Project",
                Body = new
                {
                    Detail = "Testrergergergr",
                    CreatedOn = DateTime.Today,
                    Name = "Test1Pr2",

                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));


            // Assert
            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async System.Threading.Tasks.Task TestPostProjectAsync_Negative()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Project",
                Body = new
                {
                    ID=3,
                    Detail = "Testrergergergr",
                    CreatedOn = DateTime.Today,
                    Name = "Test1Pr1",

                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));


            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        }

        [Fact]
        public async System.Threading.Tasks.Task TestPutProjectAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Project",
                Body = new
                {
                    Detail = "Testrergergergr",
                    CreatedOn = DateTime.Today,
                    Name = "Test1Pr6",
                }
            };

            // Act
            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPutProjectAsync_BadRequestError()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Project",
                Body = new
                {
                    Id = "fgfgf"
                }
            };

            // Act
            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
