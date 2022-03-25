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
  public class TreatsController : Controller {
    private readonly PierreContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public TreatsController(UserManager<ApplicationUser> userManager,PierreContext db) {
      _userManager = userManager;
      _db = db;
    }
    [AllowAnonymous]
      public ActionResult Index() {
      ViewBag.Treats = _db.Treats
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .ToList();
      return View(_db.Treats.OrderBy(treat => treat.Name).ToList());
    }
    public ActionResult Create() {
      return View();
    }
    [HttpPost]// stretch - add flavor within form
    public ActionResult Create(Treat treat) {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id= treat.TreatId});
    }
    [AllowAnonymous]
    public ActionResult Details(int id) {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      var thisTreat = _db.Treats
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
    public ActionResult AddFlavor(int id) {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }
    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId) {
      if (FlavorId != 0) {
        if (_db.TreatFlavor.Where(join => join.TreatId == treat.TreatId && join.FlavorId == FlavorId).ToList().Count() == 0) {
          _db.TreatFlavor.Add(new TreatFlavor(){TreatId = treat.TreatId, FlavorId = FlavorId} );
          _db.SaveChanges();
        }
      }
      return RedirectToAction("Details", new {id= treat.TreatId});
    }
    [HttpPost, ActionName("DeleteFlavor")]// stretch - stay on Flavors/Details/Id
    public ActionResult DeleteFlavor(int joinId, TreatFlavor treatFlavor) {
      var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index"); 
    }
    public ActionResult Edit(int id) {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
    [HttpPost]
    public ActionResult Edit(Treat treat) {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id= treat.TreatId});
    }
    public ActionResult Delete(int id) {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id) {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}