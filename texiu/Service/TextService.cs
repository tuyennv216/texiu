using System.Text.RegularExpressions;
using texiu.Interface;

namespace texiu.Service;

public class TextService : ITextService
{
	private readonly IRandomService _randomService;
	private readonly IConfiguration _configuration;
	public TextService(IRandomService randomService, IConfiguration configuration)
	{
		_randomService = randomService;
		_configuration = configuration;
	}

	public string MissingChar(string text, float percent)
	{
		var missingNumber = (int)Math.Floor(text.Length * percent / 100);
		var rndFlag = _randomService.RandomFlagInt(text.Length, missingNumber);
		var chars = text.ToCharArray();
		for (var i = 0; i < chars.Length; i++)
		{
			if (rndFlag[i] == 1) chars[i] = ReplaceLetter(chars[i]);
		}
		var output = new string(chars);
		return output;
	}

	public string MissingWord(string text, float percent)
	{
		var words = text.Split(' ');
		var missingNumber = (int)Math.Floor(words.Length * percent / 100);
		var rndFlag = _randomService.RandomFlagInt(words.Length, missingNumber);
		for (var i = 0; i < words.Length; i++)
		{
			if (rndFlag[i] == 1) words[i] = ReplaceLetter(words[i]);
		}
		var output = string.Join(' ', words);
		return output;
	}

	public char ReplaceLetter(char chr)
	{
		var replaceChar = _configuration.GetValue("custom:text:replaceChar", '_');
		var rgx = new Regex(@"\p{L}|\d");
		var output = chr;
		if (rgx.IsMatch(chr.ToString())) output = replaceChar;
		return output;
	}

	public string ReplaceLetter(string text)
	{
		var replaceChar = _configuration.GetValue("custom:text:replaceChar", '_');
		var replaceStr = replaceChar.ToString();
		var rgx = new Regex(@"\p{L}|\d");
		var output = rgx.Replace(text, replaceStr);
		return output;
	}
}
