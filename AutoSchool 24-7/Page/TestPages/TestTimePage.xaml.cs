using AutoSchool_24_7.Data;
using System.Text;
using System.Timers;

namespace AutoSchool_24_7.Page.TestPages;

public partial class TestTimePage : ContentPage
{
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int totalQuestionsToAsk = 20;
    private List<string> incorrectAnswers = new();
    private System.Timers.Timer testTimer;
    private int timeLeft = 60;
    private List<Question> questions;
    private bool isFlashing = false;

    public TestTimePage()
    {
        InitializeComponent();
        testTimer = new System.Timers.Timer(1000);
        testTimer.Elapsed += OnTimedEvent;
        testTimer.AutoReset = true;
        // ������������� �������� �� ��������� (��������, 10)
        QuestionCountPicker.SelectedIndex = 1;
        TimePicker.SelectedIndex = 1;
    }

    private void StartTest(object sender, EventArgs e)
    {
        if (QuestionCountPicker.SelectedIndex == -1 || TimePicker.SelectedIndex == -1)
        {
            DisplayAlert("������", "����������, �������� ���������� �������� � ����� �� ����", "OK");
            return;
        }

        // �������� ��������� ���������� ��������
        if (QuestionCountPicker.SelectedItem is int count)
            totalQuestionsToAsk = count;

        // ��������� ������ �������� ����� (����� ������)
        questions = QuestionRepository.AllQuestions
                    .OrderBy(q => Guid.NewGuid())
                    .Take(totalQuestionsToAsk)
                    .ToList();

        // �������� ��������� ����� (� �������)
        if (TimePicker.SelectedItem is int selectedMinutes)
            timeLeft = selectedMinutes * 60;

        currentQuestionIndex = 0;
        correctAnswers = 0;
        incorrectAnswers.Clear();
        SettingsLayout.IsVisible = false;
        LabelSetting.IsVisible=false;
        LabelQuestionCountPicker.IsVisible = false;
        LabelTimePicker.IsVisible = false;
        StartButton.IsVisible = false;
        QuestionCountPicker.IsVisible = false;
        TimePicker.IsVisible = false;
        TimerLabel.IsVisible = true;
        QuestionCounterLabel.IsVisible = true;
        QuestionLabel.IsVisible = true;
        QuestionImage.IsVisible = true;
        AnswersLayout.IsVisible = true;

        TimerLabel.Text = $"�����: {FormatTime(timeLeft)}";
        testTimer.Start();

        LoadQuestion();
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            timeLeft--;
            TimerLabel.Text = $"�����: {timeLeft / 60:D2}:{timeLeft % 60:D2}";
            if (timeLeft == 20)
            {
                TimerLabel.TextColor = Colors.Red; // ������� ���� � 20 ������
            }
            if (timeLeft == 10)
            {
               
                StartFlashingTimerLabel();         // ��������� �������
            }

            if (timeLeft <= 0)
            {
                testTimer.Stop();
                isFlashing = false;
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
                CornerRadius = 10,
                Padding = new Thickness(5)
            };
            int selectedAnswerIndex = i;
            button.Clicked += (s, e) =>
            {
                if (selectedAnswerIndex == question.CorrectAnswerIndex)
                    correctAnswers++;
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
        await Navigation.PushAsync(new TestResultPage(correctAnswers, questions.Count, incorrectAnswers));
    }
    private string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        int secs = seconds % 60;
        return $"{minutes:D2}:{secs:D2}";
    }
    private async void StartFlashingTimerLabel()
    {
        isFlashing = true;

        while (isFlashing && timeLeft > 0)
        {
            await TimerLabel.FadeTo(0.2, 300);
            await TimerLabel.FadeTo(1, 300);
        }

        TimerLabel.Opacity = 1;
    }
}
