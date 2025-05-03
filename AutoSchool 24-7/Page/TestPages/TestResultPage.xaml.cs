using System.Text;

namespace AutoSchool_24_7.Page.TestPages;

public partial class TestResultPage : ContentPage
{
    private readonly int correctAnswers;
    private readonly int totalQuestions;
    private readonly List<string> incorrectAnswers;

    public TestResultPage(int correctAnswers, int totalQuestions, List<string> incorrectAnswers)
    {
        InitializeComponent();
        this.correctAnswers = correctAnswers;
        this.totalQuestions = totalQuestions;
        this.incorrectAnswers = incorrectAnswers;

        ResultLabel.Text = $"Вы ответили правильно на {correctAnswers} из {totalQuestions} вопросов.";

        // Установка иконки в зависимости от процента правильных ответов
        double percent = (double)correctAnswers / totalQuestions;

        if (percent >= 0.8)
        {
            ResultIcon.Text = "✅ Отличный результат!";
        }
        else if (percent >= 0.5)
        {
            ResultIcon.Text = "⚠️ Есть над чем поработать.";
        }
        else
        {
            ResultIcon.Text = "❌ Рекомендуется повторить изучение.";
        }

        // Добавляем эмодзи к ошибкам
        if (incorrectAnswers.Count > 0)
        {
            var builder = new StringBuilder();
            foreach (var error in incorrectAnswers)
            {
                builder.AppendLine($"❌ {error}");
                builder.AppendLine(); // отступ
            }
            ErrorsLabel.Text = builder.ToString();
        }
        else
        {
            ErrorsLabel.IsVisible = false;
        }
    }

    private async void OnRetryClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(); // или перейти к TestPage
    }

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Menu());
    }
}