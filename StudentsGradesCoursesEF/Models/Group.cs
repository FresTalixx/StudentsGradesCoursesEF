using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsGradesCoursesEF.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Rating { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
