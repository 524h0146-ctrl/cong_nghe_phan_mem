using System;
using System.ComponentModel.DataAnnotations;
namespace School.Models { public class Department {
    [Key]
    public int DepartmentID { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; }
    public decimal Budget { get; set; }
    public DateTime StartDate { get; set; }
}}
