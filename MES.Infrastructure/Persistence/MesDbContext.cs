//Add Db Set
using Microsoft.EntityFrameworkCore;
using MES.Domain.Entities;

namespace MES.Infrastructure.Persistence;

public class MesDbContext : MesDbContext
{
    public MesDbContext(DbContextOptions<MesDbContext> options) : base(options)
    {
    }
    public Dbset<EquipSample> EquipSamples => Set<EquipSample>();
    public DbSet<EquipCommand> EquipCommands => Set<EquipCommand>();
    public DbSet<Alarm> Alarms => Set<Alarm>();
}
