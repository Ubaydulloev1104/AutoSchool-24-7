namespace AutoSchool_24_7.Page.TestPages;

public partial class TestHelpPage : ContentPage
{
	public TestHelpPage()
	{
		InitializeComponent();
	}
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}