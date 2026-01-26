using System.Text.Json;
using MES.Application.RuleEngine.Interfaces;
using MES.Application.RuleEngine.Models;
using MES.Domain.Entities;
using MES.Domain.Enums;
using MES.Infrastructure.Persistence;

namespace MES.Application.RuleEngine;

//데이터베이스에 저장된 JSON룰 기반으로 커맨드,알람 생성
// -> 룰 추가/변경 시 DB만 수정
public class DbJsonRuleEngineService : IRuleEngine
{
    private readonly IRuleDefinitionStore _store;
    private readonly MesDbContext _db;

    public DbJsonRuleEngineService (IRuleDefinietionStore store, MesDbContext db)
    {
        _store = store;
        _db = db;
    }
    public async Task EvaluateAsync (EquipSample sample)
    {
        var rules = await _store.GetEnableRulesAsync (sample.EquipId);
        foreach (var rule in rules)
        {
            //조건 해석
            var condition = JsonSerializer.Deserialize <RuleConditionModel>
                            (rule.ConditionJson);
            if (condition == null)
            {
                continue;
            }
            //조건 평가
            else if (!IsTriggered(condition, sample))
            {
                continue;
            }
            //액션 해석
            var action = JsonSerializer.Deserialize <RuleActionModel>
                         (rule.ActionJson);
            if (action == null)
            {
                continue;
            }
            //커맨드 생성
            else if (action.Command != null)
            {
                var payloadJson = JsonSerializer.Serialize(action.Command.Payload);
                _db.EquipCommands.Add(new EquipCommand
                {
                    EquipId = sample.EquipId,
                    CommandType = action.Command.Type,
                    Payload = payloadJson,
                    Status = CommandStatus.pending,
                    CreatedAt = DateTime.Now
                });
            }
            //알람 생성
            else if (action.Alarm != null)
            {
                _db.Alarms.Add (new Alarm
                {
                    EquipId = sample.EquipId,
                    Code = action.Alarm.Code,
                    Message = action.Alarm.Message,
                    Level = ParseAlarmLevel(action.Alarm.Level),
                    CreatedAt = DateTime.Now 
                });
            }
        }
        await _db.SaveChangesAsync();
    }
    //JSON조건과 실제 샘플값 비교
    private static bool IsTriggered(RuleConditionModel condition, EquipSample sample)
    {
        // 샘플에서 비교값 필드에서 바로 선택
        double fieldValue = condition.Field switch
        {
            "Temperature" => sample.Temperature,
            "Humidity" => sample.Humidity,
            "Pressure" => sample.Pressure,
            "Speed" => sample.Speed,
            "Voltage" => sample.Voltage,
            "FlowRate" => sample.FlowRate,
            "DefectCount" => sample.DefectCount,
            "ProdCount" => sample.ProdCount,
            _ => double.NaN
        };

        if (double.IsNaN(fieldValue))
        {
            return false;
        }

        return condition.Type switch
        {
            //단순 비교
            "GreaterThan" => condition.Value.HasValue && fieldValue > condition.Value.Value,
            "LessThan" => condition.Value.HasValue && fieldValue < conditionValue.Value,
            //범위 비교(온도, 습도, 압력, 속도, 전압, 유량)
            "Between" => condition.Min.HasValue && condition.Max.HasValue &&
            fieldValue >= condition.Min.Value && field <= condition.Max.Value,
            //불량률 기반 룰
            "DefectRateGreaterThan" => condition.Rate.HasValue && CalculateDefectRate(sample) > condition.Rate.Value, _=> false
            //"TemperatureGreaterThan" => sample.Temperature > contion.Value,
            //"SpeedLessThan" => sample.Speed < condtion.Value,
            //"DefectCountGreaterThan" => sample.DefectCount > condition.Value,
            _ => false
        };
    }

    private static double CalculateDefectRate(EquipSample sample)
    {
        if (sanple.Prodcount <= 0) 
        {
            return 0;
        }
        return (double)sample.DefectCount / sample.ProdCount;
    }

    private static AlarmLevel ParseAlarmLevel(string level)
    {
        return level switch
        {
            "Info" = AlarmLevel.Info,
            "Warning" = AlarmLevel.Warning,
            "Critical Demage" = AlarmLevel.Critical,
            _ => AlarmLevel.Warning
        };
    }
}