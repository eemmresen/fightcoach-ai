using FightCoachAI.Application.DTOs.Analysis;
using FightCoachAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FightCoachAI.WebAPI.Controllers;

[ApiController]
[Route("api/analysis")]
[Authorize]
public class AnalysisController : ControllerBase
{
    private readonly IAnalysisService _analysisService;

    public AnalysisController(IAnalysisService analysisService)
    {
        _analysisService = analysisService;
    }

    [HttpGet("{videoId:guid}")]
    public async Task<ActionResult<AnalysisResultResponse>> GetAnalysis(Guid videoId)
    {
        return Ok(await _analysisService.GetByVideoIdAsync(videoId));
    }

    [HttpGet("history")]
    public async Task<ActionResult<AnalysisHistoryResponse>> GetHistory([FromQuery] int limit = 20)
    {
        var userId = GetUserId();
        return Ok(await _analysisService.GetHistoryAsync(userId, limit));
    }

    [HttpPost("callback")]
    [AllowAnonymous]
    public async Task<IActionResult> Callback([FromBody] AnalysisCallbackRequest request)
    {
        await _analysisService.SaveAnalysisResultAsync(request.VideoId, new SaveAnalysisRequest
        {
            VideoId = request.VideoId,
            FightIq = request.Analysis.FightIq,
            GuardScore = request.Analysis.GuardScore,
            DefenseScore = request.Analysis.DefenseScore,
            FootworkScore = request.Analysis.FootworkScore,
            AttackScore = request.Analysis.AttackScore,
            ConsistencyScore = request.Analysis.ConsistencyScore,
            AiCoachFeedback = request.Analysis.AiCoachFeedback,
            ErrorsJson = request.Analysis.ErrorsJson,
            CombinationsJson = request.Analysis.CombinationsJson,
            ProcessingTimeMs = request.Analysis.ProcessingTimeMs,
        });
        return Ok(new { status = "saved" });
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}

public class AnalysisCallbackRequest
{
    public Guid VideoId { get; set; }
    public AnalysisCallbackData Analysis { get; set; } = null!;
}

public class AnalysisCallbackData
{
    public int FightIq { get; set; }
    public int GuardScore { get; set; }
    public int DefenseScore { get; set; }
    public int FootworkScore { get; set; }
    public int AttackScore { get; set; }
    public int ConsistencyScore { get; set; }
    public string? ErrorsJson { get; set; }
    public string? CombinationsJson { get; set; }
    public string? AiCoachFeedback { get; set; }
    public int? ProcessingTimeMs { get; set; }
}
