using System;
using System.Threading;
using DeviceControl;

namespace Fabrikam.Devices.MeasuringDevices
{
    class LengthMeasuringDevice : IControllableDevice
    {
        Random random;

        /// <summary>
        /// Создание нового экземпляра класса LengthMeasuringDevice.
        /// </summary>
        public LengthMeasuringDevice()
        {
            random = new Random();
        }

        /// <summary>
        /// Запуск LengthMeasuringDevice 
        /// </summary>
        public void StartDevice()
        {
            // Start the device.           
        }

        /// <summary>
        /// Выключение LengthMeasuringDevice
        /// </summary>
        public void StopDevice()
        {
            // Stop the device.
        }

        /// <summary>
        /// Получает последнее измерение от LengthMeasuringDevice.
        /// </summary>
        /// <returns>Последнее измерение, сделанное устройством.</returns>
        public int GetLatestMeasure()
        {
            Thread.Sleep(random.Next(6000));
            return random.Next(1000);
        }
    }
}