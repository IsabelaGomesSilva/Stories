using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stories.Data.Context;

namespace Stories.Service.Services
{
    public class DepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> Get()
        {
            var departaments = await _context.Department.AsNoTracking().ToListAsync();
            return departaments.Select(d => new DepartmentDto { Id = d.Id, Name = d.Name});   
        }
    }
}