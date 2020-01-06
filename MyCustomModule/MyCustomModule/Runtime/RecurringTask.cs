using System;
using System.Timers;

namespace MyCustomModule.Runtime
{
    /// <summary>
    /// Wrapper for recurring delayed execution of a function
    /// </summary>
    public class RecurringTask
    {
        /// <summary>
        /// The timer
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// Returns the timer state and enables/disables it
        /// </summary>
        public bool Enabled
        {
            get
            {
                return timer.Enabled;
            }
            set
            {
                timer.Enabled = value;
            }
        }

        /// <summary>
        /// The execution interval in milliseconds
        /// </summary>
        public double TimerIntervalInMilliseconds { get { return timer.Interval; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="task">The task to execute</param>
        /// <param name="intervalInMilliseconds">The execution interval in milliseconds</param>
        /// <param name="initialState">Enable the task directly after creating the instance</param>
        public RecurringTask(Action<RecurringTask> task, double intervalInMilliseconds = 1000, bool initialState = false)
        {
            timer = new Timer(intervalInMilliseconds);

            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                task(this);
            };

            Enabled = initialState;
        }
    }
}
