using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stories.API.Controllers;
using Stories.API.Request;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Controllers
{
    public class StoriesControllerTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public StoriesControllerTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "Stories").Options;
        
        [Fact]
        public async void Get_ReturnnoContent_When_NocontainsElements()
        {
            DataContext context = new(optionsBd);
            context.Database.EnsureDeleted();
            StoriesController controller = new(new StoryService(context));
            Assert.IsType<NoContentResult>( await controller.Get());
            
        }

        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Story.Add( new Story {Title = "The title", Description = "Description",  DepartmentId = 1 });
                context.Story.Add( new Story {Title = "The title two", Description = "Description two",  DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using(DataContext context = new(optionsBd))
            {
                StoriesController controller = new(new StoryService(context));
                var resultTest = Assert.IsType<OkObjectResult>( await controller.Get());
                var stories = resultTest.Value as IEnumerable<StoryDto>;
                Assert.NotEmpty(stories);
                Assert.Equal(2, stories.Count());
            }           
        }

        [Fact]
        public async void Post_ReturnCreated_When_CreateElement()
        {
            using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                StoriesController controller = new(new StoryService(context));
                var storyRequest = new StoryRequest { Title = "The title", Description = "Description", DepartmentId = 1 };
                Assert.IsType<CreatedResult>(await controller.Post(storyRequest));  
            }
        }

        [Fact]
        public async void Delete_ReturnNoContent_When_NoContainElement()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                StoriesController controller = new(new StoryService(context));
                Assert.IsType<NoContentResult>( await controller.Delete(3)); 
            }
        }
        [Fact]
        public async void Delete_ReturnOK_When_DeleteElement()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using (DataContext context = new DataContext(optionsBd))
            {
                StoriesController controller = new(new StoryService(context));
                Assert.IsType<OkResult>(await controller.Delete(2));
            }
        }

        [Fact]
        public async void Put_ReturnOK_When_UpdateElement()
        {
            using (DataContext context = new(optionsBd))
            { 
                context.Database.EnsureDeleted();
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                StoriesController controller = new(new StoryService(context));
                var storyRequest = new StoryRequest { Title = "The title update", Description = "Description update", DepartmentId = 3 };
                Assert.IsType<OkResult>(controller.Put(1, storyRequest));
            }
        }

        [Fact]
        public async void Put_ReturnNoContent_When_NoContainElement()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }

            using (DataContext context = new(optionsBd))
            {
                StoriesController controller = new(new StoryService(context));
                var storyRequest = new StoryRequest { Title = "The title update", Description = "Description update", DepartmentId = 3 };
                Assert.IsType<NoContentResult>(controller.Put(4, storyRequest));
            }
        }

        [Fact]
        public  void GetById_ReturnnoContent_When_NoContainElement()
        {
            DataContext context = new(optionsBd);
            context.Database.EnsureDeleted();
            StoriesController controller = new(new StoryService(context));
            Assert.IsType<NoContentResult>(controller.Get(1));
        }

        [Fact]
        public async void GetById_ReturnOK_When_ContainElement()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                StoriesController controller = new(new StoryService(context));
                var resultTest = Assert.IsType<OkObjectResult>(controller.Get(1));
                Assert.NotNull(resultTest.Value);     
            }
        }
    }
}