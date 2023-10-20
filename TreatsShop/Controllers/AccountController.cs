using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SweetAndSavoryTreats.Models;
using SweetAndSavoryTreats.ViewModels;
using System.Threading.Tasks;

namespace SweetAndSavoryTreats.Controllers
{
  public class AccountController : Controller
  {
    private readonly SweetAndSavoryTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, SweetAndSavoryTreatsContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.Title = "Authentication with Identity";
      return View();
    }

    public IActionResult Register()
    {
      ViewBag.Title = "Register a new user";
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        ApplicationUser user = new ApplicationUser { UserName = model.Email };
        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          foreach (IdentityError error in result.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
          return View(model);
        }
      }
    }
  }
}