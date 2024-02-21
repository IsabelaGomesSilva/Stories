using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stories.Data.Models
{
    public class Vote
    {
        public int Id { get; private set; }
        public bool Voted { get; set; }
        public int StoryId { get; set; }
        public int UserId { get; set; }
        public User User {get;}
        public Story Story {get;}
    }
}