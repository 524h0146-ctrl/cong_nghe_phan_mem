using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;
using System.Linq;using School.Models;
namespace School { public class InstructorsController: Controller {
readonly SchoolDb db; public InstructorsController(SchoolDb db){this.db=db;}
public ActionResult Index(){return View(db.Instructors.AsNoTracking().ToList());}
public ActionResult Edit(int id=0){var row=id==0?new Instructor():db.Instructors.Find(id);if(row==null)return NotFound();return View(row);}
[HttpPost,ValidateAntiForgeryToken] public ActionResult Edit(Instructor row){if(!ModelState.IsValid)return View(row);
if(row.InstructorID==0)db.Instructors.Add(row);else db.Entry(row).State=EntityState.Modified;db.SaveChanges();return RedirectToAction("Index");}
[HttpPost,ValidateAntiForgeryToken]public ActionResult Delete(int id){var row=db.Instructors.Find(id);if(row==null)return NotFound();db.Instructors.Remove(row);db.SaveChanges();return RedirectToAction("Index");}
}}
