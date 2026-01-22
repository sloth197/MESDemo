//DB에서 설정(룰) 가져옴
using MES.Domain.Entities;

namespace MES.Application.RuleEngine.Interfaces;

public interfaces IRuleDefinitionStore
{
    Task<List<RuleDefinition>> GetEnabledRulesAsync (int equipId);
}