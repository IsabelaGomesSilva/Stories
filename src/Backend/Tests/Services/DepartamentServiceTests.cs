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
    public class DepartamentServiceTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public DepartamentServiceTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "DBSTORIES").Options;
        
        [Fact]
        public async void Get_ReturnNoList_When_NocontainsElements()
        {
           using ( var context = new DataContext(optionsBd))
           {
            var service =  new DepartmentService(context);
            Assert.Empty(await service.Get());
           }
        }

        [Fact]
        public async void Get_ReturnList_When_ContainsElements()
        {
            using(var context = new DataContext(optionsBd))
            {
                context.Department.Add( new Department{Name = "Financeiro" });
                context.Department.Add( new Department{Name = "Administrativo" });
                await context.SaveChangesAsync();
            }
            using(var context = new DataContext(optionsBd))
            {
                var service = new DepartmentService(context);
                IEnumerable<DepartmentDto> departmentDtos = await service.Get();
                Assert.NotEmpty(departmentDtos);
                Assert.Equal(2, departmentDtos.Count());
            }           
        }
    }
}