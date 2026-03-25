using System.Collections.ObjectModel;
using RandomPerson.Models;
using RandomPerson.Services;

namespace RandomPerson.Views;

public partial class ClassListPage : ContentPage
{
	private string _className;
	private ObservableCollection<Student> _students;

	public ClassListPage(string className)
	{
		InitializeComponent();
		_className = className;
		Title = $"Edycja klasy {className}";
		LoadStudents();
	}

	private void LoadStudents()
	{
		_students = new ObservableCollection<Student>(FileService.LoadClass(_className));
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
		FileService.SaveClass(_className, _students.ToList());
		DisplayAlert("Sukces", "Zapisano listę uczniów.", "OK");
	}
	private void OnBack(object sender, EventArgs e)
	{
		Navigation.PopAsync();
    }	
}