using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using static Scheduler.MainScheduler;

namespace Scheduler
{
    public class Job
    {
        public Timer Timer { get; set; }
        public Object Obj { get; set; }
        public DateTime DateTime { get; set; }
        public TaskType Type { get; set; }
    }
}
