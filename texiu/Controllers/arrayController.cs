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

		_logger.LogInformation("Load array controller successful!");
	}

	[HttpPost]
	public ActionResult shuffle(object[] array)
	{
		var output = _arrayService.Shuffle(array);
		return Ok(output);
	}

}
