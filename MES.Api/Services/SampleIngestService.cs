using Mes.Api.Dtos;

namespace MES.Api.Services;

//Sample 수신 처리 서비스
public class SampleIngestService
{
    //최근 샘플들을 설비별로 메모리에 저장
    private readonly Dictionary<int, EquipSampleDto> _latestSamples = new();
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