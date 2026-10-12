using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp4
{
    public class QuizQuestion
    {
        public string Text { get; private set; }
        public string[] Answers { get; private set; }
        public int CorrectAnswer { get; private set; }

        public QuizQuestion(string text, string[] answers, int correctAnswer)
        {
            Text = text;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }

    public partial class Window3 : Window
    {
        private readonly List<QuizQuestion> questions = new List<QuizQuestion>
        {
            new QuizQuestion("Сколько дней в одной неделе?", new[] { "5", "7", "10", "12" }, 1),
            new QuizQuestion("Как называется столица России?", new[] { "Москва", "Казань", "Сочи", "Тула" }, 0),
            new QuizQuestion("Сколько будет 2 + 2?", new[] { "3", "5", "4", "6" }, 2),
            new QuizQuestion("Какого цвета обычно бывает трава?", new[] { "Синего", "Зеленого", "Красного", "Черного" }, 1),
            new QuizQuestion("Какое животное говорит «мяу»?", new[] { "Собака", "Корова", "Кошка", "Лошадь" }, 2),
            new QuizQuestion("Сколько месяцев в одном году?", new[] { "12", "6", "10", "24" }, 0),
            new QuizQuestion("Сколько минут в одном часе?", new[] { "30", "45", "100", "60" }, 3),
            new QuizQuestion("Какое время года идет после зимы?", new[] { "Лето", "Весна", "Осень", "Зима" }, 1),
            new QuizQuestion("Сколько сторон у треугольника?", new[] { "2", "4", "3", "5" }, 2),
            new QuizQuestion("Что светит на небе днем?", new[] { "Луна", "Солнце", "Звезды", "Фонарь" }, 1)
        };

        private readonly RadioButton[] answerButtons;
        private int currentQuestion;
        private int correctAnswers;
        private int lastResult = -1;

        public Window3()
        {
            InitializeComponent();
            answerButtons = new[]
            {
                Answer1RadioButton,
                Answer2RadioButton,
                Answer3RadioButton,
                Answer4RadioButton
            };
        }

        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz();
        }

        private void RestartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz();
        }

        private void ViewResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastResult < 0)
            {
                MessageBox.Show("Сначала пройдите викторину.", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ShowResult();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPanel(HomePanel);
        }

        private void AnswerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (NextQuestionButton != null)
            {
                NextQuestionButton.IsEnabled = true;
            }
        }

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedAnswer = GetSelectedAnswer();
            if (selectedAnswer < 0)
            {
                return;
            }

            if (selectedAnswer == questions[currentQuestion].CorrectAnswer)
            {
                correctAnswers++;
            }

            currentQuestion++;
            if (currentQuestion == questions.Count)
            {
                lastResult = correctAnswers;
                ShowResult();
                return;
            }

            ShowQuestion();
        }

        private void StartQuiz()
        {
            currentQuestion = 0;
            correctAnswers = 0;
            ShowPanel(QuizPanel);
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            QuizQuestion question = questions[currentQuestion];
            QuestionNumberTextBlock.Text = "Вопрос " + (currentQuestion + 1) + " из " + questions.Count;
            QuestionTextBlock.Text = question.Text;
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].Content = question.Answers[i];
                answerButtons[i].IsChecked = false;
            }

            NextQuestionButton.IsEnabled = false;
            QuizProgressBar.Value = currentQuestion + 1;
        }

        private int GetSelectedAnswer()
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (answerButtons[i].IsChecked == true)
                {
                    return i;
                }
            }

            return -1;
        }

        private void ShowResult()
        {
            ResultTextBlock.Text = "Правильных ответов: " + lastResult + " из " + questions.Count;
            ShowPanel(ResultPanel);
        }

        private void ShowPanel(UIElement panel)
        {
            HomePanel.Visibility = panel == HomePanel ? Visibility.Visible : Visibility.Collapsed;
            QuizPanel.Visibility = panel == QuizPanel ? Visibility.Visible : Visibility.Collapsed;
            ResultPanel.Visibility = panel == ResultPanel ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
