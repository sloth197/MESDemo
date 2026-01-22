namespace MES.Domain.Entities;

//데이터베이스에 저장되는 룰 정의
public class RuleDefinition
{
    public int RuleDefinitionId { get; set; }
    public string Name { get; set; } = "";
    //어떤 장비에 적용되는 룰 확인
    public int EquipId { get; set; }
    public bool Enabled { get; set; } = true;
    
    //조건JSON
    public string ConditionJson { get; set; } = "";
    //액션JSON
    public string ActionJson { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}