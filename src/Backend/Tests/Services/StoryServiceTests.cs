using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;
using Xunit;

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
    }
}