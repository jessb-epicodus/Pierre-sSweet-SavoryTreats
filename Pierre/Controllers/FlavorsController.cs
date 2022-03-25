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
  }
}