using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stories.API.Controllers;
using Stories.Data.Context;
using Stories.Data.Models;
using Stories.Service.Services;

namespace Tests.Controllers
{
    public class UserControllerTests
    {
        
        public UserControllerTests()
        {
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
            
            // Assert
            
        }
        [Fact]
        public async void Get_ReturnOK_When_ContainsElements()
        {
            
        }
        
    }
}