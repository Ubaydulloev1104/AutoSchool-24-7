using AutoSchool_24_7.Page;

namespace AutoSchool_24_7;

public partial class Menu : ContentPage
{
	public Menu()
	{
        InitializeComponent();
	}
    private async void OnSignsInfoClicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new SignsInfoPage());
    }

    private async void OnTestClicked(object sender, EventArgs e)
    {
       // await Navigation.PushAsync(new TestPage());
    }

    private async void OnHelpClicked(object sender, EventArgs e)
    {
       // await Navigation.PushAsync(new HelpPage());
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
       // await Navigation.PushAsync(new AboutPage());
    }
}