using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Stories.Data.Context;

namespace Stories.Services.Repository
{
    public class Services<T>  where T : class
    {
        private readonly DataContext _context;

        public Services(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0; 
        }
        public async Task<bool> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return await SaveChangesAsync();
        }
        
    }
}