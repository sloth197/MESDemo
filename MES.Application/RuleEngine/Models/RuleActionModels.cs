namespace MES.Application.RuleEngine.Models;

//JSON 룰 조건 모델
// -> 타입으로 종류 구분 / 필드로 비교
public class RuleConditionModel
{
    public string Type { get; set; } = "";
    //온도, 습도, 압력, 속도, 전압, 유량
    public string Field { get; set; } = "";
    public double? Value { get; set; } //단일 비교
    //between 비교
    public double? Min { get; set; } 
    public double? Max { get; set; }
    public double? Rate { get; set; } //수율 비교
}
public class RuleActionModel
{
    public RuleCommandActionModel? Command { get; set; }
    public RuleAlarmAction? Alarm { get; set; }
}
public class RuleCommandActionModel
{
    public string Type { get; set; } = "";
    public object Payload { get; set; } = new();
}
public class RuleAlarmActionModel
{
    public string Code { get; set; } = "";
    public string Message { get; set; } = "";
    public string Level { get; set; } = "Warning";
}