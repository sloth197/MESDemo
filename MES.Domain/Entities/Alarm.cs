using MES.Domain.Enums;

namespace MES.Domain.Entities;

public class Alarm
{
    public int AlarmId { get; set; }
    public int EquipId { get; set; }
    public string Code { get; set; } = "";
    public string Message { get; set; } = "";
    public AlarmLevel Level { get; set; }
    public DateTime CreateAt { get; set; }
    //사용자가 알람을 확인
    public DateTime? AckAt { get; set; }
    //사용자가 알람을 처리
    public DateTime? ClearAt { get; set; }

    public bool IsAcked => AckAt.HasValue;
    public bool IsCleared => ClearAt.HsValue;
}