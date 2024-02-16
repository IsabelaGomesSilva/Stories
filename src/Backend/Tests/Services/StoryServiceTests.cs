using Microsoft.EntityFrameworkCore;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Services
{
    public class StoryServiceTests 
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public StoryServiceTests() => optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "STORIES").Options;

        [Fact]
        public async void Get_ReturnNoList_When_NocontainsElements()
        {
           
            using (DataContext context = new(optionsBd)) {
                context.Database.EnsureDeleted();
                Assert.Empty(await new StoryService(context).Get());
            }
        }

        [Fact]
        public async void Get_ReturnList_When_ContainsElements()
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
                IEnumerable<StoryDto> storyDtos = await new StoryService(context).Get();
                Assert.NotEmpty(storyDtos);
                Assert.Equal(2, storyDtos.Count());
            }           
        }

        [Fact]
        public async void GetById_ReturnNoContain_When_NocontainElement()
        {
            using DataContext context = new(optionsBd);
            context.Database.EnsureDeleted();
            Assert.Null(new StoryService(context).Get(1));
        }

        [Fact]
        public async void GetById_ReturnOk_When_ContainElement()
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
                var result = Assert.IsType<StoryDto> (new StoryService(context).Get(1));
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void Delete_ReturnFalse_When_NoContainElement()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 2 });
                await context.SaveChangesAsync();
                
                Assert.False(await new StoryService(context).Delete(3));
            }
        }
        [Fact]
        public async void Delete_ReturnTrue_When_ContainElementAndDelete()
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
                Assert.True(await new StoryService(context).Delete(1));
            }
        }
        [Fact]
        public async void Add_ReturnTrue_When_add()
        {
            using (var context = new DataContext(optionsBd))
            {
                StoryDto storyDto = new (){ Title = "The title two", Description = "Description two", DepartmentId = 2 };
                var (isCreated, idCreated) = await new StoryService(context).Add(storyDto);
                Assert.True(isCreated);
                Assert.NotEqual(0,idCreated);
            }
        }

        //[Fact]
        //public async void Add_ReturnFalse_When_NotAdd()
        //{
        //    using (var context = new DataContext(optionsBd))
        //    {
        //        var storyDto = new StoryDto {Title= "", Description = "Description two", DepartmentId = 2 };
        //        var (isCreated, idCreated) = await new StoryService(context).Add(storyDto);
        //        Assert.False(isCreated);
        //        Assert.Equal(0, idCreated);
        //    }
        //}


    }
}