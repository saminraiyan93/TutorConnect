using AppLayerMVC.Models;
using BLL.Models;
using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace AppLayerMVC.Controllers
{
    [Authorize]
    public class TutorController : Controller
    {
        TutorOfferingService tutorOfferingService;
        CourseService courseService;
        StudentRequestService studentRequestService;
        public TutorController(TutorOfferingService tutorOfferingService, CourseService courseService, StudentRequestService studentRequestService)
        {
            this.tutorOfferingService = tutorOfferingService;
            this.courseService = courseService;
            this.studentRequestService = studentRequestService;
        }
        public IActionResult Index()
        {
            var tutorOffers = tutorOfferingService.GetAllTutorOfferingsWithInfo();
            return View(tutorOffers);
        }

        public IActionResult TutorOfferViewDetails(int id)
        {
            var tutorOffer = tutorOfferingService.GetTutorOfferingWithInfoById(id);
            return View(tutorOffer);
        }





        [HttpGet]
        public IActionResult PostTutoringRequest()
        {
            var courses = courseService.GetAllCourseWithInfo();
            ViewBag.Courses = courses;
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View(new StudentRequestViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostTutoringRequest(StudentRequestViewModel StudentRequestViewModel)
        {
            //var userId = HttpContext.Session.GetString("UserId");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Courses = courseService.GetAllCourseWithInfo();
                return View(StudentRequestViewModel);
            }

            if (StudentRequestViewModel.BudgetType == "Free")
            {
                StudentRequestViewModel.BudgetPerLecture = 0;
            }

            var studentRequest = new StudentRequestModel
            {
                UserId = Convert.ToInt32(userId),
                CourseId = StudentRequestViewModel.CourseId,
                CoverageType = StudentRequestViewModel.CoverageType,
                TopicDescription = StudentRequestViewModel.TopicDescription,
                PostTitle = StudentRequestViewModel.PostTitle,
                TeachingMode = StudentRequestViewModel.TeachingMode,
                BudgetType = StudentRequestViewModel.BudgetType,
                BudgetPerLecture = StudentRequestViewModel.BudgetPerLecture,
                Availability = StudentRequestViewModel.Availability,
                ConnectVia = StudentRequestViewModel.ConnectVia,
                ConnectValue = StudentRequestViewModel.ConnectValue,
                PostStatus = "Active"
            };

            var data = studentRequestService.CreateStudentRequest(studentRequest);

            if (data)
            {
                TempData["SuccessMessage"] = "Your tutoring request was posted successfully.";
                return RedirectToAction("Index", "Student");
            }

            ModelState.AddModelError("", "Unable to create tutoring request. Please try again.");
            ViewBag.Courses = courseService.GetAllCourseWithInfo();
            return View(StudentRequestViewModel);
        }

    }
}
