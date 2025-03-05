using AutoSchool_24_7.Page.FaultsListPages;
using AutoSchool_24_7.Page.RoadMarkingsPages;
using AutoSchool_24_7.Page.SignsIngoPage;

namespace AutoSchool_24_7.Page;

public partial class SignsInfoPage : ContentPage
{
    public SignsInfoPage()
    {
        InitializeComponent();
    }

    private async void OnWarningSignsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WarningSignsPage());
    }

    private async void OnPrioritySignsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PrioritySignsPage());
    }

    private async void OnProhibitorySignsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProhibitorySignsPage());
    }

    private async void OnMandatorySignsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MandatorySignsPage());
    }

    private async void OnSpecialRegulationSignsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SpecialRegulationSignsPage());
    }

    private async void OnInformationalSignsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InformationalSignsPage());
    }

    private async void OnServiceSignsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ServiceSignsPage());
    }

    private async void OnAdditionalInfoSignsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdditionalInfoSignsPage());
    }

    private async void OnRoadMarkingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RoadMarkingsPage());
    }

    private async void OnFaultsListClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FaultsListPage());
    }
}