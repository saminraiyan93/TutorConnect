using DAL.EF;

namespace DAL.Repository;

/// <summary>
/// Provides database operations for administrator reports and flagged user or post content.
/// Add report-specific queries after the Report entity and table are introduced.
/// </summary>
public class AdminReportRepo
{
    private readonly TutorConnectContext db;

    public AdminReportRepo(TutorConnectContext db)
    {
        this.db = db;
    }
}
