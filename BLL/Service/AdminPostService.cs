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
}
