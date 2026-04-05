using System.Windows;
using StudentsGradesCoursesEF.Models;

namespace StudentSystem;

public partial class AddCourseWindow : Window
{
    public AddCourseWindow()
    {
        InitializeComponent();
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        var name = TbName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Course name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        using var ctx = new StudentsContext();
        var c = new Course
        {
            Name = name,
            Description = TbDescription.Text.Trim()
        };
        ctx.Courses.Add(c);
        ctx.SaveChanges();
        DialogResult = true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();
}