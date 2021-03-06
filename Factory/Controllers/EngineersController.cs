using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult Edit(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      _db.Entry(engineer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddLicense(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddLicense(Engineer engineer, int MachineId)
    {
      EngineerMachine joinTableEntry = null;
      try
      {
        joinTableEntry = _db.EngineerMachine.Where(
          entry => entry.EngineerId == engineer.EngineerId
          && entry.MachineId == MachineId
        ).Single();
      }
      catch
      {
        // Console.WriteLine("Doesn't exist in the 'EngineerMachine' table");
      }

      if (MachineId != 0 && joinTableEntry == null)
      {
        _db.EngineerMachine.Add(new EngineerMachine()
        {
          EngineerId = engineer.EngineerId,
          MachineId = MachineId
        });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = engineer.EngineerId});
    }

    public ActionResult RemoveLicense(int id)
    {
      EngineerMachine joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == id);
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == joinEntry.EngineerId);
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == joinEntry.MachineId);
      ViewBag.Engineer = thisEngineer;
      ViewBag.Machine = thisMachine;
      return View(joinEntry);
    }

    [HttpPost, ActionName("RemoveLicense")]
    public ActionResult RemoveLicenseConfirmed(int joinId)
    {
      EngineerMachine joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.EngineerId });
    }

  }
}