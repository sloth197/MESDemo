using MES.Domain.Entities;
using MES.Infrastructure.Persistence;

namespace MES.Api.Services;

public class AlarmService
{
    private readonly MesDbContext _db;
    public AlarmService(MesDbContext db)
    {
        _db = db;
    }

    public async T ask RaiseAlarmAsync(
        int equipId,
        string code,
        string message,
        string level )
    {
        bool exists = _db.Alarms.Any( a=> a.EquipId == equipId &&
                                          a.Code == code && 
                                          a.ClearAt == null);
        if (exists)
        {
            return;
        }
        var alarm = new alarm{
            EquipId  = equipId,
            Code = code,
            Message = message,
            Level = level,
            CreateAt = DateTime.Now
        };
        _db.Alarms.Add(alarm);
        await _db.SaveChangesAsync();
    }
    public async  Task AckASync(int alarmId)
    {
        var alarm = await _db.Alarms.FindAsync(alarmId);
        if (alarm == null || alarm.AckAt != null)
        {
            return;
        }
        alarm.AckAt = DateTime.Now;
        await _db.SaveChangesAsync();
    }
    public async Task AutoClearAsync(int equipId, string code)
    {
        var alarms = _db.Alarms.Wherer(a => a.EquipId == equipId &&
                                            a.Code == code && 
                                            a.ClearAt == null );
        foreach (var alarm in alarms)
        {
            alarm.ClearAt = DateTime.Now;
        }
        await _db.SaveChangesAsync();
    }
}