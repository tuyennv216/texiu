using Microsoft.AspNetCore.Mvc;
using texiu.Interface;

namespace texiu.Controllers;

[ApiController]
[Route("[controller]")]
public class arrayController : ControllerBase
{
	private readonly ILogger<arrayController> _logger;
	private readonly IArrayService _arrayService;

	public arrayController(ILogger<arrayController> logger, IArrayService arrayService)
	{
		_logger = logger;
		_arrayService = arrayService;
	}

	[HttpGet("index-ratio")]
	public ActionResult IndexRatio(int length, string ratio)
	{
		var output = _arrayService.IndexesByRatio(length, ratio);
		return Ok(output);
	}

	[HttpGet("segment-ratio")]
	public ActionResult SegmentRatio(int length, string ratio)
	{
		var output = _arrayService.SegmentsByRatio(length, ratio);
		return Ok(output);
	}

	[HttpPost("increment-add")]
	public ActionResult IncrementAdd(int[] array)
	{
		var output = _arrayService.MakeIncrementAdd(array);
		return Ok(output);
	}

	[HttpPost("find-left-index")]
	public ActionResult FindLeftIndex(int[] array, int value)
	{
		var output = _arrayService.FindLeftIndex(array, value);
		return Ok(output);
	}

	[HttpPost("find-right-index")]
	public ActionResult FindRightIndex(int[] array, int value)
	{
		var output = _arrayService.FindRightIndex(array, value);
		return Ok(output);
	}

	[HttpPost("shuffle")]
	public ActionResult Shuffle(object[] array)
	{
		var output = _arrayService.Shuffle(array);
		return Ok(output);
	}

	[HttpPost("shuffle-index")]
	public ActionResult ShuffleIndex(object[] array)
	{
		var output = _arrayService.ShuffleIndex(array);
		return Ok(output);
	}

}
