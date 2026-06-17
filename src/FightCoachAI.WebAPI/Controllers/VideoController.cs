using FightCoachAI.Application.DTOs.Video;
using FightCoachAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FightCoachAI.WebAPI.Controllers;

[ApiController]
[Route("api/videos")]
[Authorize]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;

    public VideoController(IVideoService videoService)
    {
        _videoService = videoService;
    }

    [HttpPost("upload")]
    [RequestSizeLimit(500_000_000)]
    public async Task<ActionResult<VideoUploadResponse>> Upload(IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file provided");

        var userId = GetUserId();
        var result = await _videoService.UploadAsync(userId, file.FileName, file.OpenReadStream());
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<VideoListResponse>> GetVideos()
    {
        var userId = GetUserId();
        return Ok(await _videoService.GetUserVideosAsync(userId));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VideoDetailResponse>> GetVideo(Guid id)
    {
        return Ok(await _videoService.GetVideoDetailAsync(id));
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
