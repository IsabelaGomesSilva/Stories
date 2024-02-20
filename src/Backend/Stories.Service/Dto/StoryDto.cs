
namespace Stories.Service.Services
{
    public class StoryDto
    {
       public int Id { get;  set; }  
       public string Title { get; set; }
       public string Description { get; set; }
       public int DepartmentId { get; set; }
       public string DepartmentName {get; set;}
    }
}