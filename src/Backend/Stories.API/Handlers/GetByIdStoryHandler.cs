using MediatR;
using Stories.API.Request;
using Stories.API.ViewModel;
using Stories.Service.Services;

namespace Stories.API.Handlers
{
    public class GetByIdStoryHandler : IRequestHandler<GetByIdStoryRequest, StoryViewModel>
    {
        private readonly StoryService _storyService;

        public GetByIdStoryHandler(StoryService storyService)
        {
            _storyService = storyService;
        }
        public Task<StoryViewModel> Handle(GetByIdStoryRequest request, CancellationToken cancellationToken)
        {
             var story =  _storyService.Get(request.Id);
            if(story == null ) return null; 
            else
            {
                StoryViewModel storyViewModel = new ()
                {
                    Id = story.Id,
                    DepartmentId = story.DepartmentId,
                    DepartmentName = story.DepartmentName,
                    Description = story.Description,
                    Title = story.Title
                };
             return Task.FromResult(storyViewModel); 
             }
        }
    }
}