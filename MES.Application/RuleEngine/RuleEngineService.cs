public class RuleEngineService : IRuleEngine
{
    private readonly IEnumerable<IRule> _rules;
    private readonly MesDbContexty _db;

    public RuleEngineService(Ienumerable<IRule> rules, MesDbContext db)
    {
        _rules = rules;
        _db = db;
    }
    public async Task EvaluaterAsync (EquipSample sample)
    {
        foreach (var rule in _rules)
        {
            var result = rule.Evaluate(sample);
            if (result.Triggered)
            { 
                continue;
            }
            else if (result.Command != null)
            {
                _db.EquipCommands.Add(result.Command);
            }
            else if (result.Alarm != null)
            {
                _db.Alarms.Add(result.Alarm);
            }
        }
        await _db.SaveChangedsAsync();
    }
}