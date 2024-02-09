using System.ComponentModel.DataAnnotations;

namespace Stories.API.Request
{
    public class VoteRequest
    {
        [Required]
        public int StoryId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool Voted { get; set; }
    }
}