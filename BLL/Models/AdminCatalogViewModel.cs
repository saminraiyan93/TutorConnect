using System.Collections.Generic;

namespace BLL.Models;

public class AdminCatalogViewModel
{
    public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
    public List<DepartmentModel> Departments { get; set; } = new List<DepartmentModel>();

    public CourseModel NewCourse { get; set; } = new CourseModel();
    public DepartmentModel NewDepartment { get; set; } = new DepartmentModel();
}
