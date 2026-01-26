namespace MES.Api.Dtos;

public class CommandDto
{
    public int CommandId { get; set; }
    public int EquipId { get; set; }
    //속도 변화
    public string CommandType { get; set; } = "";
    //JSON 문자열
    public string Payload { get; set; } = "";
    //보류, 수락, 실패
    public string Status { get; set; } = "Pending";
}