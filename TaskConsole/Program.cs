using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            //3.5
            //Продвінутий Thead
            //Таsk працює у окремому потоці і програма завершує совю роботу
            //не дочекавшись завершення роботи потока 
            Task task = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(500);
                    int theadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Hello my task run " + theadId);
                }
            });
            task.Start();
            task.Wait();
            Console.WriteLine("Головна програма завершила роботу: "+ Thread.CurrentThread.ManagedThreadId);

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("Головний потік:");
            //    Thread.Sleep(500);
            //}

        }
    }
}
