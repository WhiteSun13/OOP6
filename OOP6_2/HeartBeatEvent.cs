using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP6_2
{
    public class HeartBeatEventArgs : EventArgs
    {
        public DateTime TimeStamp { get; }
        public HeartBeatEventArgs()
        {
            TimeStamp = DateTime.Now;
        }
    }
    public delegate void HeartBeatEventHandler(object sender, HeartBeatEventArgs args);
}
