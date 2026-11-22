using System;using System.Data;
namespace School {public class SchoolService {
    readonly Repository repo=new Repository();
    public bool Login(string user,string pass){return !string.IsNullOrWhiteSpace(user)&&repo.Login(user.Trim(),pass);}
    public DataTable List(string table,int course=0){return repo.Read(table,course);}
    public void Change(string table,string action,string[] values,string[] keys){
        if(action!="Delete")for(int i=table=="tblStudentCourses"?0:1;i<values.Length;i++)
            if(string.IsNullOrWhiteSpace(values[i]))throw new ArgumentException("Missing "+Repository.Fields[table][i]);
        repo.Write(table,action,values,keys);}
}}
