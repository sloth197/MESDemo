//Registration DI
builder.Services.AddScoped<IRule, HighTemperatureRule>();
builder.Services.AddScoped<IRule, HighDefectRateRule>();
builder.Services.AddScoped<IRuleEngine, RuleEngineService>();