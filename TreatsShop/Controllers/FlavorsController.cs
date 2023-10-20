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
      if(ModelState.IsValid)
      {
        _db.Flavors.Add(newlyAdded);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.Title = "Add a Flavor Tag";
        return View(newlyAdded);
      }
    }

    public ActionResult Details(int id)
    {
      ViewBag.Title = "Flavor Tag Details";
      Flavor targetFlavor = _db.Flavors.Include(entry => entry.JoinEntities)
                                       .ThenInclude(join => join.Treat)
                                       .FirstOrDefault(entry => entry.FlavorId == id);
      return View(targetFlavor);
    }

    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Edit Flavor Tag";
      Flavor flavorToEdit = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
      return View(flavorToEdit);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavorToUpdate)
    {
      if(ModelState.IsValid)
      {
        _db.Flavors.Update(flavorToUpdate);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = flavorToUpdate.FlavorId });
      }
      else
      {
        ViewBag.Title = "Edit Flavor Tag";
        return View(flavorToUpdate);
      }
    }

    public ActionResult Delete(int id)
    {
      ViewBag.Title = "Delete Flavor";
      Flavor flavorToDelete = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
      return View(flavorToDelete);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor flavorToDelete = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
      _db.Flavors.Remove(flavorToDelete);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat(int id)
    {
      ViewBag.Title = "Add a Treat to This Flavor Tag";
      Flavor targetFlavor = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
      ViewBag.TreatsList = _db.Treats.ToList();
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(targetFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor targetFlavor, int treatId)
    {
      #nullable enable
      FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => join.FlavorId == targetFlavor.FlavorId && join.TreatId == treatId);
      #nullable disable
      if(joinEntity == null && treatId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = targetFlavor.FlavorId, TreatId = treatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = targetFlavor.FlavorId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreats.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.FlavorId });
    }
  }
}