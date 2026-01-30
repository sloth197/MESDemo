using System.Text.Json;
using MES.Api.Dtos;
using MES.Domain.Entities;
using MES.Infrastructure.Persistence;

namespace MES.Api.Services;

//커맨드 관리 서비스
public class CommandService
{
    private readonly MesDbContext _db;
    public CommandService(MesDbContext db)
    {
        _db = db;
    }
    public async Task EvaluateAndCreateCommandAsync(EquipSampleDto sample)
    {
        bool hasPending = _db.EquipCommands.Any(c => c.EquipId == sample.EquipId &&
                                                     c.Status == "Pending");
        if (sample.Temperature >= 90)
        {
            var payload = jsomSerializer.Serialize(new { speed = 60 });
            var cmd = new EquipCommand
            {
                EquipId = sample.EquipId,
                CommandType = "ChangeSpeed",
                Payload = payload,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };
            _db.EqipCommands.Add(cmd);
            await _db.SaveChangesAsync();
        }
    }
    public List<EquipCommand> GetPending(int equipId)
    {
        return _db.EquipCommands .Where(c => c.EquipId == equipId &&
                                        c.Status == "Pending")
                                        .ToList();
    }
    public async Task<bool> AckAsync(int commandId, bool success)
    {
        var cmd = await _db.EquipCommands.FindAsync(commandId);
        if (cmd == null)
        {
            return false;
        }
        cmd.Status = success ? "Applied" : "Failed";
        cmd.AppliedAt = DateTime.Now;
        await _db.SaveChangesAsync();
        return true;
    }
    
    /* //메모리 기반 커맨드 저장
    private readonly List<CommandDto> _commands = new();
    //커맨드 ID 자동 증가
    private int _commandSeq = 1;
}
//샘플 기반 커맨드 자동 생성
public void EvalauteAndCreateCommandIfNeeded(EquipSampleFto sample)
{
    //보류 커맨드 실행 시 중복 방지
    var hasPending = _commands.Any(c => c.EquipId == sample.EquipId &&
                                        c.Status == "Pending" &&
                                        c.CommandType == "ChangeSpeed");
    if (hasPending)
    {
        return;
    }
    //속도가 90 이상이면 60으로 강제 설정
    if (sample.Temperature >= 90)
    {
        CreateChangeSpeed(sample.EquipId, 60);
        return;
    }
    //상태가 경고면 속도 80 설정
    if (sample.Status == "Warning")
    {
        CreateChangeSpeed(sample.EquipId, 80);
        return;
    }

    //보류 상태인 커맨드 반환
    public List<CommandDto> GetPendingCommands(int equipId)
    {
        return _commands .Where(c => c.EquipId == equipId &&
                                     c.Status == "Pending")
                                     .ToList();
    }

    //Ack 수신시 커맨드를 허락(?), 실패로 변경
    public bool AckCommand(int commandId, bool success, string? message)
    {
        var cmd = _commands.FirstOrDefault(c => c.CommandId == commandId);
        if (cmd == null)
        {
            return false;
        }
        cmd.Status = success ? "Applied" : "Failed";
        return false;
    }

    //속도 변화 커맨드 생성 헬퍼
    private void CreateChangeSpeed(int equipId, double speed)
    {
        //equipSim가 분석할 수 있게 JSON 문자열로 payload구성
        var payloadObj  = new { speed = speed };
        var payloadJson = JsonSerializer.Serialize(payloadObj);
        var cmd = new CommandDto
        {
            CommandId = _commandSeq++,
            EquipId = equipId,
            CommandType = "ChangeSpeed",
            Payload = payloadJson,
            Status = "Pending"
        };
        _commands.Add(cmd);
    } */
}