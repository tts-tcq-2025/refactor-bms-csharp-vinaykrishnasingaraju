using System;
using System.Threading;

namespace healthchecker
{
    public static class Checker
    {
        // --- Pure functions for vital checks --- //
        public static bool IsTemperatureOk(double temp)
        {
            return temp >= 95 && temp <= 102;
        }

        public static bool IsPulseOk(int pulse)
        {
            return pulse >= 60 && pulse <= 100;
        }

        public static bool IsSpo2Ok(int spo2)
        {
            return spo2 >= 90;
        }

        // --- Alert functions (side effects separated) --- //
        public static void BlinkAlert(int duration = 6)
        {
            for (int i = 0; i < duration; i++)
            {
                Console.Write("\r* ");
                Thread.Sleep(1000);
                Console.Write("\r *");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        public static void PrintAlert(string message)
        {
            Console.WriteLine(message);
            BlinkAlert();
        }

        // --- Mapping vital checkers to messages --- //
        public static bool VitalsOk(double temperature, int pulseRate, int spo2, Action<string>? alertFunc = null)
        {
            alertFunc ??= PrintAlert;

            if (!IsTemperatureOk(temperature))
            {
                alertFunc("Temperature critical!");
                return false;
            }
            if (!IsPulseOk(pulseRate))
            {
                alertFunc("Pulse Rate is out of range!");
                return false;
            }
            if (!IsSpo2Ok(spo2))
            {
                alertFunc("Oxygen Saturation out of range!");
                return false;
            }
            return true;
        }
    }
}
