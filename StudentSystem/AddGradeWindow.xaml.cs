using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using StudentsGradesCoursesEF.Models;

namespace StudentSystem;

public partial class AddGradeWindow : Window
{
    public AddGradeWindow()
    {
        InitializeComponent();
        LoadLookups();
    }

    private void LoadLookups()
    {
        using var ctx = new StudentsContext();
        CbStudents.ItemsSource = ctx.Students.AsNoTracking().ToList();
        CbCourses.ItemsSource = ctx.Courses.AsNoTracking().ToList();
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        var student = CbStudents.SelectedItem as Student;
        var course = CbCourses.SelectedItem as Course;
        if (student == null || course == null)
        {
            MessageBox.Show("Select both student and course.");
            return;
        }

        if (!int.TryParse(TbNumeric.Text.Trim(), out var numeric))
        {
            MessageBox.Show("Enter valid numeric grade.");
            return;
        }

        using var ctx = new StudentsContext();
        var g = new Grade
        {
            StudentId = student.Id,
            CourseId = course.Id,
            NumericGrade = numeric
        };
        ctx.Grades.Add(g);
        ctx.SaveChanges();
        DialogResult = true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();
}