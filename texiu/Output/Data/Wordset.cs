namespace texiu.Output.Data;

public class Wordset
{
	public Dictionary<string, string[]> Mapping { get; set; } = new Dictionary<string, string[]>();
	public string[] Units { get; set; } = Array.Empty<string>();
	public int Upper { get; set; }
}
