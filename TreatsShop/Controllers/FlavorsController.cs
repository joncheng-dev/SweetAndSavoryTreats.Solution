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

    public ActionResult Details(int id)
    {
      ViewBag.Title = "Flavor Details";
      Flavor targetFlavor = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
      return View(targetFlavor);
    }

    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Edit Flavor";
      Flavor flavorToEdit = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
      return View(flavorToEdit);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavorToUpdate)
    {
      _db.Flavors.Update(flavorToUpdate);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = flavorToUpdate.FlavorId });
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
  }
}