
using System.ComponentModel.DataAnnotations;

namespace Stories.API.Request
{
    public class StoryRequest
    {
       [Required]
       public string Title { get; set; }
       [Required]
       public string Description { get; set; }
       [Required]
       public int DepartmentId { get; set; }
    }
}