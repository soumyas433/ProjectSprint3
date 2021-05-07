using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;
using System;

namespace ProjectManagement.Data.Implementation
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext(DbContextOptions options) : base(options)
        {

        }

        public void SeedInitialData()
        {
            User testUser1 = new User
            {
                FirstName = "Test",
                LastName = "User1",
                Email = "testuser1@test.com",
            };
            User.Add(testUser1);
            User testUser2 = new User
            {
                FirstName = "Test",
                LastName = "User2",
                Email = "testuser2@gmail.com"
            };
            User.Add(testUser2);

            User testUser3 = new User
            {
                FirstName = "Test",
                LastName = "User3",
                Email = "testuser2@gmail.com"
            };
            User.Add(testUser3);
            User testUser4 = new User
            {
                FirstName = "Test",
                LastName = "User4",
                Email = "testuser2@gmail.com"
            };
            User.Add(testUser4);
            User testUser5 = new User
            {
                FirstName = "Test",
                LastName = "User5",
                Email = "testuser2@gmail.com"
            };
            User.Add(testUser5);


            Project testProject1 = new Project { Name = "TestProject1", CreatedOn = DateTime.Now, Detail = "This is Test project 1" };
            Project testProject2 = new Project { Name = "TestProject2", CreatedOn = DateTime.Now, Detail = "This is Test project 2" };

            Project testProject3 = new Project { Name = "TestProject3", CreatedOn = DateTime.Now, Detail = "This is Test project 3" };
            Project testProject4 = new Project { Name = "TestProject4", CreatedOn = DateTime.Now, Detail = "This is Test project 4" };

            Project testProject5 = new Project { Name = "TestProject5", CreatedOn = DateTime.Now, Detail = "This is Test project 5" };

            Project.Add(testProject1);
            Project.Add(testProject2);
            Project.Add(testProject3);
            Project.Add(testProject4);
            Project.Add(testProject5);

            Task task1 = new Task { Project = testProject1 , AssignedToUser = testUser1, Status = Entities.Enums.TaskStatus.New, CreatedOn = DateTime.Today, Detail = "Create Database Table", ProjectID = testProject1.ID, AssignedToUserID = testUser1.ID };
            Task task2 = new Task { Project = testProject2 , AssignedToUser = testUser2, Status = Entities.Enums.TaskStatus.New, CreatedOn = DateTime.Today, Detail = "Create Database Table2", ProjectID = testProject2.ID, AssignedToUserID = testUser2.ID };
            Task task3 = new Task { Project = testProject3 , AssignedToUser = testUser3, Status = Entities.Enums.TaskStatus.New, CreatedOn = DateTime.Today, Detail = "Create Database Tabl3", ProjectID = testProject3.ID, AssignedToUserID = testUser3.ID };
            Task task4 = new Task { Project = testProject4 , AssignedToUser = testUser4, Status = Entities.Enums.TaskStatus.New, CreatedOn = DateTime.Today, Detail = "Create Database Tabl4", ProjectID = testProject4.ID, AssignedToUserID = testUser4.ID };
            Task task5 = new Task { Project = testProject5 , AssignedToUser = testUser5, Status = Entities.Enums.TaskStatus.New, CreatedOn = DateTime.Today, Detail = "Create Database Tabl5", ProjectID = testProject5.ID, AssignedToUserID = testUser5.ID };

            Task.Add(task1);
            Task.Add(task2);
            Task.Add(task3);
            Task.Add(task4);
            Task.Add(task5);
            this.SaveChanges();
        }

        public DbSet<User> User { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Task> Task { get; set; }
    }
}
