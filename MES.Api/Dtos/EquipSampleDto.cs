namespace MES.Api.Dtos;

public class EquipSampleDto
{
    public int EquipId { get; set; }
    public DateTime Timestamp { get; set; }
    public int ProdCount { get; set; }
    public int DefectCount { get; set; }
    //공정 변수
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Pressure { get; set; }
    public double Speed { get; set; }
    public double Voltage { get; set; }
    public double FlowRate { get; set; }
    //상태
    public string Status { get; set; } = "Running"; 
}