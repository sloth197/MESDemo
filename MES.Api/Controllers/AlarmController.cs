using MES.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MES.Api.Controllers;

[ApiController]
[Route("api/alarm")]
public class AlarmController : ControllerBase
{
    private readonly AlarmService _service;
    public AlarmController (AlarmService service)
    {
        _service = service;
    }

    //설비 알람 목록 조회
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int equipId, [FromQuery] bool activeOnly = false)
    {
        if (equipId <= 0)
        {
            return BadRequest("equipId is required");
        }
        var result = await _alarmService.GetAlarmAsync(equipId,activeOnly);
        return Ok(result);

    }
    //사용자가 알람을 확인
    //Id -> alarmId
    [HttpPost("alarmId:int}/ack")]
    public async Task<IActionResult> Ack (int alarmId)
    {
        await _alarmService.AckAsync(alarmId);
        return Ok();
    }

    //사용자가 알람을 해제
    [HttpPost("{alarmId:int}/clear")]
    public async Task<IActionResult> Clear(int alarmId)
    {
        await _alarmService.ClearAsync(alarmId);
        return Ok();
    }
}