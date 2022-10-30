using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleverTestTask1
{
    public static class Server
    {
        static int _count;
        static readonly object conch = new object();
        static Func<int> readCount = new Func<int>(() => _count);
        public static int GetCount()
        {
            if(Monitor.IsEntered(conch))
            {
                Monitor.Wait(conch);
            }
            return Task.Run(new Func<int>(readCount)).Result;
        }
        public static void AddToCount(int value)
        {
            lock(conch)
            {
                _count += value;
            }
        }
    }
}
