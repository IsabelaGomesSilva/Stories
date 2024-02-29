using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Stories.API.ViewModel;

namespace Stories.API.Request
{
    public class CreateStoryRequest : IRequest<StoryViewModel>
    {
       public string Title { get; set; }
       public string Description { get; set; }
       public int DepartmentId { get; set; }  
    } 
}