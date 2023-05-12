using System;
using DeviceControl;
using System.IO;
using System.ComponentModel;

namespace OOP6
{
    //TODO - Modify the class definition to implement the extended interface.
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice, IDisposable
    {
        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected DeviceType measurementType;

        protected string loggingFileName;
        private TextWriter loggingFileWriter;

        /// <summary>
        /// Преобразует необработанные данные, собранные измерительным устройством, в метрическое значение.
        /// </summary>
        /// <returns>Последнее измерение с устройства, преобразованное в метрические единицы.</returns>
        public abstract decimal MetricValue();

        /// <summary>
        /// Преобразует необработанные данные, собранные измерительным устройством, в импералистическое значение.
        /// </summary>
        /// <returns>Последнее измерение с устройства, преобразованное в импералистические единицы.</returns>
        public abstract decimal ImperialValue();

        // TODO - Declare a BackgroundWorker to generate data.
        private BackgroundWorker dataCollector;

        /// <summary>
        /// Запуск измерительного устройства
        /// </summary>
        public void StartCollecting()
        {
            if (disposed == true) return;

            if (controller == null)
                controller = DeviceController.StartDevice(measurementType);

            // TODO - Call the GetMeasurements method.
            GetMeasurements();
        }

        // TODO - Implement the GetMeasurements method.
        // Метод GetMeasurements для настройки и запуска BackgroundWorker.
        private void GetMeasurements()
        {
            dataCollector = new BackgroundWorker();
            dataCollector.WorkerSupportsCancellation = true;
            dataCollector.WorkerReportsProgress = true;

            dataCollector.DoWork += new DoWorkEventHandler(dataCollector_DoWork);
            dataCollector.ProgressChanged += new ProgressChangedEventHandler(dataCollector_ProgressChanged);
            dataCollector.RunWorkerAsync();
        }
        void dataCollector_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }
        void dataCollector_DoWork(object sender, DoWorkEventArgs e)
        {
            dataCaptured = new int[10];
            int i = 0;
            while (!dataCollector.CancellationPending)
            {
                DataCaptured[i] = controller.TakeMeasurement();
                mostRecentMeasure = dataCaptured[i];

                if (disposed) break;
                
                if (loggingFileWriter != null)
                {
                    loggingFileWriter.WriteLine($"Measurement-{mostRecentMeasure}");
                }

                dataCollector.ReportProgress(0);
                i++;
                if (i > 9) i = 0;
            }
        }

        /// <summary>
        /// Остановка измерительного устройства
        /// </summary>
        public void StopCollecting()
        {
            if (disposed == true) return;

            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }

            // TODO - Cancel the data collector.
            if (dataCollector != null)
            {
                dataCollector.CancelAsync();
            }
        }

        /// <summary>
        /// Разрешает доступ к необработанным данным с устройства в любых единицах измерения, которые являются родными для устройства.
        /// </summary>
        /// <returns>Необработанные данные с устройства в собственном формате.</returns>
        public int[] GetRawData()
        {
            return dataCaptured;
        }

        /// <summary>
        /// Возвращает имя файла для устройства.
        /// </summary>
        /// <returns>Имя файла.</returns>
        public string GetLoggingFile()
        {
            return loggingFileName;
        }

        // Бул, указывающий, был ли вызван метод Dispose.
        private bool disposed = false;

        /// <summary>
        /// Требуется метод Dispose для интерфейса IDispose.
        /// </summary>
        public void Dispose()
        {
            // Был вызван метод Dispose.
            disposed = true;

            // Проверка, что файл закрыт, если он не закрыт, закрыть его.
            if (loggingFileWriter != null)
            {
                loggingFileWriter.Flush();
                loggingFileWriter.Close();
            }

            // TODO - Dispose of the data collector.  
            if (dataCollector != null)
            {
                dataCollector.Dispose();
            }
        }

        /// <summary>
        /// Получает единицы измерения, изначально используемые устройством.
        /// </summary>
        public Units UnitsToUse => unitsToUse;

        /// <summary>
        /// Получает массив измерений, сделанных устройством.
        /// </summary>
        public int[] DataCaptured => dataCaptured;

        /// <summary>
        /// Получает последние измерения, сделанные устройством.
        /// </summary>
        public int MostRecentMeasure => mostRecentMeasure;

        /// <summary>
        /// Получает или задает имя используемого файла.
        /// Если файл изменяется, текущий файл закрывается и создается новый файл.
        /// </summary>
        public string LoggingFileName
        {
            get
            {
                return loggingFileName;
            }
            set
            {
                if (loggingFileWriter == null)
                {
                    // Если файл не был открыт, обновляется имя файла.
                    loggingFileName = value;
                }
                else
                {
                    // Если файл был открыт, закрывается текущий файл
                    loggingFileWriter.Close();

                    // Обновляется имя файла и открывается новый файл.
                    loggingFileName = value;

                    // Проверяем, существует ли файл, если нет, создаем его.
                    if (!File.Exists(loggingFileName))
                    {
                        loggingFileWriter = File.CreateText(loggingFileName);
                    }
                    else
                    {
                        loggingFileWriter = new StreamWriter(loggingFileName);
                    }
                }
            }
        }

        // TODO - Add the NewMeasurementTaken event.
        // Реализация класса события NewMeasurementTaken.
        public event EventHandler NewMeasurementTaken;
        // TODO - Add an OnMeasurementTaken method.       
        // Метод для вызова события NewMeasurementTaken.
        protected virtual void OnNewMeasurementTaken()
        {
            if (NewMeasurementTaken != null)
            {
                NewMeasurementTaken(this, null);
            }
        }

    }
}