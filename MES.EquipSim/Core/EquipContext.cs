namespace MES.EquipSim.Core;

public class EquipContext
{
    public int EquipId {get; init; }
    public int TotalProd { get; set; }
    public int TotalDefect { get; set; }
    public double CurrentSpeed { get; set; } = 100;
    public EquipStatus Status { get; set; } = EquipStatus.Running;

    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Pressure { get; set; }

    public TimeSpan CycleTime => TimeSpan.FromMilliseconds(1000 / CurrentSpeed);
}