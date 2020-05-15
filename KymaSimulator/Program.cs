using System;
using System.Threading;

namespace KymaSimulator
{
    internal class Program
    {
        private static Simulator _simulator;
        private static DateTime _startTime;

        private static void Main()
        {
            Console.WriteLine("--- Kyma Simulator ---");
            Console.WriteLine("Press Q to quit, D to show time diff, T to show elapsed time, any other key to show counter values");
            _simulator = new Simulator();

            _startTime = DateTime.UtcNow;
            Log.Debug("Starting...");
            _simulator.Start();

            Log.Debug(_simulator.DataLogger.GetCounterString());
            do
            {
            } while (HandleInput(Console.ReadKey(true)));

            Log.Debug("Done");
            Thread.Sleep(500);
        }

        private static bool HandleInput(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Q: 
                    return false;
                case ConsoleKey.D:
                    Log.Debug($"Timing difference: {_simulator.DataLogger.GetTimeDifference():P}");
                    break;
                case ConsoleKey.T:
                    var elapsed = DateTime.UtcNow - _startTime;
                    Log.Debug($"Elapsed {elapsed.TotalSeconds:#.000} (0x{Convert.ToInt32(elapsed.TotalSeconds):X8}) sec since start {_startTime.ToLocalTime():HH:mm:ss.fff}");
                    break;
            }
            Log.Debug(_simulator.DataLogger.GetCounterString());
            return true;
        }
    }
}
