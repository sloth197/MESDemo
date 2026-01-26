namespace MES.Api.Dtos;

//EquipSim -> Command 적용 -> Ack Dto 
public class CommandAckDto
{
    public int CommandId { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}