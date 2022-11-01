using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleverTestTask1
{
    public static class Server
    {
        static int _count;
        static readonly object conch = new object();
        public static int GetCount()
        {
            if(Monitor.IsEntered(conch))
            {
                Monitor.Wait(conch);
            }
            return _count;
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
