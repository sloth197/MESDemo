namespace MES.Domain.Entities;

public class EquipCommand
{
    public int CommandId { get; set; }
    public int EquipId { get; set; }
    public string CommandType { get; set; } = "";
    public string Payload { get; set; } = "";
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
    public DateTime? Applied { get; set; }   
}