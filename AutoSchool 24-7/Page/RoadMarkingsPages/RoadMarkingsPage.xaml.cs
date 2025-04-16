namespace AutoSchool_24_7.Page.RoadMarkingsPages;

public partial class RoadMarkingsPage : ContentPage
{
	public RoadMarkingsPage()
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