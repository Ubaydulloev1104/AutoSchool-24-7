namespace AutoSchool_24_7.Page.SignsIngoPage;

public partial class ProhibitorySignsPage : ContentPage
{
	public ProhibitorySignsPage()
	{
		InitializeComponent();
        LoadDataAsync();

    }
    private async void LoadDataAsync()
    {
        // Имитация задержки для загрузки, можно заменить реальной логикой (например, загрузка из файла/сети)
        await Task.Delay(1500);

        // После "загрузки" скрываем панель загрузки и показываем контент
        LoadingPanel.IsVisible = false;
        MainContent.IsVisible = true;
    }
}