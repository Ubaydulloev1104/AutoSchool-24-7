
namespace AutoSchool_24_7.Pages.Base;

public class BaseContentPage : ContentPage
{
    protected Grid MainLayout;
    protected StackLayout LoadingPanel;
    protected View PageContent;

    public BaseContentPage()
    {
        BuildLayout();
    }

    private void BuildLayout()
    {
        LoadingPanel = new StackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromArgb("#80000000"),
            Padding = 30,
            Spacing = 10,
            IsVisible = true,
            Children =
            {
                new ActivityIndicator { IsRunning = true, Color = Colors.White },
                new Label
                {
                    Text = "Загрузка...",
                    TextColor = Colors.White,
                    FontSize = 18,
                    HorizontalTextAlignment = TextAlignment.Center
                }
            }
        };

        MainLayout = new Grid();
        MainLayout.Children.Add(LoadingPanel);

        Content = MainLayout;
    }

    protected void SetPageContent(View content)
    {
        PageContent = content;
        PageContent.IsVisible = false;
        MainLayout.Children.Add(PageContent);
    }

    protected void ShowContent()
    {
        LoadingPanel.IsVisible = false;
        if (PageContent != null)
            PageContent.IsVisible = true;
    }

    protected async Task SimulateLoadAsync(int delayMs = 1000)
    {
        await Task.Delay(delayMs);
        ShowContent();
    }
}
