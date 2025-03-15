using System.Text;

namespace AutoSchool_24_7.Page.TestPages;

public partial class TestPage : ContentPage
{
    public class PddTestPage : ContentPage
    {
        private Label questionLabel;
        private Label questionCounterLabel;
        private Image questionImage;
        private StackLayout answersLayout;
        private int currentQuestionIndex = 0;
        private int correctAnswers = 0;
        private List<string> incorrectAnswers = new();

        private List<Question> questions = new()
        {
            new Question("����� ���� ���������� ������� ������?", "main_road_sign.png", new string[] { "���� 1", "���� 2", "���� 3", "���� 4" }, 1),
            new Question("����� ���� ���������� ���������� �������?", "pedestrian_crossing.png", new string[] { "���� A", "���� B", "���� C" }, 2),
            new Question("����� �� ����������� �� ���������� ���������?", null, new string[] { "��", "���" }, 1)
        };

        public PddTestPage()
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
            questionCounterLabel.Text = $"������ {currentQuestionIndex + 1} �� {questions.Count}";
            questionLabel.Text = question.Text;
            questionImage.Source = string.IsNullOrEmpty(question.Image) ? null : question.Image;
            answersLayout.Children.Clear();

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


}