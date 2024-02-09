

namespace Stories.API.ViewModel
{
    public class VoteViewModel
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public int UserId { get; set; }
        public bool Voted { get; set; }
    }
}