namespace MES.Monitoring.UI.Models;

public class AlarmDto
{
    public int AlarmId { get; set; }
    public int EquipId { get; set; }
    public string Code { get; set; } = "";
    public string Message { get; set; } = "";
    public string Level { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime? AckAt { get; set; }
    public DateTime? ClearAt { get; set; }
}