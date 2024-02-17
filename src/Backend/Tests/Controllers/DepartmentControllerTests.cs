using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stories.API.Controllers;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Controllers
{
    public class DepartmentControllerTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public DepartmentControllerTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "Department").Options;
        
        [Fact]
        public async void Get_ReturnnoContent_When_NocontainsElements()
        {
            using(DataContext context = new(optionsBd))
            { 
                context.Database.EnsureDeleted();
            }

            using (DataContext context = new(optionsBd))
            {
                DepartmentsController controller = new(new DepartmentService(context));
                Assert.IsType<NoContentResult>(await controller.Get());
            }
        }

        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            using(DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Department.Add( new Department {Name = "Financeiro" });
                context.Department.Add( new Department {Name = "Administrativo" });
                await context.SaveChangesAsync();
            }
            using(DataContext context = new(optionsBd))
            {
                DepartmentsController controller = new(new DepartmentService(context));
                var resultTest = Assert.IsType<OkObjectResult>( await controller.Get());
                var departments = resultTest.Value as IEnumerable<DepartmentDto>;
                Assert.NotEmpty(departments);
                Assert.Equal(2, departments.Count());
            }           
        }
    }
}