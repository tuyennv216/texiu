namespace texiu.Data.Model;

public class Middlename
{
	public string Names { get; set; } = string.Empty;
	public float Chance { get; set; }
	public string[] NamesArr => Names.Split(',');
}
