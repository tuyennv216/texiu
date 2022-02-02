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
	public ActionResult splitNumber(int number, int upper)
	{
		var output = _randomService.RandomSplitNumber(number, upper);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult flag(int length)
	{
		var output = _randomService.RandomFlagInt(length);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult flagOne(int length, int bitOneLength)
	{
		var output = _randomService.RandomFlagInt(length, bitOneLength);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult name(int quantity)
	{
		var output = _randomService.RandomName(quantity);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult username(int length, int quantity)
	{
		var output = _randomService.RandomUsername(length, quantity);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult password(int length, int quantity)
	{
		var output = _randomService.RandomPassword(length, quantity);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult address(int quantity)
	{
		var output = _randomService.RandomAddress(quantity);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult zipcode(int quantity)
	{
		var output = _randomService.RandomZipcode(quantity);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult age(int lower, int upper, int quantity)
	{
		var output = _randomService.RandomAge(lower, upper, quantity);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult indexByRatio(int length, string ratio)
	{
		var output = _randomService.RandomIndexByRatio(length, ratio);
		return Ok(output);
	}

}
