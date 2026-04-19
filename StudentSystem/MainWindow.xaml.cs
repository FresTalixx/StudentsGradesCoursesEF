using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using StudentsGradesCoursesEF.Models;

namespace StudentSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> students = new();
        private ObservableCollection<Course> courses = new();
        private ObservableCollection<Grade> grades = new();
        private ObservableCollection<Group> groups = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using var ctx = new StudentsContext();
            ctx.Database.Migrate();

            students = new ObservableCollection<Student>(ctx.Students.AsNoTracking().ToList());
            courses = new ObservableCollection<Course>(ctx.Courses.AsNoTracking().ToList());
            grades = new ObservableCollection<Grade>(ctx.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .AsNoTracking()
                .OrderByDescending(g => g.Id)
                .ToList());
            groups = new ObservableCollection<Group>(ctx.Groups
                .Include(g => g.Students)
                .AsNoTracking()
                .ToList());


            DgStudents.ItemsSource = students;
            DgCourses.ItemsSource = courses;
            DgGrades.ItemsSource = grades;
            DgGroups.ItemsSource = groups;
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddStudentWindow();
            if (w.ShowDialog() == true)
                LoadData();
        }

        private void BtnManageDocument_Click(object sender, RoutedEventArgs e)
        {
            if (DgStudents.SelectedItem is not Student student)
            {
                MessageBox.Show("Select a student first.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var w = new StudentDocumentWindow(student.Id);
            if (w.ShowDialog() == true)
                LoadData();
        }

        private void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddCourseWindow();
            if (w.ShowDialog() == true)
                LoadData();
        }

        private void BtnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddGradeWindow();
            if (w.ShowDialog() == true)
                LoadData();
        }

        private void BtnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddGroupWindow();
            if (w.ShowDialog() == true)
                LoadData();
        }
    }
}