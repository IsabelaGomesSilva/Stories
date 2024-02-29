using MediatR;
using Stories.API.Request;
using Stories.API.ViewModel;
using Stories.Service.Services;

namespace Stories.API.Handlers
{
    public class UpdateStoryHandler : IRequestHandler<UpdateStoryRequest, bool>
    {
        private readonly StoryService _storyService;

        public UpdateStoryHandler(StoryService storyService)
        {
            _storyService = storyService;
        }
        public Task<bool> Handle(UpdateStoryRequest request, CancellationToken cancellationToken)
        {
             if (_storyService.Get(request.Id) == null)
                return null;
            else
            {
                StoryDto storyDto = new()
                {
                    Id = request.Id,
                    Title = request.Body.Title,
                    DepartmentId = request.Body.DepartmentId,
                    Description = request.Body.Description
                };
                    return _storyService.Update(storyDto);
    
            }
        }
    }
}