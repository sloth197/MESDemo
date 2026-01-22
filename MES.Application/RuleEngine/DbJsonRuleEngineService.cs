using System.Text.Json;
using MES.Application.RuleEngine.Interfaces;
using MES.Application.RuleEngine.Models;
using MES.Domain.Entities;
using MES.Domain.Enums;
using MES.Infrastructure.Persistence;

namespace MES.Application.RuleEngine;

//데이터베이스에 저장된 JSON룰 기반으로 커맨드,알람 생성
// -> 룰 추가/변경 시 DB만 수정
public class DbJsonRuleEngineService : IRuleEngine
{
    private readonly IRuleDefinitionStore _store;
    private readonly MesDbContext _db;
}