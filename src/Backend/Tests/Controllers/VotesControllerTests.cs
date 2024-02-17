using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stories.API.Controllers;
using Stories.API.Request;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Controllers
{
    public class VotesControllerTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public VotesControllerTests() => optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "Votes").Options;

        [Fact]
        public async void Get_ReturnnoContent_When_NocontainsElements()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                VotesController controller = new(new StoryService(context));
                Assert.IsType<NoContentResult>(await controller.Get());
            }
        }

        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Vote.Add(new Vote { Voted = true, StoryId = 1, UserId = 1 });
                context.Vote.Add(new Vote { Voted = true, UserId = 1, StoryId = 1 });
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                VotesController controller = new(new StoryService(context));
                var resultTest = Assert.IsType<OkObjectResult>(await controller.Get());
                var votes = resultTest.Value as IEnumerable<VoteDto>;
                Assert.NotEmpty(votes);
                Assert.Equal(2, votes.Count());
            }
        }

        [Fact]
        public async void Post_RetunCreated_When_CreatedElement()
        {
            using (DataContext context = new(optionsBd))
            {
                VoteRequest voted = new() { Voted = true, StoryId = 1, UserId = 1 };
                context.Database.EnsureDeleted();
                VotesController controller = new(new StoryService(context));
                Assert.IsType<CreatedResult>(await controller.Post(voted));
            }
        }

        //[Fact]
        //public async void Post_RetunBadRequest_When_CreatedElement()
        //{
        //    using (var context = new DataContext(optionsBd))
        //    {
        //    var voted = new VoteRequest { Voted = true, StoryId = null, UserId = 1 };
        //        var controller = new VotesController(new StoryService(context));
        //        Assert.IsType<BadRequestResult>(await controller.Post(null));
        //    }
        //}



    }
}
