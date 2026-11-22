using System;
using System.ComponentModel.DataAnnotations;
namespace School.Models { public class Course {
    [Key]
    public int CourseID { get; set; }
    [Required,MaxLength(100)]
    public string Title { get; set; }
    public int Credits { get; set; }
    public int DepartmentID { get; set; }
}}
