using System.Web.Mvc;using Microsoft.Reporting.WebForms;using System.Configuration;using System.Data.SqlClient;
using System;using System.Data;using System.IO;
namespace School {
[Authorize]public class ReportsController:Controller {

public ActionResult Index(int course=0,DateTime? from=null,DateTime? to=null,string format="html"){
 using(var c=new SqlConnection(ConfigurationManager.ConnectionStrings["School"].ConnectionString)){
 var courses=new DataTable();using(var a=new SqlDataAdapter("SELECT CourseID,Title FROM Courses ORDER BY Title",c))a.Fill(courses);ViewBag.Courses=courses;
 var rows=new DataTable();string query="SELECT s.FirstName+' '+s.LastName AS Name,c.Title AS Detail FROM Students s JOIN Enrollments e ON e.StudentID=s.StudentID JOIN Courses c ON c.CourseID=e.CourseID WHERE c.CourseID=@course";
if(from.HasValue&&to.HasValue) query="SELECT Name,CONVERT(varchar(10),BirthDate,23) AS Detail FROM Student WHERE BirthDate BETWEEN @from AND @to";
 using(var a=new SqlDataAdapter(query,c)){a.SelectCommand.Parameters.AddWithValue("@course",course);a.SelectCommand.Parameters.Add("@from",SqlDbType.Date).Value=(object)from??DBNull.Value;a.SelectCommand.Parameters.Add("@to",SqlDbType.Date).Value=(object)to??DBNull.Value;a.Fill(rows);}
 if(format=="pdf"){using(var r=new LocalReport()){r.ReportPath=Server.MapPath("~/Reports/StudentReport.rdlc");r.DataSources.Add(new ReportDataSource("Students",rows));return File(r.Render("PDF"),"application/pdf","Students.pdf");}}
 return View(rows);}}
}}
