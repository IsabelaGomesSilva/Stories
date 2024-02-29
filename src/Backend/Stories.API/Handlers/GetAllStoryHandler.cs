using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Stories.API.Request;
using Stories.API.ViewModel;
using Stories.Service.Services;

namespace Stories.API.Handlers
{
    public class GetAllStoryHandler : IRequestHandler<GetAllStoriesRequest, List<StoryViewModel>>
    {
        private readonly StoryService _storyService;

        public GetAllStoryHandler(StoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<List<StoryViewModel>> Handle(GetAllStoriesRequest request, CancellationToken cancellationToken)
        {
             var stories = await _storyService.Get();
            if(!stories.Any()) return null;
            else
            {
            return  stories.Select(s => new StoryViewModel
                {
                    Id = s.Id,
                    DepartmentId = s.DepartmentId,
                    DepartmentName = s.DepartmentName,
                    Description = s.Description,
                    Title = s.Title
                }).ToList();
            }
        }
    }
}