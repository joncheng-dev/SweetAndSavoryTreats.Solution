using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetAndSavoryTreats.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }
    [RegularExpression(@"^[a-zA-Z0-9. ]+$", ErrorMessage = "Please enter up to 40 alphanumeric characters.")]
    [Required(ErrorMessage = "Please enter a valid name for the flavor tag.")]
    [Display(Name = "Tag Name")]    
    public string TagName { get; set; }
    public List<FlavorTreat> JoinEntities { get; }
  }
}
