using MES.Application.RuleEngine.Interfaces;
using MES.Domain.Entities;
using MES.Infrastructure. Persistence;
using Microsoft.EntityFrameworkCore;

namespace MES.Infrastructure.Repositories;

public class RuleDefinitionStore : IRuleDefinitionsStore
{
    private readonly MesDbContext _db;
    public RuleDefinitionStore(MesDbContext db)
    {
        _db = db;
    }

    public async Task<List<RuleDefinition>> GetEnabledRulesAsync (int EquipId)
    {
        //EquipId == 0 -> Common Rule
        return await _db.RuleDefinitions
            .Where (r => r.Enabled && (r.EquipId == 0 || r.EquipId == equipId))
            .OrderBy (r => r. RuleDefinitionId)
            .ToListAsync();
    }
}