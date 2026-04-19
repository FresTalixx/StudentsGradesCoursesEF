using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsGradesCoursesEF.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfApplying { get; set; }
    public Group Group { get; set; } = null!;
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public StudentDocument? Document { get; set; }
    public string PhotoPath { get; set; } = string.Empty;
}
