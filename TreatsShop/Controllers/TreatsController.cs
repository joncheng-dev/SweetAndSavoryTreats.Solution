using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SweetAndSavoryTreats.Models;
using System.Collections.Generic;
using System.Linq; 

namespace SweetAndSavoryTreats.Controllers
{
  public class TreatsController : Controller
  {
    private readonly SweetAndSavoryTreatsContext _db;

    public TreatsController(SweetAndSavoryTreatsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.Title = "List of Treats";
      return View(_db.Treats.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.Title = "Add a Treat";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat newAddition)
    {
      _db.Treats.Add(newAddition);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      ViewBag.Title = "Treat Details";
      Treat targetTreat = _db.Treats.FirstOrDefault(entry => entry.TreatId == id);
      return View(targetTreat);
    }

  }
}