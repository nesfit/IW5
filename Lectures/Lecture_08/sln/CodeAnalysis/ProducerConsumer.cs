using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Program
    {
        const int size = 100;
        static Queue<int> queue;
        static object lockObj;

        static void Producer()
        {
            for (var i = 3; i >= -1; i--)
            {
                lock (lockObj)
                {
                    if (queue.Count == size)
                        Monitor.Wait(lockObj);
                    queue.Enqueue(i);
                    Monitor.Pulse(lockObj);
                }
            }
        }

        static void Consumer()
        {
            var item = 0;
            do
            {
                lock (lockObj)
                {
                    if (queue.Count == 0)
                        Monitor.Wait(lockObj);
                    item = queue.Dequeue();
                    Monitor.Pulse(lockObj);
                }
            } while (item != -1);
        }

        public static void Main(string[] args)
        {
            queue = new Queue<int>(size);
            lockObj = new object();

            var producer = Task.Run(() => Producer());
            var consumer = Task.Run(() => Consumer());
            Task.WaitAll(producer, consumer);
        }
    }
}
