var ctx = new EquipContext { EquipId = 1 };
var sim = new EquipSim(ctx);
var api = new MesApiClient("http://localhost:8080");
var executor = new CommandExecutor();

while(true)
{
    var sample = sim.GeneratorSample();
    await api.SendSampleAsync(sample);
    var commands = await api.GetPendingCommands(ctx.EquipId);

    if (commands != null)
    {
        foreach(var cmd in commands)
        {
            var result = executor.Execute(ctx, cmd);
            await api.AckAsync(new CommandAckDto
            {
                CommandId = cmd.CommandId, Success = result
            });
        }
    }
    await Task.Delay(ctx.CycleTime);
}