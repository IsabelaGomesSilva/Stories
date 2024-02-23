using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public StoryService(DataContext context) =>  _context = context;
        
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> Delete(int id) 
        {
            var story = _context.Story.FindAsync(id ).Result;
            if (story == null) return false;
            else 
               _context.Story.Remove(story);
               return await SaveChangesAsync();
        } 
        

        public async Task<(bool isCreated, int isCreatedId)> Add(StoryDto storyDto)
        {
            var story = new Story {Title = storyDto.Title, Description = storyDto.Description,
                                   DepartmentId = storyDto.DepartmentId };
            _context.Story.Add(story);
            return ( await SaveChangesAsync(), story.Id);
        }
        public async Task<IEnumerable<StoryDto>> Get()
        {
            var stories =  _context.Story.AsNoTracking().Include(d => d.Department);
            return  stories.Select(s => new StoryDto 
            {   Id = s.Id, 
                Title = s.Title, 
                DepartmentId = s.DepartmentId,
                Description = s.Description,
                DepartmentName = s.Department.Name
            }).ToList();   
        }
        public async Task<bool> Update(StoryDto storyDto)
        {
            var story = _context.Story.FindAsync(storyDto.Id).Result;
            if (story == null) { return false; }
            else
            {
                story.Description = storyDto.Description;
                story.Title = storyDto.Title;
                story.DepartmentId = storyDto.DepartmentId;

                _context.Story.Update(story);

                return await SaveChangesAsync();
            }
        }
       
        public StoryDto Get(int id)
        {
            var story = _context.Story.AsNoTracking().Include(d => d.Department).FirstOrDefault(s => s.Id == id);
            if (story == null) return null;
            else
            {
                StoryDto storyDto = new()
                {
                    Id = story.Id,
                    Title = story.Title,
                    DepartmentId = story.DepartmentId,
                    DepartmentName = story.Department.Name,
                    Description = story.Description
                };
                return storyDto;
            }
        }
        public async Task<(bool isCreated, int isCreatedId)> AddVoted(VoteDto voteDto)
        {
            var vote = new Vote
            {
                Voted = voteDto.Voted,
                StoryId = voteDto.StoryId,
                UserId = voteDto.UserId
            };
            _context.Vote.Add(vote);
            return ( await SaveChangesAsync(), vote.Id);
        }
        public async Task<IEnumerable<VoteDto>> GetVotes()
        {

            IQueryable<Vote> votes =  _context.Vote.AsNoTracking().Include(v => v.Story).Include(v => v.User);
             return votes.Select(s => new VoteDto

            {
               Id = s.Id,
               StoryId= s.StoryId,
               StoryTitle = s.Story.Title,
               Voted = s.Voted,
               UserId= s.UserId,
               UserName = s.User.Name
            }).ToList();
            
        }
    }
}
