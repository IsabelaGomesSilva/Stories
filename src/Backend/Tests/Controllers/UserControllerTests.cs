using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stories.API.Controllers;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<UserService> _mock;
        private readonly UsersController _controller;

        public UserControllerTests()
        {
            _mock = new Mock<UserService>();
            _controller = new UsersController(_mock.Object);
        }

         List<UserDto> usersDto = new List<UserDto>
        {
            new () { Id = 1 , Name = "Isabela" },
            new () { Id = 2, Name = "Julia" },
            new () { Id = 3, Name = "Carlos" }
        };
        

        [Fact]
        public async void Get_ReturnnoContent_When_NocontainsElements()
        {
            // Arrange
            _mock.Setup(c => c.Get()).ReturnsAsync(new List<UserDto>());
            // Act
            var result = await _controller.Get();
            // Assert
            Assert.IsType<NoContentResult>(result);

        }
        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            // Arrange
            _mock.Setup(c => c.Get()).ReturnsAsync(usersDto) ;
            // Act
            var result = await _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        
    }
}