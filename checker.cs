using System;
using System.Threading;

class Checker
{
    // --- Pure functions for vital checks --- //
    public static bool IsTemperatureOk(float temp) => temp >= 95 && temp <= 102;
    public static bool IsPulseOk(int pulse) => pulse >= 60 && pulse <= 100;
    public static bool IsSpo2Ok(int spo2) => spo2 >= 90;

    // --- Alert functions --- //
    private static void BlinkAlert(int duration = 6)
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

    private static void PrintAlert(string message)
    {
        Console.WriteLine(message);
        BlinkAlert();
    }

    // --- Main vitals check --- //
    public static bool VitalsOk(float temperature, int pulseRate, int spo2)
    {
        if (!IsTemperatureOk(temperature))
        {
            PrintAlert("Temperature critical!");
            return false;
        }

        if (!IsPulseOk(pulseRate))
        {
            PrintAlert("Pulse Rate is out of range!");
            return false;
        }

        if (!IsSpo2Ok(spo2))
        {
            PrintAlert("Oxygen Saturation out of range!");
            return false;
        }

        Console.WriteLine("Vitals received within normal range");
        Console.WriteLine($"Temperature: {temperature}, Pulse: {pulseRate}, SpO2: {spo2}");
        return true;
    }
}
