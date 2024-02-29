using MediatR;
using Stories.API.Request;
using Stories.Service.Services;

namespace Stories.API.Handlers
{
    public class DeleteStoryHandler : IRequestHandler<DeleteStoryRequest, bool>
    {
        private readonly StoryService _storyService;

        public DeleteStoryHandler(StoryService storyService)
        {
            _storyService = storyService;
        }
        public Task<bool> Handle(DeleteStoryRequest request, CancellationToken cancellationToken)
        {
             if (_storyService.Get(request.Id) == null) 
                return null;
            return _storyService.Delete(request.Id);
            
        }
    }
}