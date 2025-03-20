using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace AutoSchool_24_7.Page.TestPages;

public partial class TestPage : ContentPage
{
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private List<string> incorrectAnswers = new();

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
        InitializeComponent();
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
        currentQuestionIndex = 0;
        correctAnswers = 0;
        incorrectAnswers.Clear();
        LoadQuestion();
    }
}

