using Microsoft.AspNetCore.Mvc;using Microsoft.Reporting.NETCore;using Microsoft.Data.SqlClient;using Microsoft.Extensions.Configuration;
using System;using System.Data;using System.IO;
namespace School {
public class ReportsController:Controller {
readonly IConfiguration config;public ReportsController(IConfiguration config){this.config=config;}
public ActionResult Index(int course=0,DateTime? from=null,DateTime? to=null,string format="html"){
 using(var c=new SqlConnection(config.GetConnectionString("School"))){
 var courses=new DataTable();using(var a=new SqlDataAdapter("SELECT CourseID,Title FROM Courses ORDER BY Title",c))a.Fill(courses);ViewBag.Courses=courses;
 var rows=new DataTable();string query="SELECT s.FirstName+' '+s.LastName AS Name,c.Title AS Detail FROM Students s JOIN Enrollments e ON e.StudentID=s.StudentID JOIN Courses c ON c.CourseID=e.CourseID WHERE c.CourseID=@course";

 using(var a=new SqlDataAdapter(query,c)){a.SelectCommand.Parameters.AddWithValue("@course",course);a.SelectCommand.Parameters.Add("@from",SqlDbType.Date).Value=(object)from??DBNull.Value;a.SelectCommand.Parameters.Add("@to",SqlDbType.Date).Value=(object)to??DBNull.Value;a.Fill(rows);}
 if(format=="pdf"){using(var r=new LocalReport()){r.ReportPath=Path.Combine(Directory.GetCurrentDirectory(),"Reports","StudentReport.rdlc");r.DataSources.Add(new ReportDataSource("Students",rows));return File(r.Render("PDF"),"application/pdf","Students.pdf");}}
 return View(rows);}}
}}
