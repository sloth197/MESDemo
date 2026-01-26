namespace MES.Domain.Entities;

public class EquipSample
{
    public int SampleId { get; set; }
    public int EquipId { get; set; }
    public DateTime Timestamp { get; set; }

    public int ProdCount { get; set; }
    public int DefectCount { get; set; }

    public double Temperature { get; set; }
    public double Speed { get; set; }
    public double Humidity { get; set; }
    public double Pressure { get; set; }
    public double Voltage { get; set; }
    public double FlowRate { get; set; }
}