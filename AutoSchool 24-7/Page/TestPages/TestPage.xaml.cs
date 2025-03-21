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
    new Question(" ����� ������������ �������� �� �������� ��������� � ���������� ������������ \r\n���������?", null, new string[] { "1. ��� ��������", "2. ��������, ����������� � �������, ��������������� ��� ��������� ����� � ������������ �������������� �������� � ������������� ������� ���������. ", "3. ����� ������������ ��������, ����������� ����������.", }, 1),
    new Question(" � ����� ������������ ��� ��������� ���������� ��������?", "test_1_2", new string[] { "1. ������ �", "2. ������ � ��� �. ", "3. � �����. " }, 1),
    new Question(" ���� �������� ���� ���������: ", "test_1_3", new string[] { "1. ���������� �� ����� �������. ", "2. ���������� �� ����� ��������� ���������. ", "3. ����������� �������� � ���������� ������ � ���������� �� ����.  " }, 2),
    new Question(" ���� ���� ��������� ��� ������� �� ������� �������� ���������� � �������������� ��������: ", "test_1_4", new string[] { "1. ������ �� ������ ������� ������ �� ���������� �� ���� �������� �����������. ", "2. ������ �� ������ ������� ������ �� ����� ������ ���� ������������ �������.  ", "3. �� ����� ������� �����, ������������� � ���� ������������ �������.  " }, 2),
    new Question(" ��� ��������, ���������� �� ������ ��������: ", "test_1_5", new string[] { "1. ������������� ��� ������������ ��� ������������ �� ������ ������. ", "2. ����������� ��� � ���, ��� ������ ������������ �������. ", "3. ������������� ��� � ����������� � ������� �������� �����.   " }, 2)
};

    public TestPage()
    {
        Title = "���� �� ���";

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
        question.ShuffleOptions(); // ������������ �������� �������

        questionCounterLabel.Text = $"������ {currentQuestionIndex + 1} �� {questions.Count}";
        questionLabel.Text = question.Text;
        questionImage.Source = string.IsNullOrEmpty(question.Image) ? null : question.Image;
        answersLayout.Children.Clear();

        for (int i = 0; i < question.ShuffledOptions.Length; i++)
        {
            var button = new Button
            {
                Text = question.ShuffledOptions[i],
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
                    incorrectAnswers.Add($"{question.Text}\n��� �����: {question.ShuffledOptions[selectedAnswerIndex]}\n���������� �����: {question.Options[question.CorrectAnswerIndex]}\n");
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
        currentQuestionIndex = 0;
        correctAnswers = 0;
        incorrectAnswers.Clear();
        LoadQuestion();
    }
}

