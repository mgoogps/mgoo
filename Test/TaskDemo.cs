using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public class TaskDemo
    {

        public void action()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int[] ints = new int[]{ 7,81,578,5,6,5,2,2,2,78,54,578,};
             
            List<Task> taskList = new List<Task>();
            TaskFactory taskFactory = new TaskFactory();
            for (int i = 0; i < 10; i++)
            {
                int index = i;
                Task.Run(()=> {
                    int sleep = new Random().Next(3000);
                    Thread.Sleep(sleep);
                    Console.WriteLine("i:" + index + ",sleep:" + sleep + ",name:" + Thread.CurrentThread.ManagedThreadId+ ",ints["+index+"]:" + ints[index]);
                });
                //taskFactory.StartNew(() =>
                //{ 
                //    int sleep = new Random().Next(3000);
                //    Thread.Sleep(sleep);
                //    Console.WriteLine("i:" + i + ",sleep:" + sleep + ",name:" + Thread.CurrentThread.ManagedThreadId);
                //});
            }
            //Task.WaitAll(taskList.ToArray());
            sw.Stop();
            Console.WriteLine("执行完，用时："+(sw.ElapsedMilliseconds/1000)+"秒");
        }

    }
}
