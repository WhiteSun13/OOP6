using System;

using ContosoDevices = Contoso.MeasuringDevices;
using FabrikamDevices = Fabrikam.Devices.MeasuringDevices;

namespace DeviceControl
{
    public enum DeviceType
    {
        MASS, LENGTH
    }
    public class DeviceController : IDisposable
    {
        private IControllableDevice device;

        /// <summary>
        /// Метод для создания нового экземпляра устройства.
        /// </summary>
        /// <param name="MeasurementType">Указывает тип запускаемого устройства: MASS или LENGTH.</param>
        /// <returns>Экземпляр класса DeviceController с управляемым устройством в запущенном состоянии.</returns>
        public static DeviceController StartDevice(DeviceType MeasurementType)
        {
            DeviceController controller = new DeviceController();
            switch (MeasurementType)
            {
                case DeviceType.LENGTH:
                    controller.device = new FabrikamDevices.LengthMeasuringDevice();
                    break;
                case DeviceType.MASS:
                    controller.device = new ContosoDevices.MassMeasuringDevice();
                    break;
            }
            if (controller.device != null)
            {
                controller.device.StartDevice();
            }

            return controller;
        }

        /// <summary>
        /// Останавка управляемого устройство.
        /// </summary>
        public void StopDevice()
        {
            device.StopDevice();
        }

        /// <summary>
        /// Заставляет контролируемое устройство записывать измерение.
        /// </summary>
        /// <returns>Измерение, сделанное устройством.</returns>
        public int TakeMeasurement()
        {
            return device.GetLatestMeasure();
        }

        /// <summary>
        /// Утилизирует устройство.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
