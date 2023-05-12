using System;
using System.Windows;

namespace OOP6_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        MeasureMassDevice device;

        // TODO - Declare a delegate to reference NewMeasurementEvent handler.
        EventHandler newMeasurementTaken;

        private void startCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (device == null)
                device = new MeasureMassDevice(Units.Metric, "LogFile.txt");

            // TODO - Use a delegate to refer to the event handler.
            newMeasurementTaken = new EventHandler(device_NewMeasurementTaken);
            // TODO - Hook up the event handler to the event.
            device.NewMeasurementTaken += newMeasurementTaken;

            device.HeartBeat += (o, args) =>
            {
                heartBeatTimeStamp.Content = "HeartBeat Timestamp: " + args.TimeStamp;
            };

            loggingFileNameBox.Text = device.GetLoggingFile();
            unitsBox.Text = device.UnitsToUse.ToString();

            device.StartCollecting();
        }

        // TODO - Add the device_NewMeasurementTaken event handler method to update the UI with the new measurement.
        private void device_NewMeasurementTaken(object sender, EventArgs e)
        {
            if (device != null)
            {
                mostRecentMeasureBox.Text = device.MostRecentMeasure.ToString();
                metricValueBox.Text = device.MetricValue().ToString();
                imperialValueBox.Text = device.ImperialValue().ToString();
                rawDataValues.ItemsSource = null;
                rawDataValues.ItemsSource = device.GetRawData();
            }
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                device.LoggingFileName = loggingFileNameBox.Text;
            }
            else
            {
                MessageBox.Show("You must create an instance of the MeasureMassDevice class first.");
            }
        }

        private void dispose_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                device.Dispose();
                device = null;
            }
        }

        private void stopCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                device.StopCollecting();

                // TODO - Disconnect the event handler.
                device.NewMeasurementTaken -= device_NewMeasurementTaken;
            }
        }
    }
}
