using System.Collections.Generic;

namespace Pierre.Models {
  public class Treat {
    public int TreatId { get; set; }
    public string Name { get; set; }
    public virtual ApplicationUser User { get; set; }
  }
}