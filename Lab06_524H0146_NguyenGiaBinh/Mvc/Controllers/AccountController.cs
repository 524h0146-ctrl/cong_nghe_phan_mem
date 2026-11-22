using System.Linq;using System.Web.Mvc;using System.Web.Security;using System.Data.SqlClient;
namespace School {public class AccountController:Controller{
public ActionResult Login(){return View();}
[HttpPost,ValidateAntiForgeryToken]public ActionResult Login(string username,string password){using(var db=new SchoolDb()){
var ok=db.Database.SqlQuery<int>("SELECT COUNT(*) FROM tblUsers WHERE Username=@u AND PasswordHash=HASHBYTES('SHA2_256',@p)",new SqlParameter("@u",username??""),new SqlParameter("@p",password??"")).Single();
if(ok==1){FormsAuthentication.SetAuthCookie(username,false);return RedirectToAction("Index","Departments");}
ViewBag.Error="Invalid login";return View();}}}}
