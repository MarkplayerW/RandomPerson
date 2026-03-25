using RandomPerson.Models;
using RandomPerson.Services;

namespace RandomPerson.Views;

public partial class DrawPage : ContentPage
{
	private Class _classData;

	public DrawPage(string className)
	{
		InitializeComponent();
		_classData = FileService.LoadClassData(className);
		Title = $"Losowanie - {_classData.Name}";
	}

	private void OnDrawStudent(object sender, EventArgs e)
	{
		var drawn = DrawService.DrawStudent(_classData.Students);
		if (drawn != null)
		{
			DrawnStudentLabel.Text = drawn.Name;
			FileService.SaveClass(_classData);
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