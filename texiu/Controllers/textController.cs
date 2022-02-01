using Microsoft.AspNetCore.Mvc;
using texiu.Interface;

namespace texiu.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class textController : ControllerBase
{
	private readonly ILogger<textController> _logger;
	private readonly ITextService _textService;

	public textController(ILogger<textController> logger, ITextService textService)
	{
		_logger = logger;
		_textService = textService;

		_logger.LogInformation("Load text controller successful!");
	}

	[HttpGet]
	public ActionResult missingChar(string text, float percent, char replaceChar = '_')
	{
		var output = _textService.MissingChar(text, percent, replaceChar);
		return Ok(output);
	}

	[HttpGet]
	public ActionResult missingWord(string text, float percent, char replaceChar = '_')
	{
		var output = _textService.MissingWord(text, percent, replaceChar);
		return Ok(output);
	}
}
