namespace AutoSchool_24_7.Page.SignsIngoPage;

public partial class AdditionalInfoSignsPage : ContentPage
{
	public AdditionalInfoSignsPage()
	{
		InitializeComponent();
        LoadDataAsync();

    }
    private async void LoadDataAsync()
    {
        // �������� �������� ��� ��������, ����� �������� �������� ������� (��������, �������� �� �����/����)
        await Task.Delay(1500);

        // ����� "��������" �������� ������ �������� � ���������� �������
        LoadingPanel.IsVisible = false;
        MainContent.IsVisible = true;
    }
}