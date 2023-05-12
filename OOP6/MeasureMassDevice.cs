using DeviceControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP6
{
    public class MeasureMassDevice : MeasureDataDevice
    {
        public MeasureMassDevice(Units deviceUnits, string logFileName)
        {
            unitsToUse = deviceUnits;
            measurementType = DeviceType.MASS;
            loggingFileName = logFileName;
        }

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
