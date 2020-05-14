using System;

namespace KymaSimulator
{
    internal static class Log
    {
        public static void Debug(string message)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: {message}");
        }
    }
}