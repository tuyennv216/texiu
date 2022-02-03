using Microsoft.AspNetCore.Mvc;
using texiu.Interface;

namespace texiu.Controllers;

[ApiController]
[Route("[controller]")]
public class textController : ControllerBase
{
	private readonly ILogger<textController> _logger;
	private readonly ITextService _textService;

	public textController(ILogger<textController> logger, ITextService textService)
	{
		_logger = logger;
		_textService = textService;
	}

	[HttpGet("missing-char")]
	public ActionResult MissingChar(string text, float percent)
	{
		var output = _textService.MissingChar(text, percent);
		return Ok(output);
	}

	[HttpGet("missing-word")]
	public ActionResult MissingWord(string text, float percent)
	{
		var output = _textService.MissingWord(text, percent);
		return Ok(output);
	}
}
