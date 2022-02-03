namespace texiu.Data.Model;

public class AgeRatio
{
	public string Range { get; set; } = string.Empty;
	public float Chance { get; set; }
	public int Min => int.Parse(Range.Split(',')[0]);
	public int Max => int.Parse(Range.Split(',')[1]);
}
