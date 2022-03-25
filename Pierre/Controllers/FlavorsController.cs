using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Pierre.Models;

namespace Pierre.Controllers {
  [Authorize]
  public class FlavorsController : Controller {
    private readonly PierreContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public FlavorsController(UserManager<ApplicationUser> userManager,PierreContext db) {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index() {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).OrderBy(userFlavors => userFlavors.Name).ToList();
      return View(userFlavors);
    }
    public ActionResult Create(int id) {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Flavor flavor, int TreatId) {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id= flavor.FlavorId});
    }
  }
}