using System.Web.Mvc;using System.Data.Entity;
using System.Linq;using School.Models;
namespace School { [Authorize]public class InstructorsController: Controller {
readonly SchoolDb db=new SchoolDb();protected override void Dispose(bool disposing){if(disposing)db.Dispose();base.Dispose(disposing);}
public ActionResult Index(){return View(db.Instructors.AsNoTracking().ToList());}
public ActionResult Edit(int id=0){var row=id==0?new Instructor():db.Instructors.Find(id);if(row==null)return HttpNotFound();return View(row);}
[HttpPost,ValidateAntiForgeryToken] public ActionResult Edit(Instructor row){if(!ModelState.IsValid)return View(row);
if(row.InstructorID==0)db.Instructors.Add(row);else db.Entry(row).State=EntityState.Modified;db.SaveChanges();return RedirectToAction("Index");}
[HttpPost,ValidateAntiForgeryToken]public ActionResult Delete(int id){var row=db.Instructors.Find(id);if(row==null)return HttpNotFound();db.Instructors.Remove(row);db.SaveChanges();return RedirectToAction("Index");}
}}
