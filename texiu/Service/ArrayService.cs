using System.Collections;
using System.Security.Cryptography;
using texiu.Interface;

namespace texiu.Service;

public class ArrayService : IArrayService
{
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
