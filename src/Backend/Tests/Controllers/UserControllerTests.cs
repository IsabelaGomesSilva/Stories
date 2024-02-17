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
                                                     .UseInMemoryDatabase(databaseName: "User").Options;
        
        [Fact]
        public async void Get_ReturnnoContent_When_NocontainsElements()
        {
            DataContext context = new(optionsBd);
            context.Database.EnsureDeleted();
            UsersController controller = new(new UserService(context));
            Assert.IsType<NoContentResult>( await controller.Get());
            
        }

        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.User.Add( new User{Name = "Isabela"});
                context.User.Add( new User{Name = "Julia"});
                await context.SaveChangesAsync();
            }
            using (DataContext context = new(optionsBd))
            {
                UsersController controller = new(new UserService(context));
                var resultTest = Assert.IsType<OkObjectResult>( await controller.Get());
                var users = resultTest.Value as IEnumerable<UserDto>;
                Assert.NotEmpty(users);
                Assert.Equal(2,users.Count());
            }           
        }
    }
}