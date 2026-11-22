using Microsoft.EntityFrameworkCore;
using School.Models;
namespace School { public class SchoolDb : DbContext {
public SchoolDb(DbContextOptions<SchoolDb> options):base(options){}
public DbSet<Department> Departments { get; set; }
public DbSet<Instructor> Instructors { get; set; }
public DbSet<Course> Courses { get; set; }
public DbSet<Student> Students { get; set; }
public DbSet<Enrollment> Enrollments { get; set; }
protected override void OnModelCreating(ModelBuilder m){
m.Entity<Department>().ToTable("Departments");
m.Entity<Department>().Property(x=>x.Budget).HasPrecision(18,2);
m.Entity<Department>().Property(x=>x.StartDate).HasColumnType("date");
m.Entity<Instructor>().ToTable("Instructors");
m.Entity<Instructor>().Property(x=>x.HireDate).HasColumnType("date");
m.Entity<Course>().ToTable("Courses");
m.Entity<Course>().HasOne<Department>().WithMany().HasForeignKey(x=>x.DepartmentID).OnDelete(DeleteBehavior.Restrict);
m.Entity<Student>().ToTable("Students");
m.Entity<Student>().Property(x=>x.EnrollmentDate).HasColumnType("date");
m.Entity<Enrollment>().ToTable("Enrollments");
m.Entity<Enrollment>().HasOne<Student>().WithMany().HasForeignKey(x=>x.StudentID).OnDelete(DeleteBehavior.Restrict);
m.Entity<Enrollment>().HasOne<Course>().WithMany().HasForeignKey(x=>x.CourseID).OnDelete(DeleteBehavior.Restrict);
m.Entity<Enrollment>().Property(x=>x.Grade).HasPrecision(18,2);
}
}}
