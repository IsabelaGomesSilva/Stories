using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stories.Data.Context;
using Stories.Data.Models;

namespace Stories.Service.Services
{
    public class StoryService
    {
        private readonly DataContext _context;

        public StoryService(DataContext context)
        {
            _context = context;
        }
         public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Add(StoryDto storyDto)
        {
            _context.Story.Add(new Story {Title = storyDto.Title, Description = storyDto.Description
                                           , DepartmentId = storyDto.DepartmentId });
            return await SaveChangesAsync();
        }
        public async Task<IEnumerable<StoryDto>> Get()
        {
            var stories = await  _context.Story.AsNoTracking().ToListAsync();
            return stories.Select(s => new StoryDto { Id = s.Id, Title = s.Title, DepartmentId = s.DepartmentId
                                  ,Description = s.Description}).ToList();   
        }
        // public async Task<bool> Update(int id, StoryDto s)
        // {
        //     var t =  new StoryDto { Id = s.Id, Title = s.Title, DepartmentId = s.DepartmentId
        //      ,Description = s.Description};
        //     _context.Story.Where(s => s.Id == id).ExecuteUpdate(exec => exec.SetProperty(b => b.Title, s.Title   );
        // }
        public async Task<bool> Delete(int id)
        {
           
             _context.Story.Where(s => s.Id == id).ExecuteDelete();
        
            return await SaveChangesAsync();
        }
        
    }
}
