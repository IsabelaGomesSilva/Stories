using Microsoft.EntityFrameworkCore;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Services
{
    public class StoryServiceTests
    {
         private readonly DbContextOptions<DataContext> optionsBd;
        public StoryServiceTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "DBSTORIES").Options;
        
        [Fact]
        public async void Get_ReturnNoList_When_NocontainsElements()
        {
           using ( var context = new DataContext(optionsBd))
           {
            var service =  new StoryService(context);
            Assert.Empty(await service.Get());
           }
        }

        [Fact]
        public async void Get_ReturnList_When_ContainsElements()
        {
            using(var context = new DataContext(optionsBd))
            {
                context.Story.Add( new Story {Title = "The title", Description = "Description",  DepartmentId = 1 });
                context.Story.Add( new Story {Title = "The title two", Description = "Description two",  DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using(var context = new DataContext(optionsBd))
            {
                var service = new StoryService(context);
                IEnumerable<StoryDto> storyDtos = await service.Get();
                Assert.NotEmpty(storyDtos);
                Assert.Equal(2, storyDtos.Count());
            }           
        }

        [Fact]
        public async void GetById_ReturnNoContain_When_NocontainElement()
        {
            using (var context = new DataContext(optionsBd))
            {
                var service = new StoryService(context);
                Assert.Null(service.Get(1));
            }
        }

        [Fact]
        public async void GetById_ReturnOk_When_ContainElement()
        {
            using (var context = new DataContext(optionsBd))
            {
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using (var context = new DataContext(optionsBd))
            {
                var service = new StoryService(context);
                var result = Assert.IsType<StoryDto> (service.Get(1));
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void Delete_ReturnFalse_When_NoContainElement()
        {
            using (var context = new DataContext(optionsBd))
            {
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using (var context = new DataContext(optionsBd))
            {
                var service = new StoryService(context);
                Assert.False(await service.Delete(3));
            }
        }
        [Fact]
        public async void Delete_ReturnTrue_When_ContainElementAndDelete()
        {
            using (var context = new DataContext(optionsBd))
            {
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
            }
            using (var context = new DataContext(optionsBd))
            {
                var service = new StoryService(context);
                Assert.True(await service.Delete(1));
            }
        }

    }
}