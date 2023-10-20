using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetAndSavoryTreats.Models
{
  public class Treat
  {
    public int TreatId { get; set; }
    [RegularExpression(@"^[a-zA-Z0-9. ]+$", ErrorMessage = "Please enter up to 40 alphanumeric characters.")]
    [Required(ErrorMessage = "Please enter a valid name for the treat.")]        
    public string Name { get; set; }
    public List<FlavorTreat> JoinEntities { get; }
  }
}