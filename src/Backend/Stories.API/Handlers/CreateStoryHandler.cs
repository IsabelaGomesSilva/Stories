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
        public  Task<StoryViewModel> Handle(CreateStoryRequest request, CancellationToken cancellationToken)
        {
            StoryDto  storyDto =  new()
             {
                 Title = request.Title, 
                 DepartmentId = request.DepartmentId,
                 Description = request.Description
             };
             
             _storyService.Add(storyDto);
         
           var result = new StoryViewModel
           {
            Id = storyDto.Id,
            Title = storyDto.Title, 
            DepartmentId = storyDto.DepartmentId,
            Description = storyDto.Description
           };

            return Task.FromResult(result) ;
        }
    }
}