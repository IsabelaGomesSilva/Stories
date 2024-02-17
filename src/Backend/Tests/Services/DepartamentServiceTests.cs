using Microsoft.EntityFrameworkCore;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Services
{
    public class DepartamentServiceTests
    {
        private readonly DbContextOptions<DataContext> optionsBd;
        public DepartamentServiceTests() =>  optionsBd = new DbContextOptionsBuilder<DataContext>()
                                                     .UseInMemoryDatabase(databaseName: "ServiceDepartament").Options;
        
        [Fact]
        public async void Get_ReturnNoList_When_NocontainsElements()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                Assert.Empty(await new DepartmentService(context).Get());
            }
        }

        [Fact]
        public async void Get_ReturnList_When_ContainsElements()
        {
            using (DataContext context = new(optionsBd))
            {
                context.Database.EnsureDeleted();
                context.Department.Add( new Department{Name = "Financeiro" });
                context.Department.Add( new Department{Name = "Administrativo" });
                await context.SaveChangesAsync();
            }
            using(var context = new DataContext(optionsBd))
            {
                DepartmentService service = new(context);
                IEnumerable<DepartmentDto> departmentDtos = await service.Get();
                Assert.NotEmpty(departmentDtos);
                Assert.Equal(2, departmentDtos.Count());
            }           
        }
    }
}