namespace AutoSchool_24_7.Page.AboutPage;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}