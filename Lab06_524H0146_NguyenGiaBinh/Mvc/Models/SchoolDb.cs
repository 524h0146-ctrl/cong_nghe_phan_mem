using System.Data.Entity;
using School.Models;
namespace School { public class SchoolDb : DbContext {
public SchoolDb():base("name=School"){}
public DbSet<Department> Departments { get; set; }
public DbSet<Instructor> Instructors { get; set; }
public DbSet<Course> Courses { get; set; }
public DbSet<Student> Students { get; set; }
public DbSet<Enrollment> Enrollments { get; set; }
}}
