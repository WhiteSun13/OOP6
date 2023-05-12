using System;
using System.Threading;
using DeviceControl;

namespace Contoso.MeasuringDevices
{
    class MassMeasuringDevice : IControllableDevice
    {
        Random random;

        /// <summary>
        /// Создание нового экземпляра класса MassMeasuringDevice.
        /// </summary>
        public MassMeasuringDevice()
        {
            random = new Random();
        }

        /// <summary>
        /// Запуск MassMeasuringDevice
        /// </summary>
        public void StartDevice()
        {
            // Start the device.
        }

        /// <summary>
        /// Выключение MassMeasuringDevice
        /// </summary>
        public void StopDevice()
        {
            // Stop the device.
        }

        /// <summary>
        /// Получает последнее измерение от MassMeasuringDevice.
        /// </summary>
        /// <returns>Последнее измерение, сделанное устройством.</returns>
        public int GetLatestMeasure()
        {
            Thread.Sleep(random.Next(5000));
            return random.Next(1390);
        }
    }
}