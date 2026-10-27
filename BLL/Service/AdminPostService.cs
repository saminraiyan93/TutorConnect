using DAL.Repository;

namespace BLL.Service;

/// <summary>
/// Contains business logic for reviewing and moderating tutor offers and student requests.
/// </summary>
public class AdminPostService
{
    private readonly AdminPostRepo repo;

    public AdminPostService(AdminPostRepo repo)
    {
        this.repo = repo;
    }
}
