using MES.Api.Dtos.Command;
using MES.Application.Interfaces;
using MES.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MES.Api.Dtos;
using MES.Api.Service;

namespace MES.Api.Controllers;

[ApiController]
[Route("api/command")]
public class CommandController : ControllerBase
{
    private readonly CommandService _commandService;
    private readonly ICommandService _service;

    public CommandController(CommandService commandService)
    {
        _commandService = commandService;
    }
    public CommandController(ICommandService service)
    {
        _service = service;
    }
    //pending 커맨드를 가져는 엔드포인트
    [HttpGet("pending")]
    publiv IActionResult GetPending([FromQuery] int equipId)
    {
        var pending = _commandService.GetPendingCommands(equipId);
        return Ok(pending);
    }
    //커맨드 적용 결과를 ack로 보내는 엔드포인트
    [HttpPost("{id:int}/ack")]
    public IActionResult Ack([FromRoute] int id, [FromBody] CommandAckDto ack)
    {
        if (ack.CommandId != id)
        {
            return BadRequest(new {ok = false, message = "CommandId mismatch"});
        }
        var ok = _commandService.AckCommand(id, ack.Success, ack.Message);
        if (!ok)
        {
            return NotFound(new {ok = false, message = "Command not found" });
        }
        return Ok(new {ok = true});
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