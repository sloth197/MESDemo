namespace MES.EquipSim.Models;

public class EquipCommandDto
{
    public int CommandId { get; set; }
    public string CommandType { get; set; }
    public string Payload { get; set; }
}