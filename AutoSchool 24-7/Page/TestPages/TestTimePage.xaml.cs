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
        new Question(" Какие транспортные средства по Правилам относятся к маршрутным транспортным \r\nсредствам?", null, new string[] { " Все автобусы", " Автобусы, троллейбусы и трамваи, предназначенные для перевозки людей и движущиесяпо установленному маршруту с обозначенными местами остановок. ", " Любые транспортные средства, перевозящие пассажиров.", }, 1),
        new Question(" В каких направлениях Вам разрешено продолжить движение?", "test_1_2", new string[] { " Только Б", " Только А или Б. ", " В любых. " }, 1),
        new Question(" Этот дорожный знак указывает: ", "test_1_3", new string[] { " Расстояние до конца тоннеля. ", " Расстояние до места аварийной остановки. ", " Направление движения к аварийному выходу и расстояние до него.  " }, 2),
        new Question(" Этот знак разрешает Вам ставить на стоянку легковой автомобиль с использованием тротуара: ", "test_1_4", new string[] { "1. Только на правой стороне дороги до ближайшего по ходу движения перекрестка. ", "2. Только на правой стороне дороги до знака «Конец зоны регулируемой стоянки».  ", "3. На любой стороне дорог, расположенных в зоне регулируемой стоянки.  " }, 2),
        new Question(" Эта разметка, нанесенная на полосе движения: ", "test_1_5", new string[] { "1. Предоставляет Вам преимущество при перестроении на правую полосу. ", "2. Информирует Вас о том, что дорога поворачивает направо. ", "3. Предупреждает Вас о приближении к сужению проезжей части.   " }, 2),
        new Question("Что означает мигание зеленого сигнала светофора?",
        null,
        new string[] {
            "1. Предупреждает о неисправности светофора.",
            "2. Разрешает движение и информирует о том, что вскоре будет включен запрещающий сигнал.",
            "3. Запрещает дальнейшее движение."
        },
        1),

    new Question("Обязаны ли Вы в данной ситуации подать сигнал правого поворота?",
        null,
        new string[] {
            "1. Да.",
            "2. Да, но только при наличии движущихся сзади транспортных средств.",
            "3. Нет."
        },
        2),

    new Question("Кто должен уступить дорогу при одновременном перестроении?",
        null,
        new string[] {
            "1. Водитель легкового автомобиля.",
            "2. Водитель мотоцикла."
        },
        0),

    new Question("По какой траектории Вам разрешено выполнить разворот?",
        null,
        new string[] {
            "1. Только по А.",
            "2. Только по Б.",
            "3. По любой."
        },
        0),
    new Question("Какие транспортные средства по Правилам относятся к маршрутным транспортным средствам?",
        null,
        new string[] {
            "1. Все автобусы",
            "2. Автобусы, троллейбусы и трамваи, предназначенные для перевозки людей и движущиеся по установленному маршруту с обозначенными местами остановок.",
            "3. Любые транспортные средства, перевозящие пассажиров."
        },
        1),

    new Question("С какой скоростью Вы можете продолжить движение вне населенного пункта по левой полосе на легковом автомобиле?",
        null,
        new string[] {
            "1. Не более 50 км/ч.",
            "2. Не менее 50 км/ч и не более 70 км/ч.",
            "3. Не менее 50 км/ч и не более 90 км/ч."
        },
        2),

    new Question("Может ли водитель легкового автомобиля в населенном пункте выполнить опережение грузовых автомобилей по такой траектории?",
        null,
        new string[] {
            "1. Да.",
            "2. Нет."
        },
        0),

    new Question("Разрешено ли водителю поставить автомобиль на стоянку в указанном месте?",
        null,
        new string[] {
            "1. Да.",
            "2. Нет."
        },
        0),

    new Question("Вы намерены повернуть направо. Следует ли уступить дорогу автобусу?",
        null,
        new string[] {
            "1. Да.",
            "2. Нет."
        },
        0),

    new Question("Вы намерены проехать перекресток в прямом направлении. Ваши действия?",
        null,
        new string[] {
            "1. Уступите дорогу легковому автомобилю, поскольку он первым въехал на перекресток.",
            "2. Убедитесь, что легковой автомобиль уступает дорогу, и проедете перекресток первым."
        },
        1),

    new Question("Вы намерены повернуть налево. Кому следует уступить дорогу?",
        null,
        new string[] {
            "1. Только автобусу.",
            "2. Только легковому автомобилю.",
            "3. Никому."
        },
        2),

    new Question("С какой максимальной скоростью Вы можете продолжить движение за знаком?",
        null,
        new string[] {
            "1. 60 км/ч.",
            "2. 50 км/ч.",
            "3. 30 км/ч.",
            "4. 20 км/ч."
        },
        3),

    new Question("Какие внешние световые приборы Вы можете использовать при движении в темное время суток на неосвещенных участках дорог?",
        null,
        new string[] {
            "1. Только ближний свет фар.",
            "2. Только дальний свет фар.",
            "3. Ближний или дальний свет фар."
        },
        2),

    new Question("При какой неисправности разрешается эксплуатация транспортного средства?",
        null,
        new string[] {
            "1. Не работают запоры горловин топливных баков.",
            "2. Не работает механизм регулировки сиденья водителя.",
            "3. Не работает устройство обогрева и обдува стекла.",
            "4. Не работает стеклоподъемник."
        },
        3),

    new Question("В случае, когда правые колеса автомобиля наезжают на неукрепленную влажную обочину, рекомендуется:",
        null,
        new string[] {
            "1. Затормозить и полностью остановиться.",
            "2. Затормозить и плавно направить автомобиль в левую сторону.",
            "3. Не прибегая к торможению, плавно вернуть автомобиль на проезжую часть."
        },
        2),

    new Question("Начинать сердечно-легочную реанимацию следует только при:",
        null,
        new string[] {
            "1. Потере человеком сознания, независимо от наличия пульса.",
            "2. Потере человеком сознания при отсутствии пульса на сонной артерии."
        },
        1),
    //2 билед 
    new Question("При движении на легковом автомобиле, оборудованном ремнями безопасности, пристегиваться ремнями должны:",
        null,
        new string[] {
            "Только водитель.",
            "Только водитель и пассажир на переднем сиденье.",
            "Все лица, находящиеся в автомобиле."
        },
        2),

    new Question("Можете ли Вы въехать на мост первым?",
        null,
        new string[] {
            "Да.",
            "Нет."
        },
        0),

    new Question("С какой максимальной скоростью Вы можете продолжить движение на грузовом автомобиле с разрешенной максимальной массой не более 3,5 т?",
        null,
        new string[] {
            "60 км/ч.",
            "70 км/ч.",
            "80 км/ч."
        },
        2),

    new Question("Что запрещено в зоне действия этого знака?",
        null,
        new string[] {
            "Движение со скоростью более 20 км/ч.",
            "Движение только механических транспортных средств.",
            "Движение любых транспортных средств."
        },
        2),

    new Question("Разрешен ли Вам обгон, если реверсивные светофоры отключены?",
        null,
        new string[] {
            "Разрешен.",
            "Разрешен, если скорость автобуса менее 30 км/ч.",
            "Не разрешен."
        },
        2),

    new Question("В каких направлениях Вам разрешено продолжить движение?",
        null,
        new string[] {
            "Только налево.",
            "Прямо и налево.",
            "Налево и в обратном направлении."
        },
        0),

    new Question("Поднятая вверх рука водителя легкового автомобиля является сигналом, информирующим Вас:",
        null,
        new string[] {
            "О его намерении повернуть направо.",
            "О его намерении продолжить движение прямо.",
            "О его намерении снизить скорость, чтобы остановиться и уступить дорогу мотоциклисту."
        },
        2),

    new Question("Двигаясь по левой полосе, Вы намерены перестроиться на правую. На каком из рисунков показана ситуация, в которой Вы обязаны уступить дорогу?",
        null,
        new string[] {
            "На левом.",
            "На правом.",
            "На обоих."
        },
        2),

    new Question("Разрешен ли Вам разворот в указанном месте?",
        null,
        new string[] {
            "Разрешен только при отсутствии приближающегося поезда.",
            "Разрешен.",
            "Запрещен."
        },
        1),

    new Question("В каких случаях Вы можете наезжать на прерывистые линии разметки, разделяющие проезжую часть на полосы движения?",
        null,
        new string[] {
            "Только при перестроении.",
            "Только при движении в темное время суток.",
            "Только если на дороге нет других транспортных средств.",
            "Во всех перечисленных случаях."
        },
        0)
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

