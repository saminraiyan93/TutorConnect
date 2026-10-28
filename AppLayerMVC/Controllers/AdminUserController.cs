using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppLayerMVC.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUserController : Controller
{
    private readonly AdminUserService userService;

    public AdminUserController(AdminUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var users = userService.GetAllUsers();
        return View(users);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var user = userService.GetUserById(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Activate(int id)
    {
        if (User.FindFirstValue(ClaimTypes.NameIdentifier) == id.ToString())
        {
            TempData["ErrorMessage"] = "You cannot change your own account status.";
        }
        else if (userService.ActivateUser(id))
        {
            TempData["SuccessMessage"] = "User account activated.";
        }
        else
        {
            TempData["ErrorMessage"] = "Only regular user accounts can be activated.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Restrict(int id, int durationDays)
    {
        if (User.FindFirstValue(ClaimTypes.NameIdentifier) == id.ToString())
        {
            TempData["ErrorMessage"] = "You cannot change your own account status.";
        }
        else if (userService.RestrictUser(id, durationDays))
        {
            TempData["SuccessMessage"] = $"User account restricted for {durationDays} days.";
        }
        else
        {
            TempData["ErrorMessage"] = "Only regular user accounts can be restricted. Choose 1, 7, or 30 days.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Ban(int id)
    {
        if (User.FindFirstValue(ClaimTypes.NameIdentifier) == id.ToString())
        {
            TempData["ErrorMessage"] = "You cannot change your own account status.";
        }
        else if (userService.BanUser(id))
        {
            TempData["SuccessMessage"] = "User account banned.";
        }
        else
        {
            TempData["ErrorMessage"] = "Only regular user accounts can be banned.";
        }

        return RedirectToAction(nameof(Index));
    }
}
