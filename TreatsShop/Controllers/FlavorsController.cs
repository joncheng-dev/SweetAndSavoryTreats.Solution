using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SweetAndSavoryTreats.Models;
using System.Collections.Generic;
using System.Linq; 

namespace SweetAndSavoryTreats.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly SweetAndSavoryTreatsContext _db;

    public FlavorsController(SweetAndSavoryTreatsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.Title = "List of Flavor Tags";
      return View(_db.Flavors.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.Title = "Add a Flavor Tag";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor newlyAdded)
    {
      _db.Flavors.Add(newlyAdded);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}