using MES.Api.DTOs.Command;
using MES.Application.Interfaces;
using MES.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MES.Api.Controllers;

[ApiController]
[Route("api/command")]
public class CommandController : ControllerBase
{
    private readonly ICommandService _service;
    public CommandController(ICommandService service)
    {
        _service = service;
    }
    //시뮬레이터에서 Command 조회
    [HttpGet("pending/{equipId}")]
    public async Task<ActionResult<List<EquipCommand>>> GetPending(int equipId)
    {
        var commands = await _service.GetPendingAsync(equipId);
        return Ok(commands);    
    }
    //시뮬레이터에서 Command 결과 전송
    [HttpPost("ack")]
    public async Task<IActionResult> Ack( [FromBody] CommandAckRequest request)
    {
        await _service.HandleAckAsync( request.CommandId,
                                       request.Success);
        return Ok;
    }
}