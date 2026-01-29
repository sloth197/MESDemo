//Registration DI
using MES.Api.Services;

var builder = WebApplica tion.CreateBuilder(args);

//Controllers
builder.Services.AddControllers();

builder.Services.AddSingleton<SampleIngestService>();
builder.Services.AddSingleton<CommandsService>();

builder.Services.AddScoped<AlarmService>();
builder.Services.AddScoped<IRuleDefinitionStore, RuleDefinitionSotre>();
builder.Services.AddScoped<IRuleEngine, DbJsonRuleEngineService>();

//Swagger
builder.Services.AddEndPointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();