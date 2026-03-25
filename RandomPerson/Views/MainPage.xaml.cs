namespace RandomPerson;

using RandomPerson.Services;
using RandomPerson.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshClasses();
        GenerateLuckyNumber();
    }

    private void RefreshClasses()
    {
        ClassPicker.ItemsSource = FileService.GetClassNames();
        if (ClassPicker.ItemsSource.Count > 0)
            ClassPicker.SelectedIndex = 0;
    }

    private void GenerateLuckyNumber()
    {
        int number = new Random().Next(1, 36);
        LuckyNumberLabel.Text = number.ToString();
        DrawService.LuckyNumber = number;
    }
    
    private void OnDrawLuckyNumber(object sender, EventArgs e) =>
        GenerateLuckyNumber();

    private async void OnNewClass(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Nowa klasa", "Podaj nazwę klasy (np. 2A):");
        if (string.IsNullOrWhiteSpace(name)) return;
        FileService.SaveClass(name.Trim(), new());
        RefreshClasses();
    }

    private async void OnEditClass(object sender, EventArgs e)
    {
        if (ClassPicker.SelectedItem is not string className) return;
        await Navigation.PushAsync(new ClassListPage(className));
    }

    private async void OnDraw(object sender, EventArgs e)
    {
        if (ClassPicker.SelectedItem is not string className) return;
        await Navigation.PushAsync(new DrawPage(className));
    }
}