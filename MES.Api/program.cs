//Registration DI
builder.Services.AddScoped<AlarmService>();
builder.Services.AddScoped<IRuleDefinitionStore, RuleDefinitionSotre>();
builder.Services.AddScoped<IRuleEngine, DbJsonRuleEngineService>();