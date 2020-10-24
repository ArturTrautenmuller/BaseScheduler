using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Scheduler
{
    public class MainScheduler
    {
        public MainScheduler(long refreshTime) 
        {
            this.Jobs = new Dictionary<int, Job>();
            this.DailyTasks = new Dictionary<int, DailyTask>();
            this.refreshTime = refreshTime;
            

           
        }

       public void Init()
        {
            RefreshTimer = new Timer();
            RefreshTimer.Elapsed += new ElapsedEventHandler((o, e) => { refresh(); ResetRefreshTimer(); });
            RefreshTimer.Interval = this.refreshTime;
            RefreshTimer.Enabled = true;
        }

        public delegate void Execute(Object taskData);

        public Execute execute { get; set; }

        public delegate void Refresh();

        public Refresh refresh { get; set; }

        public  long refreshTime { get; set; }

        public Dictionary<int,Job> Jobs { get; set; }

        public Dictionary<int,DailyTask> DailyTasks { get; set; }

        private Timer RefreshTimer { get; set; }

        public void AddJob(int Id, Object obj , DateTime time, string type = "normal")
        {
           
            var taskTimer = new Timer();
            taskTimer.Elapsed += new ElapsedEventHandler((o, e) => { execute(obj); FinnishJob(Id); });
            double interval = (time - DateTime.Now).TotalMilliseconds;
            if (interval <= 0)
            {
                throw new Exception("Time Expired!");
            }

            taskTimer.Interval = interval;
            taskTimer.Enabled = true;

            Job job = new Job();
            job.DateTime = time;
            job.Obj = obj;
            job.Timer = taskTimer;
            job.Type = type;

            this.Jobs.Add(Id, job);
        }

       

        public void RemoveJob(int Id)
        {
            Jobs[Id].Timer.Enabled = false;
            this.Jobs.Remove(Id);
        }

        public void AddDailyTask(int Id, Object obj, string time)
        {
            DailyTask dailyTask = new DailyTask();
            dailyTask.JobId = Id;
            dailyTask.Time = time;

            this.DailyTasks.Add(Id, dailyTask);

            DateTime dateTime = Convert.ToDateTime(time);
            if(DateTime.Compare(dateTime,DateTime.Now) < 0)
            {
                dateTime.AddDays(1);
            }

            AddJob(Id, obj, dateTime,"daily");


        }

        public void RemoveDailyTask(int Id)
        {
            DailyTasks.Remove(Id);
            RemoveJob(Id);
        }

        public void FinnishJob(int Id)
        {

            switch (Jobs[Id].Type)
            {
                case "daily": {
                        DateTime dateTime = Convert.ToDateTime(DailyTasks[Id].Time);
                        if (DateTime.Compare(dateTime, DateTime.Now.AddSeconds(1)) < 0)
                        {
                           dateTime =  dateTime.AddDays(1);
                        }
                        Object obj = Jobs[Id].Obj;
                        RemoveJob(Id);
                        AddJob(Id, obj, dateTime, "daily");

                        break;
                    }

                default:{
                        
                        RemoveJob(Id);
                        break;
                    }
            }

           

        }
        
        private void ResetRefreshTimer()
        {
            this.RefreshTimer.Interval = this.refreshTime;
        }


    }
}
