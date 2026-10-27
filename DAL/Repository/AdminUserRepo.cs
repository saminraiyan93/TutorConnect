using DAL.EF;

namespace DAL.Repository;

/// <summary>
/// Provides database operations used by administrators to view and manage user accounts.
/// </summary>
public class AdminUserRepo
{
    private readonly TutorConnectContext db;

    public AdminUserRepo(TutorConnectContext db)
    {
        this.db = db;
    }
}
