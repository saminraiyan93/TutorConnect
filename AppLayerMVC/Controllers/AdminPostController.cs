using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Service;

namespace AppLayerMVC.Controllers;

[Authorize(Roles = "Admin")]
public class AdminPostController : Controller
{
    private readonly AdminPostService adminPostService;

    public AdminPostController(AdminPostService adminPostService)
    {
        this.adminPostService = adminPostService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AdminManageTutorOffers()
    {
        var offers = adminPostService.GetAllTutorOffersWithInfo();
        return View(offers);
    }

    [HttpGet]
    public IActionResult AdminManageStudentRequests()
    {
        var requests = adminPostService.GetAllStudentRequestsWithInfo();
        return View(requests);
    }

    [HttpGet]
    public IActionResult Pending()
    {
        var posts = adminPostService.GetPendingModerationPosts();
        return View(posts);
    }

    [HttpGet]
    public IActionResult TutorOfferDetails(int id, string? returnTo)
    {
        var offer = adminPostService.GetTutorOfferWithInfo(id);
        if (offer == null)
        {
            return NotFound();
        }

        if (returnTo == "AdminManageTutorOffers")
        {
            ViewBag.AdminPostBackAction = "AdminManageTutorOffers";
        }
        else
        {
            ViewBag.AdminPostBackAction = "Pending";
        }

        return View("~/Views/Tutor/TutorOfferViewDetails.cshtml", offer);
    }

    [HttpGet]
    public IActionResult StudentRequestDetails(int id, string? returnTo)
    {
        var request = adminPostService.GetStudentRequestWithInfo(id);
        if (request == null)
        {
            return NotFound();
        }

        if (returnTo == "AdminManageStudentRequests")
        {
            ViewBag.AdminPostBackAction = "AdminManageStudentRequests";
        }
        else
        {
            ViewBag.AdminPostBackAction = "Pending";
        }

        return View("~/Views/Student/StudentRequestViewDetails.cshtml", request);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateTutorOfferStatus(int id, string status)
    {
        if (adminPostService.UpdateTutorOfferStatus(id, status))
        {
            TempData["SuccessMessage"] = "Tutor offer status updated.";
        }
        else
        {
            TempData["ErrorMessage"] = "Unable to update tutor offer status.";
        }

        return RedirectToAction("AdminManageTutorOffers");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateStudentRequestStatus(int id, string status)
    {
        if (adminPostService.UpdateStudentRequestStatus(id, status))
        {
            TempData["SuccessMessage"] = "Student request status updated.";
        }
        else
        {
            TempData["ErrorMessage"] = "Unable to update student request status.";
        }

        return RedirectToAction("AdminManageStudentRequests");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ModerateTutorOffer(int id, string status)
    {
        if (adminPostService.ModerateTutorOffer(id, status))
        {
            if (status == "Active")
            {
                TempData["SuccessMessage"] = "Tutor offer approved and activated.";
            }
            else
            {
                TempData["SuccessMessage"] = "Tutor offer declined and marked inactive.";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "This tutor offer is no longer pending or the requested action is invalid.";
        }

        return RedirectToAction("Pending");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ModerateStudentRequest(int id, string status)
    {
        if (adminPostService.ModerateStudentRequest(id, status))
        {
            if (status == "Active")
            {
                TempData["SuccessMessage"] = "Student request approved and activated.";
            }
            else
            {
                TempData["SuccessMessage"] = "Student request declined and marked inactive.";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "This student request is no longer pending or the requested action is invalid.";
        }

        return RedirectToAction("Pending");
    }
}
