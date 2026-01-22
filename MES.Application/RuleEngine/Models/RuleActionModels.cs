namespace MES.Application.RuleEngine.Models;

public class RuleConditionModel
{
    public string Type { get; set; } = "";
    public double Value { get; set; }
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