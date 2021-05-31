using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Machine> model = _db.Machines.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      _db.Entry(machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult Delete(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddLicense(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddLicense(Machine machine, int EngineerId)
    {
      EngineerMachine joinTableEntry = null;
      try
      {
        joinTableEntry = _db.EngineerMachine.Where(
          entry => entry.MachineId == machine.MachineId
          && entry.EngineerId == EngineerId
        ).Single();
      }
      catch
      {
        // Console.WriteLine("Doesn't exist in the 'EngineerMachine' table");
      }

      if (EngineerId != 0 && joinTableEntry == null)
      {
        _db.EngineerMachine.Add(new EngineerMachine()
        {
          MachineId = machine.MachineId,
          EngineerId = EngineerId
        });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = machine.MachineId});
    }

    public ActionResult RemoveLicense(int id)
    {
      EngineerMachine joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == id);
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == joinEntry.MachineId);
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == joinEntry.EngineerId);
      ViewBag.Machine = thisMachine;
      ViewBag.Engineer = thisEngineer;
      return View(joinEntry);
    }

    [HttpPost, ActionName("RemoveLicense")]
    public ActionResult RemoveLicenseConfirmed(int joinId)
    {
      EngineerMachine joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.MachineId });
    }

  }
}
