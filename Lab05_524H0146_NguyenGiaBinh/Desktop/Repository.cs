using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
namespace School {
public class LoginDb : System.Data.Entity.DbContext {
    static LoginDb(){System.Data.Entity.Database.SetInitializer<LoginDb>(null);}
    public LoginDb():base("name=School"){}
}
public class Repository {
    static SqlConnection Connect(){return new SqlConnection(ConfigurationManager.ConnectionStrings["School"].ConnectionString);}
    public bool Login(string name,string pass){
        using(var db=new LoginDb()){
            var u=new SqlParameter("@u",SqlDbType.NVarChar,50){Value=name};
            var p=new SqlParameter("@p",SqlDbType.NVarChar,200){Value=pass};
            return db.Database.SqlQuery<int>("SELECT COUNT(*) FROM tblUsers WHERE Username=@u AND PasswordHash=HASHBYTES('SHA2_256',@p)",u,p).Single()==1;}}
    public static readonly Dictionary<string,string[]> Fields = new Dictionary<string,string[]> {
{"tblStudents",new[]{"StudentID","StudentName","DateOfBirth","City","Age","YearOfEnroll","Major","GPA"}},
{"tblCourses",new[]{"CourseID","CourseName","Credits","DepartmentID","InstructorID"}},
{"tblDepartments",new[]{"DepartmentID","DepartmentName"}},
{"tblInstructors",new[]{"InstructorID","InstructorName","DepartmentID"}},
{"Suppliers",new[]{"SupplierID","Name"}},
{"Products",new[]{"ProductID","Name","SupplierID"}},
{"Customers",new[]{"Id","Name","Email"}},
{"tblStudentCourses",new[]{"StudentID","CourseID"}}
    };
    public DataTable Read(string table,int course=0){
        var columns=Fields[table];string sql="SELECT "+string.Join(",",columns)+" FROM "+table;
        if(table=="tblStudentCourses"&&course>0)sql+=" WHERE CourseID=@course";
        using(var c=Connect())using(var a=new SqlDataAdapter(sql,c)){a.SelectCommand.Parameters.AddWithValue("@course",course);
            var t=new DataTable();a.Fill(t);return t;}}
    public void Write(string table,string mode,string[] values,string[] original){
        var fields=Fields[table];bool link=table=="tblStudentCourses";var keys=link?fields:fields.Take(1).ToArray();
        var edit=link?fields:fields.Skip(1).ToArray();
        string where=string.Join(" AND ",keys.Select(k=>k+"=@old"+k));
        string sql;
        if(mode=="Add")sql="INSERT INTO "+table+" ("+string.Join(",",edit)+") VALUES ("+string.Join(",",edit.Select(k=>"@"+k))+")";
        else if(mode=="Update")sql="UPDATE "+table+" SET "+string.Join(",",edit.Select(k=>k+"=@"+k))+" WHERE "+where;
        else if(mode=="Delete")sql="DELETE FROM "+table+" WHERE "+where;
        else throw new ArgumentException("Unknown operation");
        using(var c=Connect())using(var q=new SqlCommand(sql,c)){
            for(int i=0;i<fields.Length;i++)q.Parameters.AddWithValue("@"+fields[i],values[i]??"");
            for(int i=0;i<keys.Length;i++)q.Parameters.AddWithValue("@old"+keys[i],original[i]??"");
            c.Open();if(q.ExecuteNonQuery()!=1)throw new InvalidOperationException("Record was changed. Refresh.");}}
}}
