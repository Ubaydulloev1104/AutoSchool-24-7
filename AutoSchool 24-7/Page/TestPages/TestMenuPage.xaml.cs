namespace AutoSchool_24_7.Page.TestPages;

public partial class TestMenuPage : ContentPage
{
	public TestMenuPage()
	{
		InitializeComponent();
	}
    

    private async void OnTestWithTimerClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestTimePage()); // �������� � ��������
    }

    private async void OnTestWithoutTimerClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestPage()); // �������� ��� �������
    }

    private async void OnHelpClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestHelpPage()); // �������
    }
}