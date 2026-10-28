using Microsoft.AspNetCore.Mvc;
using AppLayerMVC.Models.Account;
namespace AppLayerAPI.Controllers;
using BLL.Service;
using BLL.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authorization;



public class AccountController : Controller
{

    UserService service;
    LoginService login_service;
    //IMapper mapper;       // To be added later (if needed)

    public AccountController(UserService service, LoginService login_service)
    {
        this.service = service;
        this.login_service = login_service;
    }


    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        if (Request.Query["message"] == "login-required")
        {
            ViewBag.LoginMessage = "You must log in to access that page.";
        }

        return View(new LoginModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel LoginModel)   
    {
        if (ModelState.IsValid)
        {
            try
            {
                var User = login_service.GetUserByEmailAndPassword(LoginModel.Email, LoginModel.Password);
                if(User != null)
                {
                    // Store User info in session (Replaced by AuthCookies but still keeping it just in case we need it)
                    HttpContext.Session.SetString("UserId", User.UserId.ToString());
                    HttpContext.Session.SetString("UserName", User.UserName);
                    HttpContext.Session.SetString("Email", User.Email);
                    HttpContext.Session.SetString("Role", User.Role);

                    // New: Claim Based Authentication
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, User.UserId.ToString()),
                        new Claim(ClaimTypes.Name, User.UserName),
                        new Claim(ClaimTypes.Email, User.Email),
                        new Claim(ClaimTypes.Role, User.Role)
                    };

                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    var expiresIn = LoginModel.RememberMe
                        ? TimeSpan.FromDays(7)
                        : TimeSpan.FromHours(8);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = LoginModel.RememberMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(expiresIn),
                        AllowRefresh = false
                    };

                    await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);

                    if(User.Role == "Admin")
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }           

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                // Get the inner exception details
                var innerException = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError("", $"An error occurred: {innerException}");
                // Handle unexpected errors
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }

        }

        return View(LoginModel);
    }

    [HttpGet]
    public IActionResult Registration()
    {
        return View(new RegistrationModel());   // value = @Model.UserName ,  value = @Model.Email ... initially Model can't be null. but UserName = null, Email = null can be true, thats why create an empty object
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Registration(RegistrationModel RegistrationModel)
    {
        if (ModelState.IsValid)
        {
            var userModel = new UserModel
            {
                UserName = RegistrationModel.UserName,
                Email = RegistrationModel.Email,
                Password = RegistrationModel.Password,
                CreatedAt = DateTime.Now
            };

            try
            {
                var data = service.CreateUser(userModel);

                if (data)
                {
                    TempData["SuccessMessage"] = "Registration successful. Please log in.";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");     // general error
                }
            }
            catch(Exception ex)
            {
                // Get the inner exception details
                var innerException = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError("", $"An error occurred: {innerException}");
                // Handle unexpected errors
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }    
        }

        return View(RegistrationModel);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Profile()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = service.GetUserById(userId);

        if (user == null)
        {
            return NotFound();
        }

        return ShowProfile(user);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfileName(EditProfileNameModel EditProfileNameModel)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = service.GetUserById(userId);

        if (user == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return ShowProfile(user, "name");
        }

        user.UserName = EditProfileNameModel.UserName;
        user.UpdatedAt = DateTime.Now;

        if (service.UpdateUser(user))
        {
            await RefreshProfileClaims(user);
            TempData["SuccessMessage"] = "Name updated successfully.";
            return RedirectToAction("Profile");
        }

        ModelState.AddModelError("", "Name update failed. Please try again.");
        return ShowProfile(service.GetUserById(userId), "name");
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfileEmail(EditProfileEmailModel EditProfileEmailModel)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = service.GetUserById(userId);

        if (user == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return ShowProfile(user, "email");
        }

        if (service.EmailExists(EditProfileEmailModel.Email, userId))
        {
            ModelState.AddModelError("Email", "This email is already in use.");
            return ShowProfile(user, "email");
        }

        user.Email = EditProfileEmailModel.Email;
        user.UpdatedAt = DateTime.Now;

        if (service.UpdateUser(user))
        {
            await RefreshProfileClaims(user);
            TempData["SuccessMessage"] = "Email updated successfully.";
            return RedirectToAction("Profile");
        }

        ModelState.AddModelError("", "Email update failed. Please try again.");
        return ShowProfile(service.GetUserById(userId), "email");
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateProfilePassword(EditProfilePasswordModel EditProfilePasswordModel)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = service.GetUserById(userId);

        if (user == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return ShowProfile(user, "password");
        }

        user.Password = EditProfilePasswordModel.Password;
        user.UpdatedAt = DateTime.Now;

        if (service.UpdateUser(user))
        {
            TempData["SuccessMessage"] = "Password updated successfully.";
            return RedirectToAction("Profile");
        }

        ModelState.AddModelError("", "Password update failed. Please try again.");
        return ShowProfile(service.GetUserById(userId), "password");
    }

    private IActionResult ShowProfile(UserModel user, string editField = "")
    {
        ViewBag.EditField = editField;

        var ProfileModel = new ProfileModel
        {
            UserName = user.UserName,
            Email = user.Email,
            AccountStatus = user.AccountStatus
        };

        return View("Profile", ProfileModel);
    }

    private async Task RefreshProfileClaims(UserModel user)
    {
        HttpContext.Session.SetString("UserName", user.UserName);
        HttpContext.Session.SetString("Email", user.Email);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);
        var authProperties = (await HttpContext.AuthenticateAsync("MyCookieAuth")).Properties;

        await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);
    }

    // LOGOUT Logic to be added

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        HttpContext.Session.Clear();

        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
