namespace AutoSchool_24_7.Page.SignsIngoPage;

public partial class PrioritySignsPage : ContentPage
{
	public PrioritySignsPage()
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