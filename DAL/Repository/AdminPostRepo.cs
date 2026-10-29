using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

/// <summary>
/// Provides database operations for administering, reviewing, and moderating tutor offers and student requests.
/// </summary>
public class AdminPostRepo
{
    private readonly TutorConnectContext db;

    public AdminPostRepo(TutorConnectContext db)
    {
        this.db = db;
    }

    public List<TutorOffering> GetAllTutorOffersWithInfo()
    {
        return db.TutorOfferings
            .Include(offer => offer.User)
            .Include(offer => offer.Course)
                .ThenInclude(course => course.Department)
            .OrderByDescending(offer => offer.CreatedAt)
            .ToList();
    }

    public List<StudentRequest> GetAllStudentRequestsWithInfo()
    {
        return db.StudentRequests
            .Include(request => request.User)
            .Include(request => request.Course)
                .ThenInclude(course => course.Department)
            .OrderByDescending(request => request.CreatedAt)
            .ToList();
    }

    public bool UpdateTutorOfferStatus(int id, string status)
    {
        var offer = db.TutorOfferings.Find(id);
        if (offer == null)
        {
            return false;
        }

        offer.PostStatus = status;
        offer.UpdatedAt = DateTime.Now;
        return db.SaveChanges() > 0;
    }

    public bool UpdateStudentRequestStatus(int id, string status)
    {
        var request = db.StudentRequests.Find(id);
        if (request == null)
        {
            return false;
        }

        request.PostStatus = status;
        request.UpdatedAt = DateTime.Now;
        return db.SaveChanges() > 0;
    }
}
