using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stories.API.Controllers;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public UserControllerTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "DBSTORIES").Options;
        
        [Fact]
        public async void Get_ReturnnoContent_When_NocontainsElements()
        {
            var context = new DataContext(optionsBd);
            var controller = new UsersController(new UserService(context));
            Assert.IsType<NoContentResult>( await controller.Get());
        }

        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            using(var context = new DataContext(optionsBd))
            {
                context.User.Add( new User{Name = "Isabela" });
                context.User.Add( new User{Name = "Julia" });
                context.User.Add( new User{Name = "Carol" });
                context.User.Add( new User{Name = "João" });
                await context.SaveChangesAsync();
            }
            using(var context = new DataContext(optionsBd))
            {
                var controller = new UsersController(new UserService(context));
                var resultTest = Assert.IsType<OkObjectResult>( await controller.Get());
                var users = resultTest.Value as IEnumerable<UserDto>;
                Assert.NotNull(users);
                Assert.Equal(4,users.Count());
            }           
        }
    }
}