using Microsoft.AspNetCore.Mvc;
using MES.Api.Dtos;
using MES.Api.Services;

namespace MES.Api.Controllers;

[ApiController]
[Route("api/sample")]
public class SampleController : ControllerBase
{
    private readonly SampleIngestService _sampleIngest;
    private readonly CommandService _commandService;

    public SampleController(SampleIngestService sampleIngest, CommandService commandService)
    {
        _sampleIngest = sampleIngest;
        _commandService = commandService;
    }
    //EquipSim이 샘플을 전송
    [HttpPost]
    public IActionResult PostSample([FromBody]
    EquipSampleDto sample)
    {
        //샘플 메모리 저장
        _sampleIngest.Ingest(sample);
        //룰 기반 커맨드 생성여부 판단
        _commandService.EvaluateAndCreateCommandIfNeeded(sample);
        //성공 응답
        return Ok(new {ok = true});
    }
    [HttpGet("latest")]
    public IActionResult GetLatest([FromQuery] int equipId)
    {
        var sample = _sampleIngest.GetLatest(equipId);
        if (sample == null)
        {
            return NotFound();
        }
        return Ok(sample);
    } 
}