using System.Collections.Generic;
using System.Text;
using AutoSchool_24_7.Data;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace AutoSchool_24_7.Page.TestPages;

public partial class TestPage : ContentPage
{
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int totalQuestionsToAsk = 20;
    private List<string> incorrectAnswers = new();
    private List<Question> questions;

    public TestPage()
    {
        InitializeComponent();
        QuestionCountPicker.SelectedIndex = 1; // По умолчанию 10
    }

    private void StartTest(object sender, EventArgs e)
    {
        if (QuestionCountPicker.SelectedIndex == -1)
        {
            DisplayAlert("Ошибка", "Пожалуйста, выберите количество вопросов", "OK");
            return;
        }

        if (QuestionCountPicker.SelectedItem is int count)
            totalQuestionsToAsk = count;

        questions = QuestionRepository.AllQuestions
                    .OrderBy(q => Guid.NewGuid())
                    .Take(totalQuestionsToAsk)
                    .ToList();

        currentQuestionIndex = 0;
        correctAnswers = 0;
        incorrectAnswers.Clear();

        // Скрываем настройки
        SettingsLayout.IsVisible = false;

        // Показываем тест
        QuestionCounterLabel.IsVisible = true;
        QuestionLabel.IsVisible = true;
        QuestionImage.IsVisible = true;
        AnswersLayout.IsVisible = true;

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
        StringBuilder resultMessage = new();
        resultMessage.AppendLine($"Вы ответили правильно на {correctAnswers} из {questions.Count} вопросов.\n");

        if (incorrectAnswers.Count > 0)
        {
            resultMessage.AppendLine("Ошибки:");
            foreach (var error in incorrectAnswers)
                resultMessage.AppendLine(error);
        }

        await DisplayAlert("Результаты теста", resultMessage.ToString(), "OK");
        await Navigation.PushAsync(new Menu());
    }
}



