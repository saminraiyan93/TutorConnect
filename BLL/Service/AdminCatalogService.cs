using DAL.Repository;

namespace BLL.Service;

/// <summary>
/// Contains business logic for managing the department and course catalog.
/// </summary>
public class AdminCatalogService
{
    private readonly AdminCatalogRepo repo;

    public AdminCatalogService(AdminCatalogRepo repo)
    {
        this.repo = repo;
    }
}
