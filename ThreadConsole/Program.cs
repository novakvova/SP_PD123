using Bogus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace ThreadConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Кількість ядер у системі {0}", Environment.ProcessorCount);
            Console.WriteLine("Main thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            Thread big_girl = new Thread(send_girl);
            big_girl.Start();

            var gen = new Faker<Gragon>()
                .RuleFor(x => x.Name, f => f.Person.FullName)
                .RuleFor(x => x.Image, f => f.Image.LoremFlickrUrl());

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var list = new List<Gragon>();
            for (int i = 0; i < 100000; i++)
            {
                //Thread.Sleep(500); //зупинка на пів секунди
                //Console.WriteLine("{0}", i);
                list.Add(gen.Generate());
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime Main Thread " + elapsedTime);
            //Thread.
            big_girl.Join();
            Console.WriteLine("Допобачення! Дякуємо, що ви були знами.");
            //Thread 
            
        }
        static void send_girl()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var gen = new Faker<Gragon>()
                .RuleFor(x => x.Name, f => f.Person.FullName)
                .RuleFor(x => x.Image, f => f.Image.LoremFlickrUrl());

            Console.WriteLine("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            var list = new List<Gragon>();
            for (int i = 0; i < 100000; i++)
            {
                //Thread.Sleep(500); //зупинка на пів секунди
                //Console.WriteLine("{0}", i);
                list.Add(gen.Generate());
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime Send Girl " + elapsedTime);
        }
    }
}
