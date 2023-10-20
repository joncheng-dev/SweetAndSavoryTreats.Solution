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

    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Edit Treat Details";
      Treat treatToEdit = _db.Treats.FirstOrDefault(entry => entry.TreatId == id);
      return View(treatToEdit);
    }

    [HttpPost]
    public ActionResult Edit(Treat targetTreatToEdit)
    {
      _db.Treats.Update(targetTreatToEdit);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = targetTreatToEdit.TreatId });
    }

    public ActionResult Delete(int id)
    {
      ViewBag.Title = "Delete Treat";
      Treat targetTreat = _db.Treats.FirstOrDefault(entry => entry.TreatId == id);
      return View(targetTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Treat treatToDelete = _db.Treats.FirstOrDefault(entry => entry.TreatId == id);
      _db.Treats.Remove(treatToDelete);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddFlavorTag(int id)
    {
      ViewBag.Title = "Add Flavor Tags";
      Treat treatToAddTagsTo = _db.Treats.FirstOrDefault(entry => entry.TreatId == id);
      ViewBag.FlavorsList = _db.Flavors.ToList();
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "TagName");
      return View(treatToAddTagsTo);
    }

    [HttpPost]
    public ActionResult AddFlavorTag(Treat entry, int flavorId)
    {
      #nullable enable
      FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => join.TreatId == entry.TreatId && join.FlavorId == flavorId);
      #nullable disable
      if (joinEntity == null && flavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { TreatId = entry.TreatId, FlavorId = flavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = entry.TreatId });
    }
  }
}