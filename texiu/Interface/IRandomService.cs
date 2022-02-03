using texiu.Model;

namespace texiu.Interface;

public interface IRandomService
{
	public T RandomItem<T>(T[] items);
	public T RandomItem<T>(T[] items, int lower, int upper);
	public T RandomItem<T>(T[] items, int[] indexes);
	public T RandomItem<T>(T[] items, int[] indexes, int lower, int upper);
	public int[] RandomSplitNumber(int number, int upper);

	public int[] RandomFlagInt(int length);
	public int[] RandomFlagInt(int length, int bitOneLength);
	public string[] RandomName(int quantity);
	public string[] RandomUsername(int length, int quantity);
	public string[] RandomPassword(int length, int quantity);
	public string[] RandomAddress(int quantity);
	public int[] RandomZipcode(int quantity);
	public int[] RandomAge(int lower, int upper, int quantity);
	public int[] RandomIndexByRatio(int length, string ratio);

	public Account[] RandomAccounts(int quantity);
	public Person[] RandomPeople(int quantity, int fromAge = 1, int toAge = 99);
}
