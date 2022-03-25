using System.Collections.Generic;

namespace Pierre.Models {
  public class Flavor {
    public int FlavorId { get; set; }
    public string Name { get; set; }
    public virtual ApplicationUser User { get; set; }
  }
}