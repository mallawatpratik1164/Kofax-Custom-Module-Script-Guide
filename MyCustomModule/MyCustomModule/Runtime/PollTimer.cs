using System.Timers;

namespace MyCustomModule.Runtime
{
    internal class PollTimer
    {
        private const int BATCH_POLLING_INTERVAL = 1000;

        private readonly Timer pollingTimer;

        public PollTimer(bool startOnInit, ElapsedEventHandler onPollingTimerTick)
        {
            pollingTimer = new Timer(BATCH_POLLING_INTERVAL);
            pollingTimer.Elapsed += onPollingTimerTick;
            SetTimerState(startOnInit);
        }

        public void SetTimerState(bool isActive)
        {
            pollingTimer.Enabled = isActive;
        }
    }
}