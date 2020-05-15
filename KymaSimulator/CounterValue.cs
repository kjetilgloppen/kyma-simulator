using System;

namespace KymaSimulator
{
    internal class CounterValue
    {
        public int Value => (int) Convert.ToUInt64(Math.Truncate(_internalValue));

        private decimal _internalValue;
        private readonly decimal _internalRate;

        public CounterValue(int pulsesPerUnit, double ratePerHour, decimal initialValue = 0)
        {
            // Calculate rate per milliseconds used to update internal value
            // A flow rate of 3600 kg/hr with 1 pulses per kilo should give a rate of 1/sec or 0,001/ms
            // A flow rate of 180 kg/hr with 10 pulses per liter should give a rate of 0.5ec or 0,0005/ms
            _internalRate = pulsesPerUnit * (decimal)ratePerHour / 3600 / 1000;
            _internalValue = initialValue;
        }

        public void UpdateValue(decimal elapsedMilliseconds)
        {
            _internalValue += _internalRate * elapsedMilliseconds;
        }
    }
}