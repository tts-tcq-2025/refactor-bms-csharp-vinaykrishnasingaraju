using Xunit;

public class CheckerTests
{
    [Fact]
    public void TemperatureTooHigh_NotOk()
    {
        var status = Checker.EvaluateVitals(103f, 70, 95);
        Assert.Equal(VitalStatus.TemperatureOutOfRange, status);
    }

    [Fact]
    public void TemperatureTooLow_NotOk()
    {
        var status = Checker.EvaluateVitals(94f, 70, 95);
        Assert.Equal(VitalStatus.TemperatureOutOfRange, status);
    }

    [Fact]
    public void PulseTooHigh_NotOk()
    {
        var status = Checker.EvaluateVitals(98f, 120, 95);
        Assert.Equal(VitalStatus.PulseOutOfRange, status);
    }

    [Fact]
    public void PulseTooLow_NotOk()
    {
        var status = Checker.EvaluateVitals(98f, 50, 95);
        Assert.Equal(VitalStatus.PulseOutOfRange, status);
    }

    [Fact]
    public void OxygenTooLow_NotOk()
    {
        var status = Checker.EvaluateVitals(98f, 70, 85);
        Assert.Equal(VitalStatus.OxygenOutOfRange, status);
    }

    [Fact]
    public void AllVitalsNormal_Ok()
    {
        var status = Checker.EvaluateVitals(98.6f, 72, 97);
        Assert.Equal(VitalStatus.Normal, status);
    }

    [Theory]
    [InlineData(95f, 60, 90)]   // Edge case lower bounds
    [InlineData(102f, 100, 90)] // Edge case upper bounds
    public void EdgeCases_StillOk(float temp, int pulse, int spo2)
    {
        var status = Checker.EvaluateVitals(temp, pulse, spo2);
        Assert.Equal(VitalStatus.Normal, status);
    }
}
