namespace MES.EquipSim.Services;

public class CommandExecutor
{
    public bool Execute(EquipContext ctx, EquipCommandDto cmd)
    {
        try
        {
            if (cmd.CommandType == "ChangeSpeed")
            {
                var json = JsonDocument.Parse(cmd.Payload);
                ctx.CurrentSpeed = json.RootElement.GetProperty("speed").GetDouble();
            }
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}