using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsGradesCoursesEF.Models;

public class Grade
{
    public int Id { get; set; }

    // foreign keys
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    // payload
    public int NumericGrade { get; set; } = 0;

    // navigations
    public Student? Student { get; set; }
    public Course? Course { get; set; }
}