//--------------------------------//
//            Main Logic          //
//--------------------------------//
using MES.Application.Interfaces;
using MES.Domain.Entities;
using MES.Domain.Enums;
using MES.Intfrastructure.Persistence;

namespace MES.Application.Services;

public class CommandService : ICommandService
{
    private readonly MesDbContext _db;
    public CommandService(MesDbContext db)
    {
        _db = db;
    }
    //Command 조회
    public async Task<List<EquipCommand>> GetPendingAsync(int equipId)
    {
        return _db.EquipCommands
            .Where(c => c.EquipId == equipId && c.status == CommandStatus.Pending)
            .OrderBy(c => c.CreateAt)
            .ToList();
    }
    //Command 처리
    public async Task HandleAckAsync(int commandId, bool success)
    {
        var command = await _db.EquipCommands.FindAsync(commandId);
        if (command == null)
        {
            return;
        }
        command.Status = success ? CommandStatus.Applied : CommandStatus.Failed;
        command.AppliedAt = DateTile.Now;

        //Fail -> Alarm
        if (!success)
        {
            _db.Alarms.Add(new Alarm
            {
                EquipId = command.EquipId,
                Code = "COMMAND_FAILED",
                Message = $"Command {Command.CommandType} failed",
                Level = AlarmLevel.Warning,
                CreateAt = DateTime.Now
            });
        }
        await _db.SaveChangesAsync();
    }
}