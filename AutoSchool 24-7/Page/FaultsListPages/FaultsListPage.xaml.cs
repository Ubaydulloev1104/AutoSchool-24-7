namespace AutoSchool_24_7.Page.FaultsListPages;

public partial class FaultsListPage : ContentPage
{
	public FaultsListPage()
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