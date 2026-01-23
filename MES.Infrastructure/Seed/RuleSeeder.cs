using MES.Domain.Entities;

namespace MES.Infrastructure.Seed;

public static class RuleSeeder
{
    public static Lst<RuleDefinition> GetDefaultRules()
    {
        return new List<RuleDefinition>
        {
            new RuleDefinition
            {
                // 새로운 룰 설정(일정 온도 이상 -> 속도 변화 + 알람)
                // 온도: 80 이상 -> 속도 60으로 변경 
                Name = "High Temp -> ChangeSpeed + Alarm",
                EquipId = 0,
                Enabled = true,
                ConditionJson = """{"type": "TemperatureGreaterThan", "value": 80} """,
                ActionJson = """{"command": {"type": "ChangeSpeed", "payload": {"speed": 60}}, "alarm": {"code": "TEMP_HIGH", "message": "Temperature exceeded threshold", "level": "Warning"}}"""
            }
        };
    }
}