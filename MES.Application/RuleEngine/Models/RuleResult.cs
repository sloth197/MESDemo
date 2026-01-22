namespace MES.Application.RuleEngine.Models;

public class RuleResult
{
    public bool Triggered { get; set; }
    public EquipCommand? Command { get; set; }
    public Alarm? Alarm { get; set; }
}