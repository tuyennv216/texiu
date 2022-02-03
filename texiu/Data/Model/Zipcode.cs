namespace texiu.Data.Model;

public class Zipcode
{
	public string Postcode { get; set; } = string.Empty;
	public string Province { get; set; } = string.Empty;
	public float Chance { get; set; }
	public int Min => int.Parse(Postcode.Split(':')[0]);
	public int Max => int.Parse(Postcode.Split(':')[1]);
}
