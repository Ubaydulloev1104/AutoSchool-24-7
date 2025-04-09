namespace AutoSchool_24_7.Page.TestPages;

public partial class TestMenuPage : ContentPage
{
	public TestMenuPage()
	{
		InitializeComponent();
	}
    

    private async void OnTestWithTimerClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestTimePage()); // Страница с таймером
    }

    private async void OnTestWithoutTimerClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestPage()); // Страница без таймера
    }

    private async void OnHelpClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestHelpPage()); // Справка
    }
}