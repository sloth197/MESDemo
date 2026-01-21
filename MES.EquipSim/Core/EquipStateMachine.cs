namespace MES.EquipSim.Core;

public clas EquipStateMachine
{
    public void UpdateStatus (EquipContext ctx)
    {//장비의 온도에 따라  작동,경고,알람 상태 변경
        if(ctx.Temperature > 80)
        {
            ctx.Status = EquipStatus.Alarm;
        }
        else if(ctx.Temperature > 70)
        {
            ctx.Status = EquipStatus.Warning;
        }
        else 
        {
            ctx.Status = EquipStatus.Running;
        }
    }
}
public enum EquipStatus
{
    Running,
    Warning,
    Alarm
}