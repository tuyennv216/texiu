using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using texiu.Interface;
using texiu.Output.Data;

namespace texiu.Controllers;

[ApiController]
[Route("[controller]")]
public class dataController : ControllerBase
{
	private readonly ILogger<dataController> _logger;
	private readonly IMapper _mapper;
	private readonly IDataService _dataService;

	public dataController(ILogger<dataController> logger, IMapper mapper, IDataService dataService)
	{
		_logger = logger;
		_mapper = mapper;
		_dataService = dataService;
	}

	[HttpGet("address")]
	public ActionResult GetAddress()
	{
		var data = _dataService.GetAddresses();
		var output = _mapper.Map<IEnumerable<Address>>(data);
		return Ok(output);
	}

	[HttpGet("age-ratio")]
	public ActionResult GetAgeRatio()
	{
		var data = _dataService.GetAgeRatio();
		var output = _mapper.Map<IEnumerable<AgeRatio>>(data);
		return Ok(output);
	}

	[HttpGet("first-name")]
	public ActionResult GetFirstnames()
	{
		var data = _dataService.GetFirstnames();
		var output = _mapper.Map<IEnumerable<Firstname>>(data);
		return Ok(output);
	}

	[HttpGet("middle-name")]
	public ActionResult GetMiddlenames()
	{
		var data = _dataService.GetMiddlenames();
		var output = _mapper.Map<IEnumerable<Middlename>>(data);
		return Ok(output);
	}

	[HttpGet("sur-name")]
	public ActionResult GetSurname()
	{
		var data = _dataService.GetSurnames();
		var output = _mapper.Map<IEnumerable<Surname>>(data);
		return Ok(output);
	}

	[HttpGet("zipcode")]
	public ActionResult GetZipcodes()
	{
		var data = _dataService.GetZipcodes();
		var output = _mapper.Map<IEnumerable<Zipcode>>(data);
		return Ok(output);
	}

	[HttpGet("passwordset")]
	public ActionResult GetPasswordsets()
	{
		var data = _dataService.GetPasswordsets();
		var output = _mapper.Map<IEnumerable<Passwordset>>(data);
		return Ok(output);
	}

}
