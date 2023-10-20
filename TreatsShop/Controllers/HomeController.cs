using Microsoft.AspNetCore.Mvc;
using SweetAndSavoryTreats.Models;
using System.Collections.Generic;
using System.Linq;

namespace SweetAndSavoryTreats.Controllers
{
  public class HomeController : Controller
  {
    private readonly SweetAndSavoryTreatsContext _db;
    
    public HomeController(SweetAndSavoryTreatsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.Title = "Pierre's Sweet and Savory Treats";
      Flavor[] flavorsArray = _db.Flavors.ToArray();
      Treat[] treatsArray = _db.Treats.ToArray();
      Dictionary<string,object[]> model = new Dictionary<string,object[]>();
      model.Add("flavors", flavorsArray);
      model.Add("treats", treatsArray);
      return View(model);
    }
  }
}