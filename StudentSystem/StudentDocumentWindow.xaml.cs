using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using StudentsGradesCoursesEF.Models;

namespace StudentSystem;

public partial class StudentDocumentWindow : Window
{
    private readonly int _studentId;
    private string? _selectedImagePath;
    private string? _currentImagePath;

    public StudentDocumentWindow(int studentId)
    {
        InitializeComponent();
        _studentId = studentId;
        LoadData();
    }

    private void LoadData()
    {
        using var ctx = new StudentsContext();
        var student = ctx.Students.FirstOrDefault(s => s.Id == _studentId);
        if (student is null)
        {
            MessageBox.Show("Selected student was not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            DialogResult = false;
            Close();
            return;
        }

        TxtStudentInfo.Text = $"{student.FirstName} {student.LastName} (Id: {student.Id})";

        var document = ctx.StudentDocuments.FirstOrDefault(d => d.StudentId == _studentId);
        if (document is null)
        {
            DpIssuedAt.SelectedDate = DateTime.Today;
            DpExpiryDate.SelectedDate = DateTime.Today.AddYears(1);
            _currentImagePath = student.PhotoPath;
            ShowPreview(_currentImagePath);
            return;
        }

        TbDocumentNumber.Text = document.DocumentNumber;
        DpIssuedAt.SelectedDate = document.IssuedAt == default ? DateTime.Today : document.IssuedAt;
        DpExpiryDate.SelectedDate = document.ExpiryDate == default ? DateTime.Today.AddYears(1) : document.ExpiryDate;
        _currentImagePath = !string.IsNullOrWhiteSpace(document.PhotoPath) ? document.PhotoPath : student.PhotoPath;
        ShowPreview(_currentImagePath);
    }

    private void BtnBrowsePhoto_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Select student image",
            Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
            Multiselect = false
        };

        if (dialog.ShowDialog() == true)
        {
            _selectedImagePath = dialog.FileName;
            ShowPreview(_selectedImagePath);
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        var documentNumber = TbDocumentNumber.Text.Trim();
        if (string.IsNullOrWhiteSpace(documentNumber))
        {
            MessageBox.Show("Document number is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (DpIssuedAt.SelectedDate is null || DpExpiryDate.SelectedDate is null)
        {
            MessageBox.Show("Issued and expiry dates are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (DpExpiryDate.SelectedDate < DpIssuedAt.SelectedDate)
        {
            MessageBox.Show("Expiry date must be after issued date.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        using var ctx = new StudentsContext();
        var student = ctx.Students.FirstOrDefault(s => s.Id == _studentId);
        if (student is null)
        {
            MessageBox.Show("Selected student was not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var document = ctx.StudentDocuments.FirstOrDefault(d => d.StudentId == _studentId);
        if (document is null)
        {
            document = new StudentDocument
            {
                StudentId = _studentId,
                Student = student,
                Name = "Student document"
            };
            ctx.StudentDocuments.Add(document);
        }

        var existingImagePath = !string.IsNullOrWhiteSpace(document.PhotoPath) ? document.PhotoPath : student.PhotoPath;
        var storedImagePath = SaveImageIfNeeded(documentNumber, existingImagePath, _selectedImagePath);

        document.DocumentNumber = documentNumber;
        document.IssuedAt = DpIssuedAt.SelectedDate.Value;
        document.ExpiryDate = DpExpiryDate.SelectedDate.Value;
        document.PhotoPath = storedImagePath;
        student.PhotoPath = storedImagePath;

        ctx.SaveChanges();
        DialogResult = true;
    }

    private string SaveImageIfNeeded(string documentNumber, string? existingPath, string? selectedPath)
    {
        var sourcePath = !string.IsNullOrWhiteSpace(selectedPath) ? selectedPath : existingPath;
        if (string.IsNullOrWhiteSpace(sourcePath))
        {
            return string.Empty;
        }

        if (!File.Exists(sourcePath))
        {
            return string.Empty;
        }

        var extension = Path.GetExtension(sourcePath).ToLowerInvariant();
        if (extension is not ".jpg" and not ".jpeg" and not ".png")
        {
            throw new InvalidOperationException("Only .jpg, .jpeg and .png files are allowed.");
        }

        var documentsFolder = Path.Combine(AppContext.BaseDirectory, "documents");
        Directory.CreateDirectory(documentsFolder);

        var safeDocumentNumber = new string(documentNumber
            .Select(ch => Path
            .GetInvalidFileNameChars()
            .Contains(ch) ? '_' : ch)
            .ToArray());

        var destinationPath = Path.Combine(documentsFolder, $"student_{_studentId}_{safeDocumentNumber}{extension}");

        var fullSource = Path.GetFullPath(sourcePath);
        var fullDestination = Path.GetFullPath(destinationPath);

        if (!string.Equals(fullSource, fullDestination, StringComparison.OrdinalIgnoreCase))
        {
            File.Copy(fullSource, fullDestination, true);
        }

        if (!string.IsNullOrWhiteSpace(existingPath) &&
            !string.Equals(Path.GetFullPath(existingPath), fullDestination, StringComparison.OrdinalIgnoreCase) &&
            File.Exists(existingPath))
        {
            File.Delete(existingPath);
        }

        return destinationPath;
    }

    private void ShowPreview(string? imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
        {
            ImgPreview.Source = null;
            return;
        }

        var image = new BitmapImage();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.UriSource = new Uri(imagePath, UriKind.Absolute);
        image.EndInit();
        image.Freeze();
        ImgPreview.Source = image;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();
}