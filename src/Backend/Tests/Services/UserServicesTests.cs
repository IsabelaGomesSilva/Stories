using Microsoft.EntityFrameworkCore;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Services
{
    public class UserServicesTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public UserServicesTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "ServiceUser").Options;
        
        [Fact]
        public async void Get_ReturnNoList_When_NocontainsElements()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                Assert.Empty(await new UserService(context).Get());
            }
        }
        [Fact]
        public async void Get_ReturnList_When_ContainsElements()
        {
            using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted(); 
                context.User.Add( new User{Name = "Carol" });
                context.User.Add( new User{Name = "João" });
                await context.SaveChangesAsync();
            }
            using(DataContext context = new(optionsBd))
            {
                IEnumerable<UserDto> userDtos = await new UserService(context).Get();
                Assert.NotEmpty(userDtos);
                Assert.Equal(2, userDtos.Count());
            }           
        }
    }
}