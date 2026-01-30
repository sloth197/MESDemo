using Mes.Api.Dtos;
using MES.Domain.Entities;
using MES.Infrastructure.Persistence;

namespace MES.Api.Services;

//Sample 수신 처리 서비스
public class SampleIngestService
{
    //설비 샘플 수신, DB 저장
    private readonly MesDbContext _db;
    //최근 샘플들을 설비별로 메모리에 저장
    private readonly Dictionary<int, EquipSampleDto> _latestSamples = new();
    public SampleIngestService(MesDbContext db)
    {
        _db = db;
    }
    public async Task IngestAsync(EquipSampleDto dto)
    {
        var sample = new EquipSample
        {
            EquipId = dto.EquipSample,
            Timestamp = dto.Timestamp,
            ProdCount = dto.ProdCount,
            DefectCount = dto.DefectCount,
            Temperature = dto.Temperature,
            Speed = dto.Speed
        };
        _db.EquipSamples.Add(sample);
        await _db.SaveChangesAsync();

        if (dto.Temperature >= 90)
        {
            await _alarmService.RaiseAlarmAsync(
                dto.EquipId,
                "TEMP_HIGH",
                "Temperature exceed threshold",
                "Critical"
            );
        }
        else
        {
            await _alarmService.AutoClearAsync(
                dto.EquipId,
                "TEMP_HIGH"
            );
        }
        await _db.SaveChangesAsync();
    }
    //최신 샘플 업데이트
    public void Ingest(EquipSampleDto sample)
    {
        _latestSamples[sample.EquipId] = sample;
    }
    public EquipSampleDto? GetLatest(int equipId)
    {
        _latestSamples.TryGetValue(equipId, out var sample);
        return sample;
    }
}