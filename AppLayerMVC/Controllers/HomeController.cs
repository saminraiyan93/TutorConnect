using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BLL.Service;

namespace AppLayerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly TutorOfferingService tutorOfferingService;
        private readonly StudentRequestService studentRequestService;

        public HomeController(TutorOfferingService tutorOfferingService, StudentRequestService studentRequestService)
        {
            this.tutorOfferingService = tutorOfferingService;
            this.studentRequestService = studentRequestService;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var approvedOffers = tutorOfferingService.ConsumeApprovalNotices(userId);
            var approvedRequests = studentRequestService.ConsumeApprovalNotices(userId);
            if (approvedOffers > 0 || approvedRequests > 0)
            {
                TempData["SuccessMessage"] = "One or more of your posts have been approved and are now live on the site.";
            }

            ViewBag.User = User.Identity?.Name;
            return View();
        }
    }
}