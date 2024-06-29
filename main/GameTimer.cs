using System.Diagnostics;

namespace typeFighter
{
    class GameTimer
    {
        Stopwatch stopwatch;

        public GameTimer()
        {
            stopwatch = new Stopwatch();

        }

        public void Start()
        {
            this.stopwatch.Start();
        }

        public void Stop()
        {
            this.stopwatch.Stop();
        }

        // Require: stopwatch must have elasped
        public string toString()
        {
            TimeSpan elasped = stopwatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
            elasped.Hours, elasped.Minutes, elasped.Seconds);

            return elapsedTime;

        }
    }
}