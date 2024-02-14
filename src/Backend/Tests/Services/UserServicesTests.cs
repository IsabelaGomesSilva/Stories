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
    public class UserServicesTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public UserServicesTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "DBSTORIES").Options;
        
        [Fact]
        public async void Get_ReturnNoList_When_NocontainsElements()
        {
           using ( var context = new DataContext(optionsBd))
           {
            var service =  new UserService(context);
            Assert.Empty(await service.Get());
           }
        }

        [Fact]
        public async void Get_ReturnList_When_ContainsElements()
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
                var service = new UserService(context);
                IEnumerable<UserDto> userDtos = await service.Get();
                Assert.NotEmpty(userDtos);
                Assert.Equal(4, userDtos.Count());
            }           
        }
    }
}