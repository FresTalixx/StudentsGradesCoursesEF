using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsGradesCoursesEF.Models;

public class Course
{
    public int Id { get; set; }

    // Course no longer tied to a single StudentId; uses many-to-many to Grades
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // many-to-many: Course <-> Grade
    public List<Grade> Grades { get; set; } = new List<Grade>();
}


