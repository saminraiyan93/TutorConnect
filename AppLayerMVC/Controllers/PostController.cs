
using BLL.Service;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppLayerMVC.Controllers
{
    public class PostController : Controller
    {
        TutorOfferingService tutorOfferingService;
        CourseService courseService;
        public PostController(TutorOfferingService tutorOfferingService, CourseService courseService)
        {
            this.tutorOfferingService = tutorOfferingService;
            this.courseService = courseService;
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


        // ------ Update TutorOffers ------

        [Authorize]
        [HttpGet]
        public IActionResult UpdateMyTutorOffers(int id)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (userId <= 0)
            {
                return Forbid();
            }

            var offer = tutorOfferingService.GetTutorOfferingById(id);

            if (offer == null || offer.UserId != userId)
            {
                return NotFound();
            }

            ViewBag.Courses = courseService.GetAllCourseWithInfo();

            return View(offer);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMyTutorOffers(TutorOfferingModel obj)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId <= 0)
            {
                return Forbid();
            }

            var existingOffer = tutorOfferingService.GetTutorOfferingById(obj.TutorOfferingId);

            if (existingOffer == null || existingOffer.UserId != userId)
            {
                return NotFound();
            }

            obj.UserId = userId;

            if (obj.PricingType == "Free")
            {
                obj.RateAmount = 0;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Courses = courseService.GetAllCourseWithInfo();
                return View(obj);
            }

            var updated = tutorOfferingService.UpdateTutorOffering(obj, userId);

            if (!updated)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Your tutor offer was updated successfully.";
            return RedirectToAction(nameof(ManageTutorOffers));
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
