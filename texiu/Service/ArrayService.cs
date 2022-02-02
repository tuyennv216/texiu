using System.Security.Cryptography;
using texiu.Interface;

namespace texiu.Service;

public class ArrayService : IArrayService
{
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
		for (var i=0; i<array.Length; i++)
		{
			var rndIndex = RandomNumberGenerator.GetInt32(i, array.Length);
			var value = array[rndIndex];
			array[rndIndex] = array[i];
			array[i] = value;
		}
		return array;
	}

}
