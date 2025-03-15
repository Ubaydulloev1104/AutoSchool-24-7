namespace AutoSchool_24_7.Page.TestPages;

public partial class TestPage : ContentPage
{
    private Label questionLabel;
    private Image questionImage;
    private StackLayout answersLayout;
    private int currentQuestionIndex = 0;

    private List<Question> questions = new()
        {
            new Question("����� ���� ���������� ������� ������?", "main_road_sign.png", new string[] { "���� 1", "���� 2", "���� 3", "���� 4" }, 1),
            new Question("����� ���� ���������� ���������� �������?", "pedestrian_crossing.png", new string[] { "���� A", "���� B", "���� C" }, 2),
            new Question("����� �� ����������� �� ���������� ���������?", null, new string[] { "��", "���" }, 1)
        };

    public TestPage()
    {
        InitializeComponent();
        Title = "���� �� ���";
        questionLabel = new Label
        {
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(10)
        };

        questionImage = new Image
        {
            HeightRequest = 150,
            Aspect = Aspect.AspectFit,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(10)
        };

        answersLayout = new StackLayout
        {
            Spacing = 10,
            Margin = new Thickness(10)
        };

        var mainLayout = new StackLayout
        {
            Padding = new Thickness(20),
            Children = { questionLabel, questionImage, answersLayout }
        };

        Content = new ScrollView { Content = mainLayout };

        LoadQuestion();
    }

    private void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            DisplayAlert("���� ��������", "�� �������� �� ��� �������!", "OK");
            return;
        }

        var question = questions[currentQuestionIndex];
        questionLabel.Text = question.Text;
        questionImage.Source = string.IsNullOrEmpty(question.Image) ? null : question.Image;
        answersLayout.Children.Clear();

        foreach (var option in question.Options)
        {
            var button = new Button
            {
                Text = option,
                FontSize = 16,
                BackgroundColor = Colors.LightGray,
                CornerRadius = 10,
                Padding = new Thickness(5)
            };
            button.Clicked += OnAnswerSelected;
            answersLayout.Children.Add(button);
        }
    }

    private async void OnAnswerSelected(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            await DisplayAlert("�����", $"�� �������: {button.Text}", "OK");
            currentQuestionIndex++;
            LoadQuestion();
        }
    }
  
}