using Microsoft.AspNetCore.Mvc;
using texiu.Interface;

namespace texiu.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class dataController : ControllerBase
{
	private readonly ILogger<dataController> _logger;
	private readonly IDataService _dataService;

	public dataController(ILogger<dataController> logger, IDataService dataService)
	{
		_logger = logger;
		_dataService = dataService;
	}

	[HttpGet]
	public ActionResult address()
	{
		var output = _dataService.GetAddresses();
		return Ok(output);
	}

	[HttpGet]
	public ActionResult ageRatio()
	{
		var output = _dataService.GetAgeRatio();
		return Ok(output);
	}

	[HttpGet]
	public ActionResult firstname()
	{
		var output = _dataService.GetFirstnames();
		return Ok(output);
	}

	[HttpGet]
	public ActionResult middlename()
	{
		var output = _dataService.GetMiddlenames();
		return Ok(output);
	}

	[HttpGet]
	public ActionResult surname()
	{
		var output = _dataService.GetSurnames();
		return Ok(output);
	}

	[HttpGet]
	public ActionResult zipcode()
	{
		var output = _dataService.GetZipcodes();
		return Ok(output);
	}

	[HttpGet]
	public ActionResult passwordset()
	{
		var output = _dataService.GetPasswordsets();
		return Ok(output);
	}

}
