using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace aop
{
    public interface IWorker
    {
        void DoWork();
    }

    public class Worker : IWorker
    {
        private int _idx;

        public Worker()
        {
            _idx = 0;
        }

        public void DoWork()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {++_idx}");
        }
    }
}
