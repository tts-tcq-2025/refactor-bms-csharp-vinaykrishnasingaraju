using System;
using System.Threading;

public class Checker
{
    //  Pure function (CC = 2)
    public static VitalStatus EvaluateVitals(float temperature, int pulseRate, int spo2)
    {
        if (!(temperature >= 95 && temperature <= 102))
            return VitalStatus.TemperatureOutOfRange;

        if (!(pulseRate >= 60 && pulseRate <= 100))
            return VitalStatus.PulseOutOfRange;

        if (spo2 < 90)
            return VitalStatus.OxygenOutOfRange;

        return VitalStatus.Normal;
    }

    // I/O handling, separated from logic (CC = 2)
    public static bool VitalsOk(float temperature, int pulseRate, int spo2)
    {
        var status = EvaluateVitals(temperature, pulseRate, spo2);

        if (status == VitalStatus.Normal)
        {
            Console.WriteLine("Vitals received within normal range");
            Console.WriteLine("Temperature: {0}, Pulse: {1}, SO2: {2}", temperature, pulseRate, spo2);
            return true;
        }

        ReportIssue(status);
        return false;
    }

    private static void ReportIssue(VitalStatus status)
    {
        string message = status switch
        {
            VitalStatus.TemperatureOutOfRange => "Temperature critical!",
            VitalStatus.PulseOutOfRange       => "Pulse Rate is out of range!",
            VitalStatus.OxygenOutOfRange      => "Oxygen Saturation out of range!",
            _                                 => "Unknown issue"
        };

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
