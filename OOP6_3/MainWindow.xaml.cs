using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace OOP6_3
{
    public partial class MainWindow : Window
    {
        // Секундомер
        Stopwatch Stopwatch = new Stopwatch();
        // Создаем экземпляр класса Calculator
        private Calculator _calculator = new Calculator();

        public MainWindow()
        {
            InitializeComponent();

            // Подписываемся на событие OnResult
            _calculator.OnResult += Calculator_OnResult;
        }

        // Обработка события OnResult
        private void Calculator_OnResult(object sender, ResultEventArgs e)
        {
            // Выводим результат на экран
            ResultLabel.Content = e.Result;
        }

        // Обработка нажатия кнопки "Сложить"
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(FirstNumberTextBox.Text);
            double b = double.Parse(SecondNumberTextBox.Text);

            _calculator.Add(a, b);
        }

        // Обработка нажатия кнопки "Вычесть"
        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(FirstNumberTextBox.Text);
            double b = double.Parse(SecondNumberTextBox.Text);

            _calculator.Subtract(a, b);
        }

        // Обработка нажатия кнопки "Умножить"
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            double a = double.Parse(FirstNumberTextBox.Text);
            double b = double.Parse(SecondNumberTextBox.Text);

            // Запуск секундомера
            Stopwatch.Start();
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            // Вызыв асинхронный метода MultiplyAsync
            _calculator.MultiplyAsync(a, b, result =>
            {
                ResultLabel.Dispatcher.Invoke(() => {
                    // Остановка секундомера
                    CompositionTarget.Rendering -= CompositionTarget_Rendering;
                    Stopwatch.Stop();
                    Stopwatch.Reset();
                    // Вывод результата на экран в основном потоке
                    ResultLabel.Content = result;
                });
            });
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            ResultLabel.Content = $"Вычисление... {Stopwatch.Elapsed:ss\\.ff}";
        }
    }
}
