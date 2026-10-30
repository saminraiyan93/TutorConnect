using AutoMapper;
using BLL.Models;
using DAL.Repository;

namespace BLL.Service;

/// <summary>
/// Contains business logic for reviewing and moderating tutor offers and student requests.
/// </summary>
public class AdminPostService
{
    private readonly AdminPostRepo repo;
    private readonly IMapper mapper;

    public AdminPostService(AdminPostRepo repo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
    }

    public List<TutorOfferingInfoModel> GetAllTutorOffersWithInfo()
    {
        var offers = repo.GetAllTutorOffersWithInfo();
        return mapper.Map<List<TutorOfferingInfoModel>>(offers);
    }

    public List<StudentRequestInfoModel> GetAllStudentRequestsWithInfo()
    {
        var requests = repo.GetAllStudentRequestsWithInfo();
        return mapper.Map<List<StudentRequestInfoModel>>(requests);
    }

    public List<AdminPendingPostModel> GetPendingModerationPosts()
    {
    
        var offers = mapper.Map<List<TutorOfferingInfoModel>>(repo.GetPendingTutorOffersWithInfo());
        var requests = mapper.Map<List<StudentRequestInfoModel>>(repo.GetPendingStudentRequestsWithInfo());
        var pendingPosts = new List<AdminPendingPostModel>();

        foreach (var offer in offers)
        {
            pendingPosts.Add(new AdminPendingPostModel
            {
                PostId = offer.TutorOfferingId,
                PostType = "Tutor Offer",
                UserName = offer.UserName,
                PostTitle = offer.PostTitle,
                CourseName = offer.CourseName,
                DepartmentName = offer.DepartmentName,
                CreatedAt = offer.CreatedAt
            });
        }

        foreach (var request in requests)
        {
            pendingPosts.Add(new AdminPendingPostModel
            {
                PostId = request.StudentRequestId,
                PostType = "Student Request",
                UserName = request.UserName,
                PostTitle = request.PostTitle,
                CourseName = request.CourseName,
                DepartmentName = request.DepartmentName,
                CreatedAt = request.CreatedAt
            });
        }

        return pendingPosts.OrderByDescending(post => post.CreatedAt).ToList();
    }

    public TutorOfferingInfoModel? GetTutorOfferWithInfo(int id)
    {
        var offer = repo.GetTutorOfferWithInfo(id);
        return offer == null ? null : mapper.Map<TutorOfferingInfoModel>(offer);
    }

    public StudentRequestInfoModel? GetStudentRequestWithInfo(int id)
    {
        var request = repo.GetStudentRequestWithInfo(id);
        return request == null ? null : mapper.Map<StudentRequestInfoModel>(request);
    }

    public bool UpdateTutorOfferStatus(int id, string status)
    {
        if (status != "Pending" && status != "Active" && status != "Inactive")
        {
            return false;
        }

        return repo.UpdateTutorOfferStatus(id, status);
    }

    public bool UpdateStudentRequestStatus(int id, string status)
    {
        if (status != "Pending" && status != "Active" && status != "Inactive")
        {
            return false;
        }

        return repo.UpdateStudentRequestStatus(id, status);
    }

    public bool ModerateTutorOffer(int id, string status)
    {
        if (status != "Active" && status != "Inactive")
        {
            return false;
        }

        return repo.ModerateTutorOffer(id, status);
    }

    public bool ModerateStudentRequest(int id, string status)
    {
        if (status != "Active" && status != "Inactive")
        {
            return false;
        }

        return repo.ModerateStudentRequest(id, status);
    }
}
