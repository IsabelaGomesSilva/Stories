using Microsoft.EntityFrameworkCore;
using Stories.Data.Context;
using Stories.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Service.Services
{
    public class UserService 
    {
        private readonly DataContext _context;
        

        public UserService(DataContext context)
        {
            _context = context;
        }
        

        public async Task<IEnumerable<UserDto>> Get()
        {
           var user =  await _context.User.AsNoTracking().ToListAsync();
           return user.Select(u => new UserDto {Id = u.Id, Name = u.Name}).ToList();
         }
    }
}
