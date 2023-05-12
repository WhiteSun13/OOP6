using System;
using System.Threading;
using System.Threading.Tasks;

namespace OOP6_3
{
    class Calculator
    {
        // Создаем событие OnResult
        public event EventHandler<ResultEventArgs> OnResult;

        // Реализуем метод сложения
        public void Add(double a, double b)
        {
            // Вычисляем результат
            double result = a + b;

            // Генерируем событие OnResult
            OnResult?.Invoke(this, new ResultEventArgs(result));
        }

        // Реализуем метод вычитания
        public void Subtract(double a, double b)
        {
            // Вычисляем результат
            double result = a - b;

            // Генерируем событие OnResult
            OnResult?.Invoke(this, new ResultEventArgs(result));
        }

        double Multiply(double a, double b)
        {
            Thread.Sleep(5000);
            return a * b;
        }

        // Реализуем асинхронный метод умножения
        public async void MultiplyAsync(double a, double b, Action<double> callback)
        {
            // Вычисляем результат в асинхронном режиме
            double result = await Task.Run(() => Multiply(a,b));
            // Вызываем callback-функцию в основном потоке
            System.Windows.Application.Current.Dispatcher.Invoke(() => callback(result));
        }
    }

    // Создаем класс для передачи пользовательских параметров в событие OnResult
    public class ResultEventArgs : EventArgs
    {
        public double Result { get; }

        public ResultEventArgs(double result) => Result = result;
    }
}