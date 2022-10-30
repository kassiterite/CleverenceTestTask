using System;
using System.Threading;

namespace CleverTestTask2
{
    /// <summary>
    /// Class to a semi-async call delegates
    /// </summary>
    public class AsyncCaller
    {
        public EventHandler EventHandler { get; }

        /// <summary>
        /// AsyncCaller Constructor
        /// </summary>
        public AsyncCaller(EventHandler eventHandler) => EventHandler = eventHandler;

        /// <summary>
        /// Semi-async call delegate
        /// </summary>
        /// 
        /// <returns>
        /// <list><listheader></listheader>
        /// <item>
        /// <term>true</term>
        /// the delegate was successfully invoked before the specified number of milliseconds elapsed
        /// </item>
        /// <item>
        /// <term>false</term>
        /// the delegate has not been called within the specified number of milliseconds
        /// </item>
        /// </list>
        /// </returns>
        /// 
        /// <param name="milliseconds">The time given to execute the delegate, must be greater than zero </param>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An object that can contains event data</param>
        public bool InvokeAsync(int milliseconds, object sender, EventArgs e)
        {
            if (milliseconds < 0)
                throw new ArgumentOutOfRangeException("Milliseconds amount is less than zero");
            Thread thread = new Thread(() => Thread.Sleep(milliseconds));
            var ar = EventHandler.BeginInvoke(sender, e, x => thread?.Abort(), null);
            thread.Start();
            thread.Join();
            return ar.IsCompleted;
        }
    }
}
