using System.Text;

namespace KymaSimulator
{
    internal class CounterSimulator
    {
        private readonly CounterValue[] _counterValues;
        public long ElapsedMilliseconds { get; private set; }

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

        public void UpdateValues(long elapsedMilliseconds)
        {
            ElapsedMilliseconds += elapsedMilliseconds;
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