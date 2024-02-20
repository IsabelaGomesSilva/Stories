using Microsoft.AspNetCore.Mvc;
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
                                                     .UseInMemoryDatabase(databaseName: "ServiceStory").Options;

        [Fact]
        public async void Get_ReturnNoList_When_NocontainsElements()
        {
            using (DataContext context = new(optionsBd))
            {
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
                context.Department.Add(new Department{Name = "Financeiro"});
                await context.SaveChangesAsync();
            }
            using(DataContext context = new(optionsBd))
            {
                context.Story.Add( new Story {Title = "The title", Description = "Description",  DepartmentId = 1 });
                context.Story.Add( new Story {Title = "The title two", Description = "Description two",  DepartmentId = 1 });
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
        public  void GetById_ReturnNoContain_When_NocontainElement()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                Assert.Null(new  StoryService(context).Get(1));
            }
        }
        [Fact]
        public async void GetById_ReturnOk_When_ContainElement()
        {
            using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Department.Add(new Department{Name = "Financeiro"});
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 1 });
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd)) 
            {
                Assert.NotNull(Assert.IsType<StoryDto>(new StoryService(context).Get(1)));
            }
        }
        [Fact]
        public async void Delete_ReturnFalse_When_NoContainElement()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                Assert.False(await new StoryService(context).Delete(3));
            }
        }
        [Fact]
        public async void Delete_ReturnTrue_When_ContainElementAndDelete()
        {
            using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Department.Add(new Department{Name = "Financeiro"});
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.Story.Add(new Story { Title = "The title two", Description = "Description two", DepartmentId = 1 });
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
             using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Department.Add(new Department{Name = "Financeiro"});
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                StoryDto storyDto = new (){ Title = "The title two", Description = "Description two", DepartmentId = 1 };
                var (isCreated, idCreated) = await new StoryService(context).Add(storyDto);
                Assert.True(isCreated);
                Assert.NotEqual(0,idCreated);
            }
        }
        [Fact]
        public async void Upadate_ReturnTrue_When_UpdateElement()
        {
             using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Department.Add(new Department{Name = "Financeiro"});
                await context.SaveChangesAsync();
            }                                         
            using (DataContext context = new(optionsBd))
            {
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                await context.SaveChangesAsync();
            }   
            using (DataContext context = new(optionsBd))
            {
                StoryDto story = new() {  Id = 1, Title = "The title update", 
                                          Description = "Description update", DepartmentId = 1 };
                Assert.True(await new StoryService(context).Update(story));
            }
        }
        [Fact]
        public async void Upadate_ReturnFalse_When_NoContainElement()
        {
            using (DataContext context = new(optionsBd))
            {
                StoryDto storyDto = new(){Id = 50, Title = "The title update",
                                          Description = "Description update", DepartmentId = 2};
                Assert.False(await new StoryService(context).Update(storyDto));
            }
        }
        [Fact]
        public async void AddVotes_ReturnTrue_When_add()
        {
            using (DataContext context = new(optionsBd) )
            {
                context.Database.EnsureDeleted();  
                context.Department.Add(new Department{Name = "Financeiro"});
                  await context.SaveChangesAsync(); 
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.User.Add(new User{Name = "Carlos Silva"});
                  await context.SaveChangesAsync();

            }
            using (DataContext context = new(optionsBd))
            {
                VoteDto voteDto = new() { StoryId = 1, UserId = 1, Voted = false };
                var (isCreated, idCreated) = await new StoryService(context).AddVoted(voteDto);
                Assert.True(isCreated);
                Assert.NotEqual(0, idCreated);
            }
        }
        [Fact]
        public async void GetVotes_ReturnVotes_When_ContainElements()
        {
             using (DataContext context = new(optionsBd) )
            {
                context.Database.EnsureDeleted();  
                context.Department.Add(new Department{Name = "Financeiro"});
                  await context.SaveChangesAsync(); 
                context.Story.Add(new Story { Title = "The title", Description = "Description", DepartmentId = 1 });
                context.User.Add(new User{Name = "João Silva"});
                  await context.SaveChangesAsync();

            }
            using (DataContext context = new(optionsBd))
            {
                context.Vote.Add(new Vote { StoryId = 1, UserId = 1, Voted = false });
                context.Vote.Add(new Vote { StoryId = 1, UserId = 1, Voted = true });
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                IEnumerable<VoteDto> votesDto = await new StoryService(context).GetVotes();
                Assert.NotEmpty(votesDto);
                Assert.Equal(2, votesDto.Count());
            }
        }
        [Fact]
        public async void GetVotes_ReturnNull_When_NoContainElements()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                Assert.Empty(await new StoryService(context).GetVotes());
            }
        }
    }
}