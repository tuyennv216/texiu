using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using texiu.Interface;
using texiu.Output.Model;

namespace texiu.Controllers;

[ApiController]
[Route("[controller]")]
public class randomController : ControllerBase
{
	private readonly ILogger<randomController> _logger;
	private readonly IMapper _mapper;
	private readonly IRandomService _randomService;

	public randomController(ILogger<randomController> logger, IMapper mapper, IRandomService randomService)
	{
		_logger = logger;
		_mapper = mapper;
		_randomService = randomService;
	}

	[HttpGet("split-number")]
	public ActionResult GetSplitNumber(int number, int upper)
	{
		var output = _randomService.RandomSplitNumber(number, upper);
		return Ok(output);
	}

	[HttpGet("flag")]
	public ActionResult GetFlag(int length)
	{
		var output = _randomService.RandomFlagInt(length);
		return Ok(output);
	}

	[HttpGet("flag-one")]
	public ActionResult GetFlagOne(int length, int bitOneLength)
	{
		var output = _randomService.RandomFlagInt(length, bitOneLength);
		return Ok(output);
	}

	[HttpGet("name")]
	public ActionResult GetName(int quantity)
	{
		var output = _randomService.RandomName(quantity);
		return Ok(output);
	}

	[HttpGet("username")]
	public ActionResult GetUsername(int length, int quantity)
	{
		var output = _randomService.RandomUsername(length, quantity);
		return Ok(output);
	}

	[HttpGet("password")]
	public ActionResult GetPassword(int length, int quantity)
	{
		var output = _randomService.RandomPassword(length, quantity);
		return Ok(output);
	}

	[HttpGet("address")]
	public ActionResult GetAddress(int quantity)
	{
		var output = _randomService.RandomAddress(quantity);
		return Ok(output);
	}

	[HttpGet("zipcode")]
	public ActionResult GetZipcode(int quantity)
	{
		var output = _randomService.RandomZipcode(quantity);
		return Ok(output);
	}

	[HttpGet("age")]
	public ActionResult GetAge(int lower, int upper, int quantity)
	{
		var output = _randomService.RandomAge(lower, upper, quantity);
		return Ok(output);
	}

	[HttpGet("index-ratio")]
	public ActionResult GetIndexRatio(int length, string ratio)
	{
		var output = _randomService.RandomIndexByRatio(length, ratio);
		return Ok(output);
	}

	[HttpGet("account")]
	public ActionResult GetAccounts(int quantity)
	{
		var output = _mapper.Map<IEnumerable<Account>>(_randomService.RandomAccounts(quantity));
		return Ok(output);
	}

	[HttpGet("people")]
	public ActionResult GetPeople(int quantity, int fromAge, int toAge)
	{
		var output = _mapper.Map<IEnumerable<Person>>(_randomService.RandomPeople(quantity, fromAge, toAge));
		return Ok(output);
	}

}
