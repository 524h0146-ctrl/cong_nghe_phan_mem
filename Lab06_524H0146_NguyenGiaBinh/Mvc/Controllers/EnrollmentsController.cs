using System.Web.Mvc;using System.Data.Entity;
using System.Linq;using School.Models;
namespace School { [Authorize]public class EnrollmentsController: Controller {
readonly SchoolDb db=new SchoolDb();protected override void Dispose(bool disposing){if(disposing)db.Dispose();base.Dispose(disposing);}
public ActionResult Index(){return View(db.Enrollments.AsNoTracking().ToList());}
public ActionResult Edit(int id=0){var row=id==0?new Enrollment():db.Enrollments.Find(id);if(row==null)return HttpNotFound();return View(row);}
[HttpPost,ValidateAntiForgeryToken] public ActionResult Edit(Enrollment row){if(!ModelState.IsValid)return View(row);
if(row.EnrollmentID==0)db.Enrollments.Add(row);else db.Entry(row).State=EntityState.Modified;db.SaveChanges();return RedirectToAction("Index");}
[HttpPost,ValidateAntiForgeryToken]public ActionResult Delete(int id){var row=db.Enrollments.Find(id);if(row==null)return HttpNotFound();db.Enrollments.Remove(row);db.SaveChanges();return RedirectToAction("Index");}
}}
