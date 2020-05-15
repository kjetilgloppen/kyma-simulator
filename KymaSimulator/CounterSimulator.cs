using System.Text;

namespace KymaSimulator
{
    internal class CounterSimulator
    {
        private decimal _actualElapsedMilliseconds;
        private readonly CounterValue[] _counterValues;
        private decimal _intervalElapsedMilliseconds;
        public long ElapsedMilliseconds => (long) _actualElapsedMilliseconds;  

        public decimal TimeDifference
        {
            get
            {
                var actual = _actualElapsedMilliseconds;
                var interval = _intervalElapsedMilliseconds;
                return (actual - interval) / interval;
            }
        }

        public CounterSimulator()
        {
            _counterValues = new[]
            {
                new CounterValue(1, 100),
                new CounterValue(10, 180),
                new CounterValue(1, 3600),
                new CounterValue(1, 7200),
                new CounterValue(10, 7200),
                new CounterValue(1, 360),
                new CounterValue(1, 3600, uint.MaxValue - 60),
                new CounterValue(400, 16.00)
            };
        }

        public void UpdateValues(double timerInterval, decimal elapsedMilliseconds)
        {
            _intervalElapsedMilliseconds += (decimal)timerInterval;
            _actualElapsedMilliseconds += elapsedMilliseconds;
            foreach (var counterValue in _counterValues)
            {
                counterValue.UpdateValue(elapsedMilliseconds);
            }
        }

        public string GetCounterString()
        {
            var result = new StringBuilder();
            foreach (var counterValue in _counterValues)
            {
                result.AppendFormat("{0:X8},", counterValue.Value);
            }

            result.AppendFormat("{0:X8}", ElapsedMilliseconds);
            return result.ToString();
        }
    }
}