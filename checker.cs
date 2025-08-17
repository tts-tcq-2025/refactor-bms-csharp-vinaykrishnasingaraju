using System;
using System.Threading;

public class Checker
{
    // Pure function that checks vitals and returns a result
    public static VitalStatus EvaluateVitals(float temperature, int pulseRate, int spo2)
    {
        if (temperature > 102 || temperature < 95)
        {
            return VitalStatus.TemperatureOutOfRange;
        }
        if (pulseRate < 60 || pulseRate > 100)
        {
            return VitalStatus.PulseOutOfRange;
        }
        if (spo2 < 90)
        {
            return VitalStatus.OxygenOutOfRange;
        }
        return VitalStatus.Normal;
    }

    // Handles side effects (I/O + blinking)
    public static bool VitalsOk(float temperature, int pulseRate, int spo2)
    {
        var status = EvaluateVitals(temperature, pulseRate, spo2);

        switch (status)
        {
            case VitalStatus.TemperatureOutOfRange:
                ReportIssue("Temperature critical!");
                return false;

            case VitalStatus.PulseOutOfRange:
                ReportIssue("Pulse Rate is out of range!");
                return false;

            case VitalStatus.OxygenOutOfRange:
                ReportIssue("Oxygen Saturation out of range!");
                return false;

            case VitalStatus.Normal:
                Console.WriteLine("Vitals received within normal range");
                Console.WriteLine("Temperature: {0}, Pulse: {1}, SO2: {2}", temperature, pulseRate, spo2);
                return true;
        }
        return true;
    }

    private static void ReportIssue(string message)
    {
        Console.WriteLine(message);
        BlinkAlert(6, 1000);
    }

    private static void BlinkAlert(int times, int intervalMs)
    {
        for (int i = 0; i < times; i++)
        {
            Console.Write("\r* ");
            Thread.Sleep(intervalMs);
            Console.Write("\r *");
            Thread.Sleep(intervalMs);
        }
        Console.WriteLine();
    }
}

public enum VitalStatus
{
    Normal,
    TemperatureOutOfRange,
    PulseOutOfRange,
    OxygenOutOfRange
}
