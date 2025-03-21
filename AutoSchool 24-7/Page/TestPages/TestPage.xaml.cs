using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace AutoSchool_24_7.Page.TestPages;

public partial class TestPage : ContentPage
{
    private Label questionLabel;
    private Label questionCounterLabel;
    private Image questionImage;
    private StackLayout answersLayout;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private List<string> incorrectAnswers = new();
    private Random random = new();

    private List<Question> questions = new()
        {
            new Question("Какой знак обозначает главную дорогу?", "main_road_sign.png", new string[] { "Знак 1", "Знак 2", "Знак 3", "Знак 4" }, 1),
            new Question("Какой знак обозначает пешеходный переход?", "pedestrian_crossing.png", new string[] { "Знак A", "Знак B", "Знак C" }, 2),
            new Question("Можно ли парковаться на автобусной остановке?", null, new string[] { "Да", "Нет" }, 1)
        };

    public TestPage()
    {
        Title = "Тест по ПДД";

        questionCounterLabel = new Label
        {
            FontSize = 16,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(10)
        };

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
            Children = { questionCounterLabel, questionLabel, questionImage, answersLayout }
        };

        Content = new ScrollView { Content = mainLayout };

        LoadQuestion();
    }

    private void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            DisplayResults();
            return;
        }

        var question = questions[currentQuestionIndex];
        question.ShuffleOptions();

        questionCounterLabel.Text = $"Вопрос {currentQuestionIndex + 1} из {questions.Count}";
        questionLabel.Text = question.Text;
        questionImage.Source = string.IsNullOrEmpty(question.Image) ? null : question.Image;
        answersLayout.Children.Clear();

        for (int i = 0; i < question.ShuffledOptions.Length; i++)
        {
            var button = new Button
            {
                Text = $"{i + 1}) {question.ShuffledOptions[i]}",  
                FontSize = 16,
                BackgroundColor = Colors.LightGray,
                CornerRadius = 10,
                Padding = new Thickness(5)
            };
            int selectedAnswerIndex = i;
            button.Clicked += async (sender, e) =>
            {
                if (question.ShuffledOptions[selectedAnswerIndex] == question.Options[question.CorrectAnswerIndex])
                {
                    correctAnswers++;
                }
                else
                {
                    incorrectAnswers.Add($"{question.Text}\nВаш ответ: {question.ShuffledOptions[selectedAnswerIndex]}\nПравильный ответ: {question.Options[question.CorrectAnswerIndex]}\n");
                }
                currentQuestionIndex++;
                LoadQuestion();
            };
            answersLayout.Children.Add(button);
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

        
        await Navigation.PushAsync(new Menu());

       
        currentQuestionIndex = 0;
        correctAnswers = 0;
        incorrectAnswers.Clear();
        LoadQuestion();
    }
}

