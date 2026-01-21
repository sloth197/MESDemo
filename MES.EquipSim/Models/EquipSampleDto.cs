namespace MES.EquipSim.Models;

public class EquipSampleDto
{
    public int EquipId { get; set; }
    public int ProdCount { get; set; }
    public int DefectCount { get; set; }
    public double Temperature { get; set; }
    public double Speed { get; set; }
    public string Status { get; set; }
    public DateTime Timestamp { get; set; }
}