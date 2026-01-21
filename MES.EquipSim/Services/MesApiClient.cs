namespace MES.EquipSim.Services;

public class MesApiClient
{
    private readonly HttpClient _client;
    public MesApiClient(string baseUrl)
    {
        _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public Task SendSampleAsync(EquipSampleDto sample)
        => _client.PostAsJsonAsync("/api/sample", sample);

    public Task<List<EquipCommandDto>?> GetPendingCommands(int equipId)
        => _client.GetFromJsonAsync("/api/command/ack", ack);
}