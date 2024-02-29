using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Stories.API.Request
{
    public class UpdateBodyStoryRequest : IRequest<bool>
    {
       public string Title { get; set; }
       public string Description { get; set; }
       public int DepartmentId { get; set; }
    }
}