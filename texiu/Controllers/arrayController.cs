using Microsoft.AspNetCore.Mvc;
using texiu.Interface;

namespace texiu.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class arrayController : ControllerBase
{
	private readonly ILogger<arrayController> _logger;
	private readonly IArrayService _arrayService;

	public arrayController(ILogger<arrayController> logger, IArrayService arrayService)
	{
		_logger = logger;
		_arrayService = arrayService;
	}

	[HttpPost]
	public ActionResult indexRatio(int length, string ratio)
	{
		var output = _arrayService.IndexesByRatio(length, ratio);
		return Ok(output);
	}

	[HttpPost]
	public ActionResult segmentRatio(int length, string ratio)
	{
		var output = _arrayService.SegmentsByRatio(length, ratio);
		return Ok(output);
	}

	[HttpPost]
	public ActionResult incrementAdd(int[] array)
	{
		var output = _arrayService.MakeIncrementAdd(array);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult findLeftIndex(int[] array, int value)
	{
		var output = _arrayService.FindLeftIndex(array, value);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult findRightIndex(int[] array, int value)
	{
		var output = _arrayService.FindRightIndex(array, value);
		return Ok(output);
	}

	[HttpPost]
	public ActionResult shuffle(object[] array)
	{
		var output = _arrayService.Shuffle(array);
		return Ok(output);
	}

	[HttpPost]
	public ActionResult shuffleIndex(object[] array)
	{
		var output = _arrayService.ShuffleIndex(array);
		return Ok(output);
	}

}
