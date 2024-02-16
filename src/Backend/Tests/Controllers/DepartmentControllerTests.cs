using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stories.API.Controllers;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;
using Xunit;

namespace Tests.Controllers
{
    public class DepartmentControllerTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public DepartmentControllerTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "DBSTORIES").Options;
        
        [Fact]
        public async void Get_ReturnnoContent_When_NocontainsElements()
        {
          using(  var context = new DataContext(optionsBd))
          {
            var controller = new DepartmentsController(new DepartmentService(context));
            Assert.IsType<NoContentResult>( await controller.Get());
          }   
        }

        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            using(var context = new DataContext(optionsBd))
            {
                context.Department.Add( new Department {Name = "Financeiro" });
                context.Department.Add( new Department {Name = "Administrativo" });
                await context.SaveChangesAsync();
            }
            using(var context = new DataContext(optionsBd))
            {
                var controller = new DepartmentsController(new DepartmentService(context));
                var resultTest = Assert.IsType<OkObjectResult>( await controller.Get());
                var departments = resultTest.Value as IEnumerable<DepartmentDto>;
                Assert.NotEmpty(departments);
                Assert.Equal(2, departments.Count());
            }           
        }
    }
}