using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsGradesCoursesEF.Models;

public class StudentDocument
{
    [Key]
    [ForeignKey("Student")]
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiryDate { get; set; }

    [MaxLength(20)]
    public string DocumentNumber { get; set; } = string.Empty;

    public string PhotoPath { get; set; } = string.Empty; // Keep the document image path for file storage and preview
}

    