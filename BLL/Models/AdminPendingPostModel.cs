namespace BLL.Models;

public class AdminPendingPostModel
{
    public int PostId { get; set; }
    public string PostType { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string PostTitle { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public string DepartmentName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
