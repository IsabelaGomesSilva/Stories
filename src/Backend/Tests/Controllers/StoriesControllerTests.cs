using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stories.API.Controllers;
using Stories.API.Request;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;
using Xunit;

namespace Tests.Controllers
{
    public class StoriesControllerTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public StoriesControllerTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "DBSTORIES").Options;
        
        [Fact]
        public async void Get_ReturnnoContent_When_NocontainsElements()
        {
            var context = new DataContext(optionsBd);
            var controller = new StoriesController(new StoryService(context));
            Assert.IsType<NoContentResult>( await controller.Get());
            
        }

        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            using(var context = new DataContext(optionsBd))
            {
                context.Story.Add( new Story {Title = "The title", Description = "Description",  DepartmentId = 1 });
                context.Story.Add( new Story {Title = "The title two", Description = "Description two",  DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using(var context = new DataContext(optionsBd))
            {
                var controller = new StoriesController(new StoryService(context));
                var resultTest = Assert.IsType<OkObjectResult>( await controller.Get());
                var stories = resultTest.Value as IEnumerable<StoryDto>;
                Assert.NotEmpty(stories );
                Assert.Equal(2, stories.Count());
            }           
        }
        [Fact]
        public async void Post_ReturnCreated_When_CreateElement()
        {
            using (var context = new DataContext(optionsBd))
            {
                var controller = new StoriesController(new StoryService(context));
                var storyRequest = new StoryRequest { Title = "The title", Description = "Description", DepartmentId = 1 };

                var resultTest = Assert.IsType<CreatedResult>(await controller.Post(storyRequest));
               
                Assert.NotNull(resultTest);
                
            }
        }
        [Fact]
        public async void Delete_ReturnNoContent_When_NoContainElement()
        {
            using (var context = new DataContext(optionsBd))
            {
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(optionsBd))
            {
                var controller = new StoriesController(new StoryService(context));

                Assert.IsType<NoContentResult>( controller.Delete(3)); 
            }
        }
        [Fact]
        public async void Delete_ReturnOK_When_DeleteElement()
        {
            using (var context = new DataContext(optionsBd))
            {
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(optionsBd))
            {
                var controller = new StoriesController(new StoryService(context));
                var t =  controller.Delete(2);

                Assert.IsType<OkResult>( t);
            }
        }
    }
}