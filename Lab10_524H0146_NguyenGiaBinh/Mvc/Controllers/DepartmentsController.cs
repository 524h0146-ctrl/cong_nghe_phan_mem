using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;
using System.Linq;using School.Models;
namespace School { public class DepartmentsController: Controller {
readonly SchoolDb db; public DepartmentsController(SchoolDb db){this.db=db;}
public ActionResult Index(){return View(db.Departments.AsNoTracking().ToList());}
public ActionResult Edit(int id=0){var row=id==0?new Department():db.Departments.Find(id);if(row==null)return NotFound();return View(row);}
[HttpPost,ValidateAntiForgeryToken] public ActionResult Edit(Department row){if(!ModelState.IsValid)return View(row);
if(row.DepartmentID==0)db.Departments.Add(row);else db.Entry(row).State=EntityState.Modified;db.SaveChanges();return RedirectToAction("Index");}
[HttpPost,ValidateAntiForgeryToken]public ActionResult Delete(int id){var row=db.Departments.Find(id);if(row==null)return NotFound();db.Departments.Remove(row);db.SaveChanges();return RedirectToAction("Index");}
}}
