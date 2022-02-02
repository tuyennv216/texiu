namespace texiu.Data.Model;

public class Address
{
	public string Province { get; set; } = string.Empty;
	public string[] Districts { get; set; } = Array.Empty<string>();
}
