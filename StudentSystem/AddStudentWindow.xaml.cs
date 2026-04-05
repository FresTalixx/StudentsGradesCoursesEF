using System;
using System.Linq;
using System.Windows;
using StudentsGradesCoursesEF.Models;

namespace StudentSystem;

public partial class AddStudentWindow : Window
{
    public AddStudentWindow()
    {
        InitializeComponent();
        LoadGroups();
    }

    private void LoadGroups()
    {
        try
        {
            using var ctx = new StudentsContext();
            var groups = ctx.Groups
                            .OrderBy(g => g.Name)
                            .ToList();
            CbGroup.ItemsSource = groups;
            CbGroup.SelectedIndex = groups.Count > 0 ? 0 : -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load groups: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        var first = TbFirstName.Text.Trim();
        var last = TbLastName.Text.Trim();
        if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
        {
            MessageBox.Show("First and last name are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (CbGroup.SelectedValue is not int groupId)
        {
            MessageBox.Show("Please select a group.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        using var ctx = new StudentsContext();

        var group = ctx.Groups.Find(groupId);
        if (group is null)
        {
            MessageBox.Show("Selected group no longer exists.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var s = new Student
        {
            FirstName = first,
            LastName = last,
            DateOfBirth = DpDob.SelectedDate ?? DateTime.MinValue,
            DateOfApplying = DateTime.Now,
            Group = group
        };

        ctx.Students.Add(s);
        ctx.SaveChanges();
        DialogResult = true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();
}