using MES.Domain.Entities;

namespace MES.Application.RuleEngine;

public interface IRule
{
    RuleResult Evaluate(EquipSample sample);
}