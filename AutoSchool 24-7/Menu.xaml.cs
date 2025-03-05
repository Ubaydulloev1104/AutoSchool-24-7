using AutoSchool_24_7.Page;
using AutoSchool_24_7.Page.AboutPage;
using AutoSchool_24_7.Page.FaultsListPages;
using AutoSchool_24_7.Page.HelpPage;
using AutoSchool_24_7.Page.RoadMarkingsPages;
using AutoSchool_24_7.Page.TestPages;

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
    private async void OnRoadMarkingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RoadMarkingsPage());
    }

    private async void OnFaultsListClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FaultsListPage());
    }

    private async void OnTestClicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new TestPage());
    }

    private async void OnHelpClicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new HelpPage());
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new AboutPage());
    }
}