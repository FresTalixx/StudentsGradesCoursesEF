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
    public List<Grade> Grades { get; set; } = new List<Grade>();
    public List<Course> Courses { get; set; } = new List<Course>();
}
