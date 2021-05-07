using Microsoft.AspNetCore.Hosting;
using ProjectManagement.Api;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Data.Implementation;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;

namespace ProjectManagement.API.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                services.AddDbContext<ProjectManagementContext>(options =>
                {
                    options.UseInMemoryDatabase("ProjectManagement");
                    options.UseInternalServiceProvider(serviceProvider);
                });
               
                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<ProjectManagementContext>();
                   
                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        appDb.SeedInitialData();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            });
        }
            private IServiceCollection RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
            services.AddTransient<IBaseRepository<Project>, BaseRepository<Project>>();
            services.AddTransient<IBaseRepository<Entities.Task>, BaseRepository<Entities.Task>>();

            return services;

        }
    }

   
}

