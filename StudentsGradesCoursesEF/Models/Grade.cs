using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsGradesCoursesEF.Models;

public class Grade
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int NumericGrade { get; set; } = 0;

    // optional navigation to Student
    public Student? Student { get; set; }

    // many-to-many: Grade <-> Course
    public List<Course> Courses { get; set; } = new List<Course>();
}