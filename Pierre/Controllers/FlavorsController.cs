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
    [AllowAnonymous]
    public ActionResult Index() {
      ViewBag.Flavors = _db.Flavors
        .Include(flavor => flavor.JoinEntities)
        .ThenInclude(join => join.Treat)
        .ToList();
      return View(_db.Flavors.OrderBy(flavor => flavor.Name).ToList());
    }
    public ActionResult Create(int id) {
      return View();
    }
    [HttpPost]// stretch - add treat within form
    public ActionResult Create(Flavor flavor, int TreatId) {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id= flavor.FlavorId}); 
    }
    [AllowAnonymous]
    public ActionResult Details(int id) {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      var thisFlavor = _db.Flavors
        .Include(flavor => flavor.JoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }
    public ActionResult AddTreat(int id) {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(thisFlavor);
    }
    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId) {
      if (TreatId != 0) {
        if (_db.TreatFlavor.Where(join => join.FlavorId == flavor.FlavorId && join.TreatId == TreatId).ToList().Count() == 0) {
          _db.TreatFlavor.Add(new TreatFlavor(){FlavorId = flavor.FlavorId, TreatId = TreatId} );
          _db.SaveChanges();
        }
      }
      return RedirectToAction("Details", new {id = flavor.FlavorId});
    }
    [HttpPost, ActionName("DeleteTreat")]
    public ActionResult DeleteTreat(int joinId, TreatFlavor treatFlavor) {
      var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Edit(int id) {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }
    [HttpPost]
    public ActionResult Edit(Flavor flavor) {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id= flavor.FlavorId});
    }
    public ActionResult Delete(int id) {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id) {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}