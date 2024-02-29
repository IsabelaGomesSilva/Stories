using MediatR;
using Stories.API.Request;
using Stories.API.ViewModel;
using Stories.Service.Services;

namespace Stories.API.Handlers
{
    public class CreateStoryHandler : IRequestHandler<CreateStoryRequest, StoryViewModel>
    {
        private readonly StoryService _storyService;

        public CreateStoryHandler(StoryService storyService)
        {
            _storyService = storyService;
        }
        public  async Task<StoryViewModel> Handle(CreateStoryRequest request, CancellationToken cancellationToken)
        {
            StoryDto  storyDto =  new()
             {
                 Title = request.Title, 
                 DepartmentId = request.DepartmentId,
                 Description = request.Description
             };
             
          var result = await _storyService.Add(storyDto); 
         
           var story = new StoryViewModel
           {
            Id = result,
            Title = storyDto.Title, 
            DepartmentId = storyDto.DepartmentId,
            Description = storyDto.Description
           };

            return story ;
        }
    }
}