using RandomPerson.Models;
using RandomPerson.Services;

namespace RandomPerson.Views;

public partial class ClassListPage : ContentPage
{
   private readonly Class _classData;

	public ClassListPage(string className)
	{
		InitializeComponent();
		_classData = FileService.LoadClassData(className);
		Title = $"Edycja klasy {_classData.Name}";
     RefreshStudentsView();
	}

 private void RefreshStudentsView()
	{
     StudentsListView.ItemsSource = null;
		StudentsListView.ItemsSource = _classData.Students;
	}

	private void OnAddStudent(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(NewStudentEntry.Text)) return;
      _classData.Students.Add(new Student { Name = NewStudentEntry.Text.Trim() });
		NewStudentEntry.Text = string.Empty;
       RefreshStudentsView();
	}

	private void OnDeleteStudent(object sender, EventArgs e)
	{
		if (sender is Button btn && btn.CommandParameter is Student student)
		{
          _classData.Students.Remove(student);
			RefreshStudentsView();
		}
	}

	private void OnSave(object sender, EventArgs e)
	{
		FileService.SaveClass(_classData);
		DisplayAlert("Sukces", "Zapisano listę uczniów.", "OK");
	}
	private void OnBack(object sender, EventArgs e)
	{
		Navigation.PopAsync();
    }	
}