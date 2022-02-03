using System.Collections;

namespace texiu.Interface;

public interface IArrayService
{
	public int[] IndexesByRatio(int length, string ratio);
	public int[] SegmentsByRatio(int length, string ratio);
	public int[] MakeIncrementAdd(int[] array);
	public int FindLeftIndex(int[] array, int value);
	public int FindRightIndex(int[] array, int value);
	public int[] ShuffleIndex<T>(T[] array);
	public T[] Shuffle<T>(T[] array);
}
