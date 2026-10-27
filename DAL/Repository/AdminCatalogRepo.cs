using DAL.EF;

namespace DAL.Repository;

/// <summary>
/// Provides database operations for administrators to manage departments and courses in the catalog.
/// </summary>
public class AdminCatalogRepo
{
    private readonly TutorConnectContext db;

    public AdminCatalogRepo(TutorConnectContext db)
    {
        this.db = db;
    }
}
