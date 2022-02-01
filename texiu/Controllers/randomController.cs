using Microsoft.AspNetCore.Mvc;
using texiu.Interface;

namespace texiu.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class randomController : ControllerBase
{
	private readonly ILogger<randomController> _logger;
	private readonly IRandomService _randomService;

	public randomController(ILogger<randomController> logger, IRandomService randomService)
	{
		_logger = logger;
		_randomService = randomService;
	}

	[HttpGet]
	public ActionResult randomFlag(int length)
	{
		var output = _randomService.RandomFlagInt(length);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult randomFlagOne(int length, int bitOneLength)
	{
		var output = _randomService.RandomFlagInt(length, bitOneLength);
		return Ok(output);
	}

}
