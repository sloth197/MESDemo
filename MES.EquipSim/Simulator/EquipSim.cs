namespace MES.EquipSim.Simulator;

public class EquipSim
{
    private readonly EquipContext _ctx;
    private readonly SensorGenerator _sensor;
    private readonly ProductionGenerator _prod;
    private readonly EquipStateMachine _state;

    public EquipSim(EquipContext ctx)
    {
        _ctx = ctx;
        _sensor = new SensorGenerator();
        _prod = new ProductionGenerator();
        _state = new EquipStateMachine();
    }

    public EquipSampleDto GenerateSample()
    {
        _sensor.Update(_ctx);
        _prod.Produce(_ctx);
        _state.UpdateStatus(_ctx);

        return new EquipSampleDto
        {
            EquipId = _ctx.EquipId,
            Timestamp = Datetime.Now,
            ProdCount = _ctx.TotalProd,
            DefectCount = _ctx.TotalDefect,
            Temperature = _ctx.Temperature,
            Speed = _ctx.CurrentSpeed,
            Status = _ctx.Status.ToString()
        };
    }
}