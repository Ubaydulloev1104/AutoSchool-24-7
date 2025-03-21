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
    private int timeLeft = 60; // 60 секунд

    private List<Question> questions = new()
        {
            new Question("Какой знак обозначает главную дорогу?", "main_road_sign.png", new string[] { "1) Знак 1", "2) Знак 2", "3) Знак 3", "4) Знак 4" }, 1),
            new Question("Какой знак обозначает пешеходный переход?", "pedestrian_crossing.png", new string[] { "1) Знак A", "2) Знак B", "3) Знак C" }, 2),
            new Question("Можно ли парковаться на автобусной остановке?", null, new string[] { "1) Да", "2) Нет" }, 1)
        };

    public TestTimePage()
    {
        InitializeComponent();
        testTimer = new System.Timers.Timer(1000); // Таймер обновляется каждую секунду
        testTimer.Elapsed += OnTimedEvent;
        testTimer.AutoReset = true;
    }

    private void StartTest(object sender, EventArgs e)
    {
        StartButton.IsVisible = false; // Скрываем кнопку "Старт"
        TimerLabel.IsVisible = true;
        QuestionCounterLabel.IsVisible = true;
        QuestionLabel.IsVisible = true;
        QuestionImage.IsVisible = true;
        AnswersLayout.IsVisible = true;

        timeLeft = 60; // Сбрасываем время
        TimerLabel.Text = $"Время: {timeLeft} сек";
        testTimer.Start();

        LoadQuestion();
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            timeLeft--;
            TimerLabel.Text = $"Время: {timeLeft} сек";

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
        QuestionCounterLabel.Text = $"Вопрос {currentQuestionIndex + 1} из {questions.Count}";
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
                    incorrectAnswers.Add($"{question.Text}\nВаш ответ: {question.Options[selectedAnswerIndex]}\nПравильный ответ: {question.Options[question.CorrectAnswerIndex]}\n");
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
        resultMessage.AppendLine($"Вы ответили правильно на {correctAnswers} из {questions.Count} вопросов.\n");

        if (incorrectAnswers.Count > 0)
        {
            resultMessage.AppendLine("Ошибки:");
            foreach (var error in incorrectAnswers)
            {
                resultMessage.AppendLine(error);
            }
        }

        await DisplayAlert("Результаты теста", resultMessage.ToString(), "OK");

        // Переход в меню после завершения
        await Navigation.PushAsync(new Menu());
    }
}

