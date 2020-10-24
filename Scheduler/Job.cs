using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Scheduler
{
    public class Job
    {
        public Timer Timer { get; set; }
        public Object Obj { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
    }
}
