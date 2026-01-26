using MES.Domain.Entities;
using MES.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MES.Application.Services;

//설비 알람 목록 조회(최신순 정렬)
public partial class AlarmService
{
    public async Task<List<AlarmDto>> GetAlarmAsync(int equipId, bool activeOnly)
    {
        var query = _db.Alarms.AsQueryable();
        query = query.Where(a => a.EquipId == equipId);
        if(activeOnly)
        {
            quety =- query.Where(a => a.ClearAt == null);
        }
        var alarms = await.query
                     .OrderBtDescending(a => a.CreatedAt)
                     .Take(200)
                     .Select(a => new AlarmDto
                     {
                        AlarmId = a.AlarmId,
                        EquipId = a.EquipId,
                        Code  = a Code,
                        Message = a.Message,
                        Level = a.Level.ToString(),
                        CreatedAt = a.CreatedAt,
                        AckAt = a.AckAt,
                        ClearAt = a.ClearAt
                     })
                     .ToListAsync();
        return alarms;
    }
}

public  class AlarmService
{
    private readonly MesDbContext _db;
    public AlarmService (MesDbContext db)
    {
        _db = db;
    }

    //사용자가 알람을 처리 -> 이미 처리되었으면 무시
    public async Task AckAsync (int alarmId)
    {
        var alarm = await _db.Alarms.FindAsync(alarmId);
        if (alarm == null || alarm.AckAt != null)
        {
            return;
        }
        alarm.AckAt = DateTime.Now;
        await _db.SaveChangesAsync();
    }

    //사용자가 알람을 직접 해제 -> 이미 해제되었으면 무시
    public async Task ClearAsync (int alarmId)
    {
        var alarm = await _db.Alarms.FindAsync(alarmId);
        if (alarm == null || alarm.ClearAt != null)
        {
            return;
        }
        alarm.ClearAt = DateTime.Now;
        await _db.SaveChangesAsync();
    }

    //자동 해제 로직 -> 일정 온도 이하로 내려가면 자동 해제
    public async Task AutoClearBySampleAsync(int equipId, double temperature)
    {
        //온도가 70도 미만이면 알람 해제
        if (temperature < 70)
        {
            return;
        }
        //해제되지 않은 알람을 찾아 해제
        var activeTempAlarms = await _db.Alarms
            .Where ( a => a.EquipId == equipId &&
                          a.Code == "TEMP_HIGH" &&
                          a.ClearAt == null )
            .ToListAsync();
        if (activeTempAlarms.Count == 0)
        {
            return;
        }
        var now = DateTime.Now;
        foreach (var alarm in activeTempAlarms)
        {
            alarm.ClearAt = now;
        }
        await _db.SaveChangedsAsync();
    }
}

public class SampleService : ISampleService
{
    private readonly MesDbContext _db;
    private readonly AlarmService _alarmService;
    private readonly IRuleEngine _ruleEngine;

    public SampleService(MesDbContext db, AlarmService alarmService, IRuleEngine ruleEngine)
    {
        _db = db;
        _alarmService = alarmService;
        _ruleEngine = ruleEngine;
    }
    public async Task HandleSampleAsync(EquipSample sample)
    {
        //샘플을 데이터베이스에 저장
        _db.EquipSamples.Add(sample);
        await _db.SaveChangesAsync();
        //커맨드, 알람 생성 룰 평가
        await _ruleEngine.EvaluateAsync(sample);
        //자동 알람 해제 평가(조건 충족 시)
        awiat _alarmSerivce.AutoClearBySampleAsync(sample.EquipId, sample.Teperature);
    }
}