using Microsoft.AspNetCore.Mvc;

namespace AppLayerMVC.Controllers
{
    public class PostController : Controller
    {
        public IActionResult ManagePosts()
        {
            return View();
        }

        public IActionResult ManageTutorOffers()
        {
            return View();
        }

        public IActionResult ManageStudentRequests()
        {
            return View();
        }
    }
}
