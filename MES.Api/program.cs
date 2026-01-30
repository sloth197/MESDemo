//Registration DI
using MES.Api.Services;
using MES.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Controllers
builder.Services.AddControllers();
//EF core
builder.Services.AddDbContext<MesDbContext>(options => 
{
    options.UseSqlServer(builder.Configuratuin.GetConnectionString("MesDb");)
});

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