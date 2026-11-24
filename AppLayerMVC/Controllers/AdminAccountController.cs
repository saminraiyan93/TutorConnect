using BLL.Service;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppLayerMVC.Controllers;

[Authorize(Roles = "SuperAdmin")]
public class AdminAccountController : Controller
{
    private readonly AdminUserService userService;

    public AdminAccountController(AdminUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(userService.GetAllAdmins());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new AdminCreateModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AdminCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!userService.CreateAdmin(model))
        {
            ModelState.AddModelError("Email", "This email is already in use or the administrator could not be created.");
            return View(model);
        }

        TempData["SuccessMessage"] = "Administrator account created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var admin = userService.GetUserById(id);

        if (admin == null || (admin.Role != "Admin" && admin.Role != "SuperAdmin"))
        {
            return NotFound();
        }

        var model = new AdminUpdateModel
        {
            UserId = admin.UserId,
            UserName = admin.UserName,
            Email = admin.Email
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AdminUpdateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!userService.UpdateAdmin(model))
        {
            ModelState.AddModelError("Email", "This email is already in use or the administrator could not be updated.");
            return View(model);
        }

        TempData["SuccessMessage"] = "Administrator account updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Activate(int id)
    {
        if (userService.ActivateAdmin(id))
        {
            TempData["SuccessMessage"] = "Administrator account activated.";
        }
        else
        {
            TempData["ErrorMessage"] = "Unable to activate this administrator account.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Disable(int id)
    {
        var currentUserIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(currentUserIdValue, out var currentUserId))
        {
            return Forbid();
        }

        if (userService.DisableAdmin(id, currentUserId))
        {
            TempData["SuccessMessage"] = "Administrator account disabled.";
        }
        else
        {
            TempData["ErrorMessage"] = "This administrator cannot be disabled. You cannot disable your own account or the final active Super Admin.";
        }

        return RedirectToAction(nameof(Index));
    }
}
