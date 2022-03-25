using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Pierre.Models;

namespace Pierre.Controllers {
  public class HomeController : Controller {
    private readonly PierreContext _db;
    public HomeController(PierreContext db) {
      _db = db;
    }
    [HttpGet("/")]
    public ActionResult Index() {
      ViewBag.Flavors = _db.Flavors.OrderBy(recipe => recipe.Name).ToList();
      ViewBag.Treats = _db.Treats.OrderBy(treat => treat.Name).ToList();
      return View();
    }
  }
}