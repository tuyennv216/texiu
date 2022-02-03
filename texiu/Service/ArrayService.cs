using System.Security.Cryptography;
using texiu.Interface;

namespace texiu.Service;

public class ArrayService : IArrayService
{
	#region indexing
	public int[] IndexesByRatio(int length, string ratio)
	{
		var chances = ratio.Split(",").Select(x => (int)Math.Ceiling(float.Parse(x))) ?? Array.Empty<int>();
		var total = chances.Sum();
		var segments = chances.Select(x => x * length / total).ToArray();
		if (segments.Length == 0)
		{
			var defaultArr = new int[length];
			Array.Fill(defaultArr, 0);
			return defaultArr;
		}

		var index = 0;
		var output = new int[length];
		for (var i = 0; i < segments.Length; i++)
		{
			Array.Fill(output, i, index, segments[i]);
			index += segments[i];
		}

		if (segments.Length > 0)
		{
			Array.Fill(output, segments.Length - 1, index, length - index);
		}

		return output;
	}

	public int[] SegmentsByRatio(int length, string ratio)
	{
		var chances = ratio.Split(",").Select(x => (int)Math.Ceiling(float.Parse(x))) ?? Array.Empty<int>();
		var total = chances.Sum();
		var segments = chances.Select(x => x * length / total).ToArray();
		var segmentsTotal = segments.Sum();
		if (segments.Length > 0 && segmentsTotal != length)
			segments[segments.Length - 1] += length - segmentsTotal;
		return segments;
	}
	#endregion

	#region modify
	// add to each item with the previous item value
	public int[] MakeIncrementAdd(int[] array)
	{
		if (array.Length < 2) return array;
		for (var i = 1; i < array.Length; i++)
			array[i] += array[i - 1];
		return array;
	}
	#endregion

	#region find index
	// find the first index of array where the value greater than or equal
	public int FindLeftIndex(int[] array, int value)
	{
		for (var i = 0; i < array.Length; i++)
		{
			if (value >= array[i]) return i;
		}
		return array.Length - 1;
	}

	// find the last index of array where the value less than
	public int FindRightIndex(int[] array, int value)
	{
		for (var i = array.Length; i > 0; i--)
		{
			if (value < array[i]) return i;
		}
		return 0;
	}
	#endregion

	#region shuffle
	public int[] ShuffleIndex<T>(T[] array)
	{
		var output = array.Select((x, i) => i).ToArray();
		for (var i = 0; i < array.Length; i++)
		{
			var rndIndex = RandomNumberGenerator.GetInt32(i, array.Length);
			var value = output[rndIndex];
			output[rndIndex] = output[i];
			output[i] = value;
		}
		return output;
	}

	public T[] Shuffle<T>(T[] array)
	{
		for (var i = 0; i < array.Length; i++)
		{
			var rndIndex = RandomNumberGenerator.GetInt32(i, array.Length);
			var value = array[rndIndex];
			array[rndIndex] = array[i];
			array[i] = value;
		}
		return array;
	}
	#endregion
}
