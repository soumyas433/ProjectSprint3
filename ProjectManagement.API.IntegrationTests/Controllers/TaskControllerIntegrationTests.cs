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


    public class TaskControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TaskControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/task")]
        [InlineData("/api/task/1")]
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
        public async System.Threading.Tasks.Task CanGetTasks()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/Task");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
        [Fact]
        public async System.Threading.Tasks.Task CanGetTasks_TestValues()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/Task");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<IEnumerable<Task>>(stringResponse);
            Assert.Contains(task, p => p.Detail == "Create Database Table");
        }

        [Fact]
        public async System.Threading.Tasks.Task CanGetTasks_Negative_testCase()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/Tasks");
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }


        [Fact]
        public async System.Threading.Tasks.Task CanGetTaskById()
        {

            var postRequest = new
            {
                Url = "/api/Task",
                Body = new
                {
                    Detail = "TestGetByID",
                    CreatedOn = DateTime.Today
                }
            };

            // Act
            var postResponse = await _client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<Task>(jsonFromPostResponse);

            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync(string.Format("/api/Task/{0}", singleResponse.ID));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<Task>(stringResponse);
            Assert.Equal("TestGetByID", task.Detail);
        }


        [Fact]
        public async System.Threading.Tasks.Task CanGetTaskById_NegativetestCase()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/Task/45");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var Task = JsonConvert.DeserializeObject<Task>(stringResponse);
            Assert.Null(Task);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDeleteTasksync()
        {
            // Arrange

            var postRequest = new
            {
                Url = "/api/Task",
                Body = new
                {
                    Detail = "Testrergergergr",
                    CreatedOn = DateTime.Today
                }
            };

            // Act
            var postResponse = await _client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
             var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<Task>(jsonFromPostResponse);

            var deleteResponse = await _client.DeleteAsync(string.Format("/api/Task/{0}", singleResponse.ID));
           // var deleteResponse = await _client.DeleteAsync(string.Format("/api/Task/{0}", postRequest.Body.ID));
            // Assert
              postResponse.EnsureSuccessStatusCode();



            deleteResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDeleteTasksync_NegativetestCase()
        {

            var deleteResponse = await _client.DeleteAsync(string.Format("/api/Task/{0}", 0));

            Assert.Equal(HttpStatusCode.BadRequest, deleteResponse.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPostTaskAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Task",
                Body = new
                {
                    // ID = 6,
                    Detail = "Testrergergergr",
                    CreatedOn = DateTime.Today
                    

                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));


            // Assert
            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async System.Threading.Tasks.Task TestPostTaskAsync_Negative()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Task",
                Body = new
                {
                    ID = 3

                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));


            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        }

        [Fact]
        public async System.Threading.Tasks.Task TestPutTaskAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Task",
                Body = new
                {
                    Detail = "TestrergergergrPut",
                    CreatedOn = DateTime.Today
                }
            };

            // Act
            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPutTaskAsync_BadRequestError()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Task",
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
