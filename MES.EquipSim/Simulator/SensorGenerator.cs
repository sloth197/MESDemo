namespace MES.EquipSim.Simulator;

public class SensorGenelator
{
    private readonly Random _read = new();
    public void Update(EquipContext ctx)
    {
        ctx.Temperature = _rand.Next(60, 100);
        ctx.Humidity = _rand.Next(30, 90);
        ctx.Pressure = _rand.Next(90, 110);
    }
}