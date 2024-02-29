using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Stories.API.ViewModel;

namespace Stories.API.Request
{
    public class DeleteStoryRequest : IRequest<bool?>
    {
        public int Id { get;  set; }    
    }
}