

namespace Stories.API.ViewModel
{
    public class VoteRequest
    {

        public int StoryId { get; set; }
        public int UserId { get; set; }
        public bool Voted { get; set; }
    }
}