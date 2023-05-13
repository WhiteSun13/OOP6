using DeviceControl;

namespace OOP6_2
{
    public class MeasureMassDevice : MeasureDataDevice
    {
        public MeasureMassDevice(Units deviceUnits, string logFileName, int heartBeatInterval)
        {
            unitsToUse = deviceUnits;
            measurementType = DeviceType.MASS;
            loggingFileName = logFileName;
            HeartBeatInterval = heartBeatInterval;
        }

        public MeasureMassDevice(Units deviceUnits, string logFileName) : this(deviceUnits, logFileName, 1000)
        { }

        public override decimal MetricValue()
        {
            if (unitsToUse == Units.Metric)
            {
                return mostRecentMeasure;
            }
            else
            {
                return mostRecentMeasure * 0.4536m;
            }
        }

        public override decimal ImperialValue()
        {
            if (unitsToUse == Units.Imperial)
            {
                return mostRecentMeasure;
            }
            else
            {
                return mostRecentMeasure * 2.2046m;
            }
        }
    }
}
