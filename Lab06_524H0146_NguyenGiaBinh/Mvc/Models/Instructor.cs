using System;
using System.ComponentModel.DataAnnotations;
namespace School.Models { public class Instructor {
    [Key]
    public int InstructorID { get; set; }
    [Required,MaxLength(100)]
    public string FirstName { get; set; }
    [Required,MaxLength(100)]
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
}}
