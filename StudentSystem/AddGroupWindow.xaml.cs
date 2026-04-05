using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using StudentsGradesCoursesEF.Models;

namespace StudentSystem;

public partial class AddGroupWindow : Window
{
    public AddGroupWindow()
    {
        InitializeComponent();
    }



    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        var name = TbName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Group name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!int.TryParse(TbRating.Text.Trim(), out var rating))
        {
            MessageBox.Show("Enter valid numeric rating.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }


        using var ctx = new StudentsContext();
        var g = new Group
        {
            Name = name,
            Rating = rating,
        };

        ctx.Groups.Add(g);
        ctx.SaveChanges();

        DialogResult = true;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();
}