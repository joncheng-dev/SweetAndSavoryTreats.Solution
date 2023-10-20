using System.Collections.Generic;

namespace SweetAndSavoryTreats.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }
    public string TagName { get; set; }
    public List<FlavorTreat> JoinEntities { get; }
  }
}
