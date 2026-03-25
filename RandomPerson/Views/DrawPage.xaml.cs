using RandomPerson.Models;
using RandomPerson.Services;

namespace RandomPerson.Views;

public partial class DrawPage : ContentPage
{
	private string _className;
	private List<Student> _students;

	public DrawPage(string className)
	{
		InitializeComponent();
		_className = className;
		Title = $"Losowanie - {className}";
		_students = FileService.LoadClass(_className);
	}

	private void OnDrawStudent(object sender, EventArgs e)
	{
		var drawn = DrawService.DrawStudent(_students);
		if (drawn != null)
		{
			DrawnStudentLabel.Text = drawn.Name;
			FileService.SaveClass(_className, _students);
		}
		else
		{
			DrawnStudentLabel.Text = "Brak dostępnych uczniów!";
		}
	}
	private void OnBack(object sender, EventArgs e)
	{
		Navigation.PopAsync();
    }
}