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
        public StoryService(DataContext context) =>  _context = context;
        
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public bool Delete(int id) =>  _context.Story.Where(s => s.Id == id).ExecuteDelete() > 0;
        

        public async Task<(bool isCreated, int isCreatedId)> Add(StoryDto storyDto)
        {
            var story = new Story {Title = storyDto.Title, Description = storyDto.Description,
                                   DepartmentId = storyDto.DepartmentId };
            _context.Story.Add(story);
            return ( await SaveChangesAsync(), story.Id);
        }
        public async Task<IEnumerable<StoryDto>> Get()
        {
            var stories = await  _context.Story.AsNoTracking().ToListAsync();
            return stories.Select(s => new StoryDto { Id = s.Id, Title = s.Title, DepartmentId = s.DepartmentId
                                  ,Description = s.Description}).ToList();   
        }
        public bool Update(int id, StoryDto s)
        {
            return _context.Story.Where(s => s.Id == id)
                                 .ExecuteUpdate(exec => exec.SetProperty(t => t.Title, s.Title)
                                                            .SetProperty(d => d.DepartmentId, s.DepartmentId)
                                                            .SetProperty(d => d.Description, s.Description)) > 0;
        }
       
        public StoryDto Get(int id)
        {
            var story = _context.Story.AsNoTracking().FirstOrDefault(s => s.Id == id);
            if (story == null) return null;
            else
            {
                StoryDto storyDto = new()
                {
                    Id = story.Id,
                    Title = story.Title,
                    DepartmentId = story.DepartmentId,
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
            return ( await SaveChangesAsync(), vote.Id);;
        }
        public async Task<IEnumerable<VoteDto>> GetVotes()
        {
            var votes = await _context.Vote.AsNoTracking().ToListAsync();
            return votes.Select(s => new VoteDto
            {
                Id = s.Id,
               StoryId= s.StoryId,
               Voted = s.Voted,
               UserId= s.UserId
            }).ToList();
        }
    }
}
