using System;
using System.ComponentModel.DataAnnotations;
namespace School.Models { public class Enrollment {
    [Key]
    public int EnrollmentID { get; set; }
    public int StudentID { get; set; }
    public int CourseID { get; set; }
    public decimal Grade { get; set; }
}}
