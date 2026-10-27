using DAL.Repository;

namespace BLL.Service;

/// <summary>
/// Contains business logic for reviewing reports and flagged user or post content.
/// </summary>
public class AdminReportService
{
    private readonly AdminReportRepo repo;

    public AdminReportService(AdminReportRepo repo)
    {
        this.repo = repo;
    }
}
