using System.Collections.ObjectModel;
using RandomPerson.Models;
using RandomPerson.Services;

namespace RandomPerson.Views;

public partial class ClassListPage : ContentPage
{
	private Class _classData;
	private ObservableCollection<Student> _students = new();

	public ClassListPage(string className)
	{
		InitializeComponent();
		_classData = FileService.LoadClassData(className);
		Title = $"Edycja klasy {_classData.Name}";
		LoadStudents();
	}

	private void LoadStudents()
	{
		_students = new ObservableCollection<Student>(_classData.Students);
		StudentsListView.ItemsSource = _students;
	}

	private void OnAddStudent(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(NewStudentEntry.Text)) return;
		_students.Add(new Student { Name = NewStudentEntry.Text.Trim() });
		NewStudentEntry.Text = string.Empty;
	}

	private void OnDeleteStudent(object sender, EventArgs e)
	{
		if (sender is Button btn && btn.CommandParameter is Student student)
		{
			_students.Remove(student);
		}
	}

	private void OnSave(object sender, EventArgs e)
	{
		_classData.Students = _students.ToList();
		FileService.SaveClass(_classData);
		DisplayAlert("Sukces", "Zapisano listę uczniów.", "OK");
	}
	private void OnBack(object sender, EventArgs e)
	{
		Navigation.PopAsync();
    }	
}