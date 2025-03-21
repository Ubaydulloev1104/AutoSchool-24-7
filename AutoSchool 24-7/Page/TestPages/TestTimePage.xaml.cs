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
        new Question(" Какие транспортные средства по Правилам относятся к маршрутным транспортным \r\nсредствам?", null, new string[] { "1. Все автобусы", "2. Автобусы, троллейбусы и трамваи, предназначенные для перевозки людей и движущиесяпо установленному маршруту с обозначенными местами остановок. ", "3. Любые транспортные средства, перевозящие пассажиров.", }, 1),
        new Question(" В каких направлениях Вам разрешено продолжить движение?", "test_1_2", new string[] { "1. Только Б", "2. Только А или Б. ", "3. В любых. " }, 1),
        new Question(" Этот дорожный знак указывает: ", "test_1_3", new string[] { "1. Расстояние до конца тоннеля. ", "2. Расстояние до места аварийной остановки. ", "3. Направление движения к аварийному выходу и расстояние до него.  " }, 2),
        new Question(" Этот знак разрешает Вам ставить на стоянку легковой автомобиль с использованием тротуара: ", "test_1_4", new string[] { "1. Только на правой стороне дороги до ближайшего по ходу движения перекрестка. ", "2. Только на правой стороне дороги до знака «Конец зоны регулируемой стоянки».  ", "3. На любой стороне дорог, расположенных в зоне регулируемой стоянки.  " }, 2),
        new Question(" Эта разметка, нанесенная на полосе движения: ", "test_1_5", new string[] { "1. Предоставляет Вам преимущество при перестроении на правую полосу. ", "2. Информирует Вас о том, что дорога поворачивает направо. ", "3. Предупреждает Вас о приближении к сужению проезжей части.   " }, 2)
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

