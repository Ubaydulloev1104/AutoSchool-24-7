namespace AutoSchool_24_7.Page.SignsIngoPage;

public partial class InformationalSignsPage : ContentPage
{
	public InformationalSignsPage()
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