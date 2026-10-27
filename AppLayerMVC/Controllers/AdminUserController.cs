using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
}
