using System.Collections;

namespace texiu.Interface;

public interface IArrayService
{
	public int[] IndexesByRatio(int length, string ratio);
	public int[] ShuffleIndex<T>(T[] array);
	public T[] Shuffle<T>(T[] array);
}
