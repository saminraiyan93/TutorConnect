using BLL.Models;
using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerMVC.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AdminDashboardService dashboardService;

    public AdminController(AdminDashboardService dashboardService)
    {
        this.dashboardService = dashboardService;
    }

    [HttpGet]
    public IActionResult Dashboard()
    {
        var model = new AdminDashboardModel
        {
            TotalUsers = dashboardService.GetTotalUsers(),
            ActiveTutorOffers = dashboardService.GetActiveTutorOffers(),
            ActiveStudentRequests = dashboardService.GetActiveStudentRequests(),
            PendingModerationItems = dashboardService.GetPendingModerationItems()
        };

        return View(model);
    }
}
