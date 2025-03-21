using System.Text;
using System.Timers;

namespace AutoSchool_24_7.Page.TestPages;

public partial class TestTimePage : ContentPage
{
    private Label questionLabel;
    private Label questionCounterLabel;
    private Image questionImage;
    private StackLayout answersLayout;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private List<string> incorrectAnswers = new();
    private System.Timers.Timer testTimer;
    private int timeLeft = 60; // 60 ������


    private List<Question> questions = new()
    {
        new Question(" ����� ������������ �������� �� �������� ��������� � ���������� ������������ \r\n���������?", null, new string[] { "1. ��� ��������", "2. ��������, ����������� � �������, ��������������� ��� ��������� ����� � ������������ �������������� �������� � ������������� ������� ���������. ", "3. ����� ������������ ��������, ����������� ����������.", }, 1),
        new Question(" � ����� ������������ ��� ��������� ���������� ��������?", "test_1_2", new string[] { "1. ������ �", "2. ������ � ��� �. ", "3. � �����. " }, 1),
        new Question(" ���� �������� ���� ���������: ", "test_1_3", new string[] { "1. ���������� �� ����� �������. ", "2. ���������� �� ����� ��������� ���������. ", "3. ����������� �������� � ���������� ������ � ���������� �� ����.  " }, 2),
        new Question(" ���� ���� ��������� ��� ������� �� ������� �������� ���������� � �������������� ��������: ", "test_1_4", new string[] { "1. ������ �� ������ ������� ������ �� ���������� �� ���� �������� �����������. ", "2. ������ �� ������ ������� ������ �� ����� ������ ���� ������������ �������.  ", "3. �� ����� ������� �����, ������������� � ���� ������������ �������.  " }, 2),
        new Question(" ��� ��������, ���������� �� ������ ��������: ", "test_1_5", new string[] { "1. ������������� ��� ������������ ��� ������������ �� ������ ������. ", "2. ����������� ��� � ���, ��� ������ ������������ �������. ", "3. ������������� ��� � ����������� � ������� �������� �����.   " }, 2)
    };


    public TestTimePage()
    {
        InitializeComponent();
        testTimer = new System.Timers.Timer(1000); // ������ ����������� ������ �������
        testTimer.Elapsed += OnTimedEvent;
        testTimer.AutoReset = true;
    }

    private void StartTest(object sender, EventArgs e)
    {
        StartButton.IsVisible = false; // �������� ������ "�����"
        TimerLabel.IsVisible = true;
        QuestionCounterLabel.IsVisible = true;
        QuestionLabel.IsVisible = true;
        QuestionImage.IsVisible = true;
        AnswersLayout.IsVisible = true;

        timeLeft = 60; // ���������� �����
        TimerLabel.Text = $"�����: {timeLeft} ���";
        testTimer.Start();

        LoadQuestion();
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            timeLeft--;
            TimerLabel.Text = $"�����: {timeLeft} ���";

            if (timeLeft <= 0)
            {
                testTimer.Stop();
                DisplayResults();
            }
        });
    }

    private void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            testTimer.Stop();
            DisplayResults();
            return;
        }

        var question = questions[currentQuestionIndex];
        QuestionCounterLabel.Text = $"������ {currentQuestionIndex + 1} �� {questions.Count}";
        QuestionLabel.Text = question.Text;
        QuestionImage.Source = string.IsNullOrEmpty(question.Image) ? null : question.Image;
        AnswersLayout.Children.Clear();

        for (int i = 0; i < question.Options.Length; i++)
        {
            var button = new Button
            {
                Text = question.Options[i],
                FontSize = 16,
                BackgroundColor = Colors.LightGray,
                CornerRadius = 10,
                Padding = new Thickness(5)
            };
            int selectedAnswerIndex = i;
            button.Clicked += async (sender, e) =>
            {
                if (selectedAnswerIndex == question.CorrectAnswerIndex)
                {
                    correctAnswers++;
                }
                else
                {
                    incorrectAnswers.Add($"{question.Text}\n��� �����: {question.Options[selectedAnswerIndex]}\n���������� �����: {question.Options[question.CorrectAnswerIndex]}\n");
                }
                currentQuestionIndex++;
                LoadQuestion();
            };
            AnswersLayout.Children.Add(button);
        }
    }

    private async void DisplayResults()
    {
        StringBuilder resultMessage = new StringBuilder();
        resultMessage.AppendLine($"�� �������� ��������� �� {correctAnswers} �� {questions.Count} ��������.\n");

        if (incorrectAnswers.Count > 0)
        {
            resultMessage.AppendLine("������:");
            foreach (var error in incorrectAnswers)
            {
                resultMessage.AppendLine(error);
            }
        }

        await DisplayAlert("���������� �����", resultMessage.ToString(), "OK");

        // ������� � ���� ����� ����������
        await Navigation.PushAsync(new Menu());
    }
}

