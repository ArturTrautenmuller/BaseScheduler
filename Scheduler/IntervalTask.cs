using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class IntervalTask
    {
        public int JobId { get; set; }
        public DateTime NextExecution { get; set; }
        public TimeSpan Interval { get; set; }
    }
}
