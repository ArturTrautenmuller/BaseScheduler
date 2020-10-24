using System;

namespace Scheduler
{
    class Program
    {
        public static void Execute(Object msg)
        {
            Console.WriteLine(msg);
        }

        public static void Refresh()
        {
            Console.WriteLine("Refreshing...");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MainScheduler scheduler = new MainScheduler(10000); // define em 10s o tempo de iniciaização
            scheduler.execute = Execute;      // atribui a função Execute
            scheduler.refresh = Refresh;      // atribui a função Refresh
           
            // criar um agendamento unico para daqui a 20 segundos.
            DateTime time = DateTime.Now.AddSeconds(20);   
            scheduler.AddJob(1,"First Job",time);

            // criar uma task diaria para executar sempre as 19:08:45
            scheduler.AddDailyTask(2, "First Daily Task", "19:08:45");

            // inicia o scheduler
            scheduler.Init();


            Console.ReadKey();
        }
    }
}
