namespace texiu.Data.Model;

public class Wordset
{
	public Dictionary<string, string[]> Mapping { get; set; } = new Dictionary<string, string[]>();
	public string[] Units { get; set; } = Array.Empty<string>();
	public int Upper { get; set; }

	public string[] GetItems(string text)
	{
		if (text.Length == 2) return Mapping[text];

		var output = new string[text.Length / 2];
		for (var i = 0; i < text.Length; i += 2)
		{
			output[i >> 1] = text.Substring(i, 2);
		}
		return output;
	}

}
