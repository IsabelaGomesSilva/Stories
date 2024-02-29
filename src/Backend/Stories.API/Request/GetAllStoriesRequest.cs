using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Stories.API.ViewModel;

namespace Stories.API.Request
{
    public class GetAllStoriesRequest : IRequest<List<StoryViewModel>>
    {
        
    }
}