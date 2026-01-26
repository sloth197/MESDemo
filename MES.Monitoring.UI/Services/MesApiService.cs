using System.Net.Http.Json;

namespace MES.Monitoring.UI.Services;

public class MesApiService
{
    private readonly HttpClient _http;

    public MesApiService(HttpClient http)
    {
        _http = http;
    }

    //알람 목록 조회
    public async Task<List<AlarmDto>> GetAlarmAsync (int equipId, bool activeOnly = false)
    {
        var url = $"/api/alarm?equipId={equipId}&activeOnly={activeOnlt}";
        var result = await _http.GetFromJsonAsync<List<AlarmDto>>(url);
        return result ?? new List<AlarmDto>();
    }

    //알람 Ack api호출
    public async Task AckAlarmAsync(int alarmId)
    {
        await _http.PostAsync($"/api/alarm/{alarmId}/ack", null);
    }
    //알람 해체 api 호출
    public async Task ClearAlarmAsync(int alarmId)
    {
        await _http.PostAsync($"/api/alarm/{alarmId}/clear", null);
    }
}