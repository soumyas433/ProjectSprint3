using Newtonsoft.Json;
using ProjectManagement.Api;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace ProjectManagement.API.IntegrationTests.Controllers
{
    public class UserControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UserControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/user")]
        [InlineData("/api/user/1")]
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
        public async System.Threading.Tasks.Task CanGetUsers()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/user");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResponse);
            Assert.Contains(users, p => p.FirstName == "Test");
            Assert.Contains(users, p => p.LastName == "User1");
        }

        [Fact]
        public async System.Threading.Tasks.Task CanGetUsers_Negative_testCase()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/users");    
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }


        [Fact]
        public async System.Threading.Tasks.Task CanGetUserById()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/User/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);
            Assert.Equal(1, user.ID);
            Assert.Equal("User1", user.LastName);
        }

        [Fact]
        public async System.Threading.Tasks.Task CanGetUserById_NegativetestCase()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/User/45");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);
            Assert.Null(user);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDeleteUsersync()
        {
            // Arrange

            var postRequest = new
            {
                Url = "/api/User",
                Body = new
                {
                    FirstName = "Test",
                    LastName = "Tes",
                    Email="delete@gmail.com"
                }
            };

            // Act
            var postResponse = await _client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<User>(jsonFromPostResponse);

            var deleteResponse = await _client.DeleteAsync(string.Format("/api/User/{0}", singleResponse.ID));
           // var deleteResponse = await _client.DeleteAsync(string.Format("/api/User/{0}", postRequest.Body.ID));
            // Assert
           postResponse.EnsureSuccessStatusCode();

            

             deleteResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDeleteUsersync_NegativetestCase()
        {
            
            var deleteResponse = await _client.DeleteAsync(string.Format("/api/User/{0}", 0));

            Assert.Equal(HttpStatusCode.BadRequest, deleteResponse.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPostUserAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/user",
                Body = new
                {
                   // ID = 6,
                    FirstName = "Test",
                    LastName = "Tes"
                    
                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
           

            // Assert
            response.EnsureSuccessStatusCode();
           
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPostUserAsync_Negative()
        {
            // Arrange
            var request = new
            {
                Url = "/api/user",
                Body = new
                {
                    ID = 3,
                    FirstName = "Test",
                    LastName = "Tes"

                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));


            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        }

        [Fact]
        public async System.Threading.Tasks.Task TestPutUserAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/user",
                Body = new
                {
                    ID = 1,
                    FirstName = "Test",                   
                    Email="test@gmail.com"
                }
            };

            // Act
            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async System.Threading.Tasks.Task TestPutUserAsync_BadRequestError()
        {
            // Arrange
            var request = new
            {
                Url = "/api/user",
                Body = new
                {
                    Id="fgfgf"
                }
            };

            // Act
            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
        }
    }
