using BLL.Service;
using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppLayerMVC.Controllers
{
    public class PostController : Controller
    {
        TutorOfferingService tutorOfferingService;
        public PostController(TutorOfferingService tutorOfferingService)
        {
            this.tutorOfferingService = tutorOfferingService;
        }
        public IActionResult ManagePosts()
        {
            return View();
        }

        public IActionResult ManageTutorOffers()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var getMyTutorOffers = tutorOfferingService.GetTutorOfferingsByUserIdWithInfo(userId);
            return View(getMyTutorOffers);
        }

        public IActionResult GetMyTutorOffers()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var getMyTutorOffers = tutorOfferingService.GetTutorOfferingsByUserIdWithInfo(userId);
            return View(getMyTutorOffers);
        }

        public IActionResult UpdateMyTutorOffers()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMyTutorOffers(int id)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var data = tutorOfferingService.DeleteTutorOffering(id, userId);

            if (data)
            {
                TempData["SuccessMessage"] = "Your tutor offer was deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to delete tutor offer.";
            }

            return RedirectToAction("ManageTutorOffers");
        }

        public IActionResult ManageStudentRequests()
        {
            return View();
        }
    }
}
