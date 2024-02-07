using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stories.Service.Services
{
    public class VoteDto
    {
        public int Id { get; private set; }
        public int StoryId { get; set; }
        public int UserId { get; set; }
        public bool Voted { get; set; }
    }
}