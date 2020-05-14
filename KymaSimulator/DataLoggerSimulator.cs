using System.Diagnostics;
using System.Timers;

namespace KymaSimulator
{
    internal class DataLoggerSimulator
    {
        private readonly CounterSimulator _counterSimulator;
        private readonly Stopwatch _stopwatch;
        private readonly Timer _timer;

        public DataLoggerSimulator()
        {
            _counterSimulator = new CounterSimulator();
            _stopwatch = new Stopwatch();
            _timer = new Timer();
            _timer.Elapsed += Elapsed;
        }

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            var elapsed = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Restart();
            _counterSimulator.UpdateValues(elapsed);
        }

        public void Start()
        {
            _stopwatch.Restart();
            _timer.Start();
        }

        public string GetCounterString()
        {
            return _counterSimulator.GetCounterString();
        }
    }
}