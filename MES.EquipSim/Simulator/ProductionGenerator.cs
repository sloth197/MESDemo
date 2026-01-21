namespace  MES.EquipSim.Simulator;

public class ProductionGenerator
{
    private readonly Random _rand = new();
    public void Produce(EquipContext ctx)
    {
        var prod = _rand.Next(1, 5);
        var defect = _rand.Next(0, 2);

        ctx.TotalProd += prod;
        ctx.TotalDefect += defect;
    }
}